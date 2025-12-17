<template>
  <div>
    <template v-if="dialog">
      <Button
        :disabled="executing"
        :icon="buttonIcon"
        :loading="executing"
        :rounded="buttonRounded"
        :variant="buttonVariant"
        @click="visible = true"
      />
      <Dialog
        v-model:visible="visible"
        modal
        :style="{ width: '50rem' }"
        @after-hide="afterSubmit"
      >
        <Inputs
          v-if="inputs"
          :inputs="inputs"
          input-class="w-full"
          class="flex-col"
          @ready="onReady"
          @changed="onChanged"
        />
        <template #footer>
          <Button
            :disabled="executing"
            :label="l('Cancel')"
            @click="() => visible = false"
          />
          <Button
            :disabled="!ready"
            :label="l(buttonLabel)"
            @click="submit"
          />
        </template>
      </Dialog>
    </template>
    <template v-else>
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
          :icon="buttonIcon"
          :loading="executing"
          :disabled="!ready || executing"
          :label="l(buttonLabel)"
          @click="$emit('submit', formData)"
        />
      </div>
    </template>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Button, Dialog } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Inputs } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const formData = ref({});
const submitted = ref(false);

const { buttonIcon, buttonLabel, buttonRounded, buttonVariant, dialog, inputs } = schema;

const executing = context.injectExecuting();
const ready = ref(inputs.length === 0);
const visible = ref(false);

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  formData.value = values;
}

function submit() {
  submitted.value = true;
  visible.value = false;
}

function afterSubmit() {
  if(submitted.value) {
    submitted.value = false;
    emit("submit", formData.value);
  }
}
</script>