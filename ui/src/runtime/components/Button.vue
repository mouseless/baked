<template>
  <Wrap :when="dialogOptions">
    <PrimeButton
      :disabled="!ready || executing"
      :loading="executing"
      :icon
      :label="l(label)"
      :rounded
      :severity
      :size
      :variant
      @click="clicked"
    />
    <Dialog
      v-if="dialogOptions"
      v-model:visible="dialogVisible"
      :header="l(dialogOptions.header)"
      :style="{ width: 'min(700px, 90vw)' }"
      :pt="{
        content: 'flex flex-col gap-8'
      }"
      closable
      modal
      :dismissable-mask="dialogOptions.dismissableMask"
      :draggable="false"
      @after-hide="dialogHidden"
    >
      <Bake
        v-if="dialogOptions.content"
        name="dialog-options/content"
        :descriptor="dialogOptions.content"
      />
      <Bake
        v-if="dialogOptions.form"
        name="dialog-options/form"
        :descriptor="dialogOptions.form"
        :submit-target="`#${submitId}`"
      />
      <template #footer>
        <Button
          v-if="dialogOptions.cancel"
          :schema="dialogOptions.cancel"
          class="w-full"
          @submit="() => dialogVisible = false"
        />
        <div
          v-if="dialogOptions.form"
          :id="submitId"
          class="w-full [&_.p-button]:w-full"
        />
        <Button
          v-if="dialogOptions.submit"
          :schema="dialogOptions.submit"
          class="w-full"
          @submit="submitClicked"
        />
      </template>
    </Dialog>
  </Wrap>
</template>
<script setup>
import { ref, useId } from "vue";
import { Button as PrimeButton, Dialog } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Wrap } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const submitId = useId();

const { schema } = defineProps({
  schema: { type: null, required: true },
  ready: { type: Boolean, default: true }
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
  dismissableMask: true,
  ...schema.dialogOptions
};

const executing = context.injectExecuting();
const events = context.injectEvents();
const path = context.injectPath();
const formSuccess = ref(false);
const dialogVisible = ref(false);

function clicked() {
  if(dialogOptions) {
    dialogVisible.value = true;
    events.on("close-dialog", path, formExecuted);
  } else {
    emit("submit");
  }
}

function formExecuted() {
  events.off("close-dialog", path);
  formSuccess.value = true;
  dialogVisible.value = false;
}

function submitClicked() {
  formSuccess.value = true;
  dialogVisible.value = false;
}

function dialogHidden() {
  if(!formSuccess.value) { return; }

  formSuccess.value = false;
  emit("submit");
}
</script>