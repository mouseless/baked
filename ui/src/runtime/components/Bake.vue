<template>
  <component
    :is="is"
    :key="loading"
    v-model="model"
    :schema="descriptor.schema"
    :data
    :class="classes"
    @submit="onSubmit"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { onMounted, onUnmounted, ref } from "vue";
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
const data = ref(dataFetcher.get({ data: descriptor.data, injectedData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const loading = ref(shouldLoad);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];

context.provideData(data, "ParentData");

// TODO - review this in form components
if(descriptor.binding) {
  events.on(descriptor.binding, path, load);
}

if(shouldLoad || descriptor.action || descriptor.postAction) {
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

async function load() {
  loading.value = true;
  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    injectedData
  });
  loading.value = false;
  emit("loaded");
}

async function onSubmit() {
  if(!descriptor.action) { return; }

  loading.value = true;
  await actionExecuter.execute({ action: descriptor.action, injectedData, events });

  if(descriptor.postAction) {
    await actionExecuter.execute({ action: descriptor.postAction, injectedData, events });
  }
  loading.value = false;
}
</script>
