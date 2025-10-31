<template>
  <Button @click="submit">{{ label }}</Button>
</template>
<script setup>
import { Button } from "primevue";
import { useRuntimeConfig } from "#app";
import { useContext } from "#imports";

const { public: { composables } } = useRuntimeConfig();
const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { label, endpoint, submitEventName } = schema;

const events = context.injectEvents();

async function submit() {
  await $fetch(endpoint.path,
    {
      baseURL: composables.useDataFetcher.baseURL,
      method: endpoint.method
    }
  );

  if(submitEventName) {
    events.emit(submitEventName);
  }
}
</script>