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
      icon="pi pi-save"
      :loading
      :disabled="!ready"
      :label
      @click="submit"
    />
  </div>
</template>
<script setup>
// TODO - review this in form components
import { ref } from "vue";
import { Button } from "primevue";
import { useRuntimeConfig } from "#app";
import { useContext } from "#imports";
import { Parameters } from "#components";

const { public: { apiBaseURL } } = useRuntimeConfig();
const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { label, action, submitEventName, parameters } = schema;

const events = context.injectEvents();
const loading = ref(false);
const ready = ref(parameters.length === 0);
const body = ref();

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  body.value = values;
}

async function submit() {
  loading.value = true;
  await $fetch(action.path,
    {
      baseURL: apiBaseURL,
      method: action.method,
      body: body.value
    }
  );
  loading.value = false;

  if(submitEventName) {
    events.emit(submitEventName);
  }
}
</script>