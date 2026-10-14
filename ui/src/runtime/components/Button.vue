<template>
  <Wrap
    :when="dialogOptions"
    as="div"
    class="b-button-wrapper"
  >
    <PrimeVueButon
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
    />
    <Dialog
      v-if="dialogOptions?.mode == 'Dialog'"
      v-model:visible="dialogVisible"
      :header="l(dialogOptions.header)"
      :style="{ width: 'min(700px, 90vw)' }"
      :pt="{ content: 'flex flex-col gap-8' }"
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
    <ConfirmPopup
      v-if="dialogOptions?.mode == 'Confirm'"
      :group="confirmId"
      :pt="{ transition: { onAfterLeave: dialogHidden } }"
    >
      <template #container="{ acceptCallback, rejectCallback }">
        <div class="flex flex-col text-sm min-w-64 [&_input]:text-sm">
          <Bake
            v-if="dialogOptions.content"
            name="dialog-options/content"
            :descriptor="dialogOptions.content"
            class="p-confirmpopup-content"
          />
          <Bake
            v-if="dialogOptions.form"
            name="dialog-options/form"
            :descriptor="dialogOptions.form"
            :submit-target="`#${submitId}`"
            class="p-confirmpopup-content "
          />
          <div class="p-confirmpopup-footer [&_.p-button]:py-1 [&_.p-button]:text-sm">
            <Button
              v-if="dialogOptions.cancel"
              :schema="dialogOptions.cancel"
              class="w-full"
              @submit="rejectCallback"
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
              @submit="acceptCallback"
            />
          </div>
        </div>
      </template>
    </ConfirmPopup>
  </Wrap>
</template>
<script setup>
import { ref, useId } from "vue";
import { Button as PrimeVueButon, ConfirmPopup, Dialog, useConfirm } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Wrap } from "#components";

const confirm = useConfirm();
const context = useContext();
const { localize: l } = useLocalization();
const submitId = useId();
const confirmId = useId();

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
  mode: "Dialog",
  dismissableMask: true,
  ...schema.dialogOptions
};

const executing = context.injectExecuting();
const events = context.injectEvents();
const formSuccess = ref(false);
const dialogVisible = ref(false);

function clicked(event) {
  if(!dialogOptions) {
    emit("submit");

    return;
  }

  events.on("close-dialog", confirmId, submitClicked);

  if(dialogOptions.mode === "Confirm") {
    confirm.require({
      target: event.currentTarget,
      group: confirmId,
      accept: submitClicked
    });
  } else {
    dialogVisible.value = true;
  }
}

function submitClicked() {
  formSuccess.value = true;

  if(dialogOptions.mode === "Confirm") {
    confirm.close();
  } else {
    dialogVisible.value = false;
  }
}

function dialogHidden() {
  events.off("close-dialog", confirmId);
  if(!formSuccess.value) { return; }

  formSuccess.value = false;
  emit("submit");
}
</script>