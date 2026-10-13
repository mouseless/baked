<template>
  <Button
    :disabled="!ready || executing"
    :loading="executing"
    :icon
    :label="l(label)"
    :rounded
    :severity
    :size
    :variant
    v-bind="$attrs"
    @click="clicked"
  />
  <Dialog
    v-model:visible="visible"
    :header="l(title)"
    :style="{ width: 'min(700px, 90vw)' }"
    :pt="{
      content: 'flex flex-col gap-8'
    }"
    closable
    modal
    :dismissable-mask
    :draggable="false"
    @after-hide="dialogHidden"
  >
    <Bake
      name="dialog-options/content"
      :descriptor="dialogOptions.form"
      :submit-target="`#${actionsId}`"
      @executed="formExecuted"
    />
    <template #footer>
      <Button
        :schema="dialogOptions.cancel"
        @submit="() => visible = false"
      />
      <div :id="actionsId" />
    </template>
  </Dialog>
</template>
<script setup>
import { useContext, useLocalization } from "#imports";
import { Button } from "primevue";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true },
  ready: { type: Boolean, default: true }
});
const emit = defineEmits(["submit"]);

const { icon, label, overlayOptions,rounded, severity = "secondary", size, variant } = schema;

const executing = context.injectExecuting();
const formSuccess = ref(false);
const overlayVisible = ref(false);

function clicked() {
  if(overlayOptions) {
    overlayVisible.value = true;
  } else {
    emit("submit");
  }
}

function formExecuted(success) {
  formSuccess.value = success;
  overlayVisible.value = false;
}

function dialogHidden() {
  if(!formSuccess.value) { return; }

  formSuccess.value = false;
  emit("submit");
}
</script>