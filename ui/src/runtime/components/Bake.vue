<template>
  <component
    :is="render()"
    v-if="visible"
    :key="loading"
    :class="classes"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { h, onBeforeUnmount, onMounted, ref } from "vue";
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
const is = componentResolver.resolve(descriptor.type, "MissingComponent");
const data = ref(dataFetcher.get({ data: descriptor.data, contextData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
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
  if(!shouldLoad) { return; }

  await load();
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

function render() {
  const props = { };
  if(descriptor.schema) { props.schema = descriptor.schema; }
  if(descriptor.data) { props.data = data.value; }
  if(is.emits?.includes("submit")) { props.onSubmit = onModelUpdate; }
  if(is.props?.modelValue) {
    props.modelValue = model.value;
    props["onUpdate:modelValue"] = onModelUpdate;
  }

  return h(is, props);
}

async function onModelUpdate(newModel) {
  if(is.props?.modelValue) {
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
