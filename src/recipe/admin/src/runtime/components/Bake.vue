<template>
  <component
    :is="is"
    :key="loading"
    v-model="model"
    :schema="descriptor.schema"
    :data
    :class="classes"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useComponentResolver, useContext, useDataFetcher, useFormat } from "#imports";

const componentResolver = useComponentResolver();
const context = useContext();
const dataFetcher = useDataFetcher();
const { asClasses } = useFormat();

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});
const model = defineModel({ type: null, required: false });
const emit = defineEmits(["loaded"]);

context.add(name);
context.provideDataDescriptor(descriptor.data);

const is = componentResolver.resolve(descriptor.type, "None");
const injectedData = context.injectData();
const data = ref(dataFetcher.get({ data: descriptor.data, injectedData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const loading = ref(shouldLoad);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];

context.setInjectedData(data, "ParentData");

if(shouldLoad) {
  context.provideLoading(loading);
}

onMounted(async() => {
  if(!shouldLoad) { return; }

  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    injectedData
  });
  loading.value = false;
  emit("loaded");
});
</script>
