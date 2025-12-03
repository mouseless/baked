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
      :disabled="!ready || waitingAction"
      :label
      @click="$emit('submit', model)"
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
defineEmits(["submit"]);

const model = ref({});

const { label, inputs } = schema;

const loading = context.injectLoading();
const waitingAction = context.injectWaitingAction();
const ready = ref(inputs.length === 0);

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  model.value = values;
}
</script>