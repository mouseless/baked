<template>
  <Dialog
    v-if="mode == 'Dialog'"
    v-bind="$attrs"
    v-model:visible="dialogVisible"
    :header="l(header)"
    class="b-Button--dialog"
    :style="{ width: 'min(700px, 90vw)' }"
    :pt="{ content: 'flex flex-col gap-8' }"
    closable
    modal
    :dismissable-mask="dismissableMask"
    :draggable="false"
    @after-hide="dialogHidden"
  >
    <Bake
      v-if="content"
      :name="`${name}/content`"
      :descriptor="content"
    />
    <Bake
      v-if="form"
      :name="`${name}/form`"
      :descriptor="form"
      :submit-target="`#${submitId}`"
    />
    <template #footer>
      <Button
        v-if="cancel"
        :schema="cancel"
        class="w-full"
        @submit="() => dialogVisible = false"
      />
      <div
        v-if="form"
        :id="submitId"
        class="w-full [&_.p-button]:w-full"
      />
      <Button
        v-if="submit"
        :schema="submit"
        class="w-full"
        @submit="submitClicked"
      />
    </template>
  </Dialog>
  <ConfirmPopup
    v-if="mode == 'Confirm'"
    :group="confirmId"
    :pt="{ transition: { onAfterLeave: dialogHidden } }"
  >
    <template #container="{ acceptCallback, rejectCallback }">
      <div
        v-bind="$attrs"
        class="b-Button--confirm flex flex-col text-sm min-w-64 max-w-80 [&_input]:text-sm"
      >
        <Bake
          v-if="content"
          :name="`${name}/content`"
          :descriptor="content"
          class="p-confirmpopup-content"
        />
        <Bake
          v-if="form"
          :name="`${name}/form`"
          :descriptor="form"
          :submit-target="`#${submitId}`"
          class="p-confirmpopup-content"
        />
        <div class="p-confirmpopup-footer [&_.p-button]:py-1 [&_.p-button]:text-sm">
          <Button
            v-if="cancel"
            :schema="cancel"
            class="w-full"
            @submit="rejectCallback"
          />
          <div
            v-if="form"
            :id="submitId"
            class="w-full [&_.p-button]:w-full"
          />
          <Button
            v-if="submit"
            :schema="submit"
            class="w-full"
            @submit="acceptCallback"
          />
        </div>
      </div>
    </template>
  </ConfirmPopup>
</template>
<script setup>
import { ref, useId } from "vue";
import { ConfirmPopup, Dialog, useConfirm } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Bake, Button } from "#components";

const confirm = useConfirm();
const context = useContext();
const submitId = useId();
const confirmId = useId();
const { localize: l } = useLocalization();

const { mode } = defineProps({
  name: { type: String, required: true },
  mode: { type: String, default: "Dialog" },
  header: { type: String, default: "" },
  dismissableMask: { type: Boolean, default: true },
  content: { type: null, default: null },
  form: { type: null, default: null },
  submit: { type: null, default: null },
  cancel: { type: null, default: null }
});
defineExpose({ show });
const emit = defineEmits(["submit"]);

const events = context.injectEvents();
const formSuccess = ref(false);
const dialogVisible = ref(false);

function show(event) {
  events.on("close-dialog", confirmId, submitClicked);

  if(mode === "Confirm") {
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

  if(mode === "Confirm") {
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