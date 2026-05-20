<template>
  <template v-if="dialogOptions">
    <Button
      :schema="dialogOptions.open"
      v-bind="$attrs"
      @click="visible = true"
    />
    <Dialog
      v-model:visible="visible"
      :header="l(title)"
      :style="{ width: 'min(700px, 90vw)' }"
      :pt="{
        header: !dialogOptions.message && inputs.length > 0 ? 'pb-0' : '',
        content: 'flex flex-col gap-8'
      }"
      closable
      modal
      dismissable-mask
      :draggable="false"
      @after-hide="emitSubmit"
    >
      <div v-if="dialogOptions.message">
        {{ l(dialogOptions.message) }}
      </div>
      <div
        v-if="inputs.length > 0"
        class="flex flex-col gap-4"
        :class="{ 'pt-[--p-dialog-header-padding]': !dialogOptions.message }"
      >
        <Inputs
          v-if="inputs"
          :inputs
          input-class="w-full"
          @ready="onReady"
          @changed="onChanged"
        />
      </div>
      <template #footer>
        <Button
          :schema="dialogOptions.cancel"
          @submit="() => visible = false"
        />
        <Button
          :schema="submit"
          :ready
          @submit="execute"
        />
      </template>
    </Dialog>
  </template>
  <template v-else>
    <div
      v-bind="$attrs"
      class="flex flex-col gap-8"
    >
      <h1 class="font-bold text-xl truncate">
        {{ l(title) }}
      </h1>
      <div class="flex flex-col gap-4">
        <Inputs
          v-if="inputs"
          :inputs
          input-class="w-full"
          @ready="onReady"
          @changed="onChanged"
        />
      </div>
      <Button
        v-tooltip.left="{
          disabled: !showValidationSummary,
          value: messages,
          pt: { text: 'text-sm' }
        }"
        :schema="submit"
        :ready
        @submit="$emit('submit', model)"
      />
    </div>
  </template>
</template>
<script setup>
import { ref, computed } from "vue";
import { Dialog } from "primevue";
import { useLocalization, useValidation } from "#imports";
import { Button, Inputs } from "#components";

const { localize: l } = useLocalization();
const { validate } = useValidation();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const { dialogOptions, inputs, submit, title, validateComposables = [], showValidationSummary = false } = schema;

const model = ref({});
const readyData = ref({});
const submitted = ref(false);
const visible = ref(false);

const ready = computed(() => Object.values(readyData.value).every(v => v) && isValid.value);

const { isValid, messages } = validate({
  inputs,
  model,
  composables: validateComposables
});

function onReady(value) {
  readyData.value = value;
}

function onChanged({ values }) {
  model.value = values;
}

function execute() {
  if(!ready.value) { return; }

  submitted.value = true;
  visible.value = false;
}

function emitSubmit() {
  if(submitted.value && ready.value) {
    submitted.value = false;
    emit("submit", model.value);
  }
}
</script>
