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
      @click="$emit('submit')"
    />
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Button } from "primevue";
import { useContext } from "#imports";
import { Inputs } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const body = defineModel({ type: null });
defineEmits(["submit"]);

const { label, inputs } = schema;

const loading = context.injectLoading();
const ready = ref(inputs.length === 0);

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  body.value = values;
}
</script>