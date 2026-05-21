<template>
  <component
    :is="component"
    v-if="visible"
    :key="loading"
    :class="classes"
    v-bind="{
      ...baseAttrs,
      ...dataAttrs(),
      ...modelAttrs()
    }"
  >
    <template
      v-for="(_, slotName) in $slots"
      #[slotName]="slotProps"
    >
      <slot
        :name="slotName"
        v-bind="slotProps ?? {}"
      />
    </template>
  </component>
</template>
<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from "vue";
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
const parentLoading = context.injectLoading();
const path = parentPath && parentPath !== "" ? `${parentPath}/${name}` : name;
const events = context.injectEvents();
const contextData = context.injectContextData();
const component = componentResolver.resolve(descriptor.type, "MissingComponent");
const data = ref(dataFetcher.get({ data: descriptor.data, contextData }));
const shouldLoad = !parentLoading.value && dataFetcher.shouldLoad(descriptor.data);
const loading = ref(shouldLoad);
const visible = ref(true);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];
const baseAttrs = { };

context.providePath(path);
context.provideDataDescriptor(descriptor.data);
context.provideParentContext({ ...contextData.parent, data });

if(shouldLoad) {
  context.provideLoading(loading);
}

let executing = null;
if(descriptor.action) {
  executing = ref(false);
  context.provideExecuting(executing);
}

if(descriptor.schema) {
  baseAttrs.schema = descriptor.schema;
}

if(component.emits?.includes("submit")) {
  baseAttrs.onSubmit = updateModel;
}

let dataAttrs = () => ({});
if(descriptor.data) {
  dataAttrs = () => ({ data: data.value });
}

let lastModel = null;
let modelAttrs = () => ({});
if(component.props?.modelValue) {
  lastModel = ref();
  modelAttrs = () => ({ modelValue: model.value, "onUpdate:modelValue": updateModel });
  watch(model, updateModel);
}

let reactions = null;
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

async function updateModel(newModel) {
  if(component.props?.modelValue) {
    model.value = newModel;
  }

  if(!descriptor.action) { return; }

  if(component.props?.modelValue) {
    if(lastModel.value == newModel) { return; }

    lastModel.value = newModel;
  }

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