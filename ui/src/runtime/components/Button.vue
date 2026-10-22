<template>
  <Wrap
    :when="dialogOptions || validationSummary"
    as="div"
  >
    <Button
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
      @click="dialogOptions ? dialogRef.show($event) : $emit('submit')"
      @touchstart="validationSummaryRef?.show($event)"
      @mouseenter="validationSummaryRef?.show($event)"
      @mouseleave="validationSummaryRef?.hide()"
    />
    <ValidationSummaryPopover
      v-if="!ready && validationSummary && !validationSummary.disabled"
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
import { Button } from "primevue";
import { useContext, useLocalization } from "#imports";
import { ButtonDialog, ValidationSummaryPopover, Wrap } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true },
  ready: { type: Boolean, default: true },
  validationSummary: { type: null, default: null }
});
defineEmits(["submit"]);

const { icon, label, rounded, severity = "secondary", size, variant } = schema;
const dialogOptions = schema.dialogOptions && {
  mode: "Dialog",
  dismissableMask: true,
  ...schema.dialogOptions
};

const executing = context.injectExecuting();
const validationSummaryRef = ref();
const dialogRef = ref();
</script>