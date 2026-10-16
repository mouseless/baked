<template>
  <Wrap
    :when="dialogOptions || validationSummary"
    as="div"
    class="b-button-wrapper"
  >
    <PrimeVueButon
      ref="buttonRef"
      :disabled="!ready || executing"
      :loading="executing"
      :icon
      :label="l(label)"
      :rounded
      :severity
      :size
      :variant
      class="text-nowrap"
      @click="clicked"
      @touchstart="showValidationSummary"
      @mouseenter="showValidationSummary"
      @mouseleave="hideValidationSummary"
    />
    <ValidationSummaryPopover
      v-if="validationSummary && !validationSummary.disabled"
      ref="validationSummaryRef"
      :messages="validationSummary.messages"
    />
    <ButtonDialog
      v-if="dialogOptions"
      ref="dialogRef"
      name="dialog-options"
      class="b-Button--dialog-options"
      v-bind="dialogOptions"
      @submit="$emit('submit')"
    />
  </Wrap>
</template>
<script setup>
import { ref } from "vue";
import { Button as PrimeVueButon } from "primevue";
import { useContext, useLocalization } from "#imports";
import { ButtonDialog, ValidationSummaryPopover, Wrap } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema, ready } = defineProps({
  schema: { type: null, required: true },
  ready: { type: Boolean, default: true },
  validationSummary: { type: null, default: null }
});
const emit = defineEmits(["submit"]);

const {
  icon,
  label,
  rounded,
  severity = "secondary",
  size,
  variant
} = schema;
const dialogOptions = schema.dialogOptions && {
  mode: "Dialog",
  dismissableMask: true,
  ...schema.dialogOptions
};

const executing = context.injectExecuting();
const validationSummaryRef = ref();
const dialogRef = ref();

function showValidationSummary(event) {
  if(ready) { return; }

  validationSummaryRef.value?.show(event);
}

function hideValidationSummary() {
  validationSummaryRef.value?.hide();
}

function clicked(event) {
  if(!dialogOptions) {
    emit("submit");

    return;
  }

  dialogRef.value.show(event);
}
</script>