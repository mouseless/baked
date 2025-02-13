<template>
  <div class="w-[1000px] mx-auto my-4">
    <Panel :header="schema.title">
      <div v-if="data" class="grid grid-cols-2 gap-4">
        <component
          :is="prop.component.$type"
          v-for="prop in props"
          :key="prop.name"
          :schema="prop.component"
          :data="data[prop.name]"
        />
      </div>
    </Panel>
  </div>
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { props, path } = schema;
const { public: { apiBaseURL: baseURL } } = useRuntimeConfig();
const params = inject("params");
const data = ref();

onMounted(async() => {
  data.value = await $fetch(
    `${path}/${params[1]}`,
    {
      baseURL,
      headers: { Authorization: "token-jane" }
    }
  );
});
</script>
