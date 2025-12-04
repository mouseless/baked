<template>
  <component
    :is="render()"
    :key="loading"
    :class="classes"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { h, onMounted, onUnmounted, ref } from "vue";
import { useActionExecuter, useComponentResolver, useContext, useDataFetcher, useFormat } from "#imports";

const actionExecuter = useActionExecuter();
const componentResolver = useComponentResolver();
const context = useContext();
const dataFetcher = useDataFetcher();
const { asClasses } = useFormat();

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});
const model = defineModel({ type: null });
const emit = defineEmits(["loaded"]);

context.providePath(name);
context.provideDataDescriptor(descriptor.data);

const path = context.injectPath();
const events = context.injectEvents();
const is = componentResolver.resolve(descriptor.type, "None");
const injectedData = context.injectData();
const data = ref(dataFetcher.get({ data: descriptor.data, contextData: injectedData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const loading = ref(shouldLoad);
const waitingAction = ref(false);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];

context.provideData(data, "ParentData");
context.provideWaitingAction(waitingAction);

// TODO - review this in form components
if(descriptor.binding) {
  events.on(descriptor.binding, path, load);
}

if(shouldLoad) {
  context.provideLoading(loading);
}

onMounted(async() => {
  if(!shouldLoad) { return; }

  await load();
});

// TODO - review this in form components
onUnmounted(() => {
  if(descriptor.binding) {
    events.off(descriptor.binding, path);
  }
});

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

async function load() {
  loading.value = true;
  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    contextData: injectedData
  });
  loading.value = false;
  emit("loaded");
}

async function onModelUpdate(newModel) {
  if(is.props?.modelValue) {
    model.value = newModel;
  }

  if(!descriptor.action) { return; }

  const contextData = { ...injectedData };
  if(newModel) {
    contextData.ModelData = newModel;
  }

  try {
    waitingAction.value = true;
    await actionExecuter.execute({ action: descriptor.action, contextData, events });

    if(descriptor.postAction) {
      await actionExecuter.execute({ action: descriptor.postAction, contextData, events });
    }
  } finally {
    waitingAction.value = false;
  }
}
</script>
