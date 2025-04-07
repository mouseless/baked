<template>
  <component
    :is="is"
    v-model="model"
    :schema="descriptor.schema"
    :data
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useComponentResolver, useContext, useDataFetcher } from "#imports";

const componentResolver = useComponentResolver();
const context = useContext();
const dataFetcher = useDataFetcher();

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});
const model = defineModel({ type: null, required: false });

context.add(name);

const is = componentResolver.resolve(descriptor.type, "None");
const injectedData = context.injectedData();
const data = ref(dataFetcher.get({ data: descriptor.data, injectedData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const loading = ref(shouldLoad);

context.setInjectedData(data, "ParentData");

if(shouldLoad) {
  context.setLoading(loading);
}

onMounted(async() => {
  if(!shouldLoad) { return; }

  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    injectedData
  });
  loading.value = false;
});
</script>
