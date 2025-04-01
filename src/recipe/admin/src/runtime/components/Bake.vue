<template>
  <component
    :is="is"
    v-model="model"
    :schema="descriptor.schema"
    :data="data"
    :loading="loading"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useComponentResolver, useContext, useDataFetcher } from "#imports";

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});

const model = defineModel({
  type: null,
  required: false
});

const componentResolver = useComponentResolver();
const context = useContext();
const dataFetcher = useDataFetcher();

context.add(name);

const injectedData = context.injectedData();
const is = componentResolver.resolve(descriptor.type, "None");
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const data = ref(dataFetcher.get(descriptor.data));
const loading = ref(shouldLoad);

onMounted(async() => {
  if(!shouldLoad) { return; }

  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    injectedData
  });
  loading.value = false;
});
</script>
