<template>
  <template v-if="dialogOptions">
    <Button
      ref="submitRef"
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
          form-mode
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
          v-tooltip.top="{
            disabled: !showValidationSummary,
            value: messages,
            pt: { text: 'text-sm' }
          }"
          :schema="submit"
          :ready
          @submit="execute"
        />
      </template>
    </Dialog>
    <ErrorPopover ref="errorPopoverRef">
      <InlineError :error />
    </ErrorPopover>
  </template>
  <template v-else>
    <div
      v-bind="$attrs"
      class="flex flex-col gap-4"
      :class="[
        { 'gap-8': !horizontal },
        { 'horizontal': horizontal }
      ]"
    >
      <h2
        v-if="alwaysShowTitle"
        class="font-bold text-xl truncate"
      >
        {{ l(title) }}
      </h2>
      <div
        class="flex gap-4"
        :class="{ 'flex-col': !horizontal }"
      >
        <Inputs
          v-if="inputs"
          :inputs
          form-mode
          input-class="w-full"
          @ready="onReady"
          @changed="onChanged"
        />
        <Button
          ref="submitRef"
          v-tooltip.top="{
            disabled: !showValidationSummary,
            value: messages,
            pt: { text: 'text-sm' }
          }"
          :schema="submit"
          :ready
          class="min-w-min"
          :class="[
            { 'mt-4': !horizontal && inputs.length },
            { 'text-nowrap self-start': horizontal }
          ]"
          @submit="$emit('submit', model)"
        />
        <ErrorPopover ref="errorPopoverRef">
          <InlineError :error />
        </ErrorPopover>
      </div>
    </div>
  </template>
</template>
<script setup>
import { computed, ref, watch } from "vue";
import { Dialog } from "primevue";
import { useBakeError, useLocalization, useValidation } from "#imports";
import { Button, ErrorPopover, InlineError, Inputs } from "#components";

const { handle: handleError } = useBakeError();
const { localize: l } = useLocalization();
const { validate } = useValidation();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const { alwaysShowTitle, dialogOptions, inputs, horizontal, submit, title, validations = [], showValidationSummary } = schema;

const model = ref({});
const readyData = ref({});
const submitted = ref(false);
const visible = ref(false);
const submitRef = ref();
const errorPopoverRef = ref();
const { error } = handleError();

const ready = computed(() => Object.values(readyData.value).every(v => v) && isValid.value);

const { isValid, messages } = validate({
  inputs,
  model,
  composables: validations
});

watch(error, newError => {
  if(newError) {
    errorPopoverRef.value.show(submitRef);
  } else {
    errorPopoverRef.value.hide();
  }
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
<style scoped>
.horizontal {
  :has(.p-iftalabel) {
    .p-button[data-p="large"] {
      @apply mt-1;
    }

    .p-button:not([data-p="large"]) {
      @apply mt-2;
    }
  }
}
</style>