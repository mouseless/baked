<template>
  <component
    :is="componentTemplate"
    v-if="visible"
    :key="loading"
    :class="classes"
    v-bind="getComponentProps()"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { nextTick, onBeforeUnmount, onMounted, ref } from "vue";
import { useActionExecuter, useComponentResolver, useContext, useDataFetcher, useFormat, useReactionHandler } from "#imports";

const actionExecuter = useActionExecuter();
const componentResolver = useComponentResolver();
const context = useContext();
const dataFetcher = useDataFetcher();
const { asClasses } = useFormat();
const reactionHandler = useReactionHandler();

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});
const model = defineModel({ type: null });
const emit = defineEmits(["loaded"]);

const parentPath = context.injectPath();
const path = parentPath && parentPath !== "" ? `${parentPath}/${name}` : name;
const events = context.injectEvents();
const contextData = context.injectContextData();
const componentTemplate = componentResolver.resolve(descriptor.type, "MissingComponent");
const componentProps = buildComponentProps();
const data = ref(dataFetcher.get({ data: descriptor.data, contextData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data);
const loading = ref(shouldLoad);
const executing = ref(false);
const visible = ref(true);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];
let reactions = null;

context.providePath(path);
context.provideDataDescriptor(descriptor.data);
context.provideParentContext({ ...contextData.parent, data });
context.provideExecuting(executing);

if(shouldLoad) {
  context.provideLoading(loading);
}

if(descriptor.reactions) {
  reactions = reactionHandler.create(`${path}:bake`, {
    reload(success) {
      if(!success) { return; }

      load();
    },
    show(success) {
      visible.value = success;
    }
  });
  reactions.bind(descriptor.reactions);
}

onMounted(async() => {
  if(shouldLoad) {
    await load();
  }
});

onBeforeUnmount(() => {
  if(descriptor.reactions) {
    reactions.unbind();
  }
});

async function load() {
  loading.value = true;
  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    contextData
  });
  loading.value = false;
  emit("loaded");
}

function buildComponentProps() {
  const result = {};

  if(descriptor.schema) { result.schema = descriptor.schema; }
  if(componentTemplate.emits?.includes("submit")) { result.onSubmit = onModelUpdate; }
  if(componentTemplate.props?.modelValue) { result["onUpdate:modelValue"] = onModelUpdate; }

  return result;
}

function getComponentProps() {
  const result = { ...componentProps };

  if(descriptor.data) { result.data = data.value; }
  if(componentTemplate.props?.modelValue) {
    result.modelValue = model.value;

    nextTick(() => onModelUpdate(model.value));
  }

  return result;
}

async function onModelUpdate(newModel) {
  if(componentTemplate.props?.modelValue) {
    model.value = newModel;
  }

  if(!descriptor.action) { return; }

  try {
    executing.value = true;
    await actionExecuter.execute({
      action: descriptor.action,
      contextData: { ...contextData, model: newModel },
      events
    });
  } finally {
    executing.value = false;
  }
}
</script>
