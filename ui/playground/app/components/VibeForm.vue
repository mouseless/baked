<template>
  <div class="flex flex-col gap-4">
    <Inputs
      v-if="inputs"
      :inputs="inputs"
      input-class="w-full"
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
import { Inputs } from "#components";

const { public: { apiBaseURL } } = useRuntimeConfig();
const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { label, action, submitEventName, inputs } = schema;

const events = context.injectEvents();
const loading = ref(false);
const ready = ref(inputs.length === 0);
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