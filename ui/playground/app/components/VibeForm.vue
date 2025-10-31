<template>
  <div class="flex flex-col gap-4">
    <Parameters
      v-if="parameters"
      :parameters="parameters"
      parameter-class="w-full"
      class="flex-col"
      @ready="onReady"
      @changed="onChanged"
    />
    <Button
      @click="submit"
      :loading icon="pi pi-save"
      :disabled="!ready"
      :label
    />
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Button } from "primevue";
import { useRuntimeConfig } from "#app";
import { useContext } from "#imports";
import { Parameters } from "#components";

const { public: { composables } } = useRuntimeConfig();
const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { label, endpoint, submitEventName, parameters } = schema;

const events = context.injectEvents();
const loading = ref(false);
const ready = ref(parameters.length === 0);
const body = ref();

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  console.log(values);

  body.value = values;
}

async function submit() {
  loading.value = true;
  await $fetch(endpoint.path,
    {
      baseURL: composables.useDataFetcher.baseURL,
      method: endpoint.method,
      body: body.value
    }
  );
  loading.value = false;

  if(submitEventName) {
    events.emit(submitEventName);
  }
}
</script>