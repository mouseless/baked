<template>
  <div>
    <template v-if="dialogTemplate">
      <Button
        :disabled="executing"
        :schema="dialogTemplate.toggleButton"
        @click="visible = true"
      />
      <Dialog
        v-model:visible="visible"
        closable
        :header="l(name)"
        modal
        :style="{ width: 'min(700px, 90vw)' }"
        @after-hide="submit"
      >
        <div class="flex flex-col gap-4">
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
            :disabled="executing"
            :schema="dialogTemplate.cancelButton"
            @submit="() => visible = false"
          />
          <Button
            :disabled="!ready"
            :schema="submitButton"
            @submit="execute"
          />
        </template>
      </Dialog>
    </template>
    <template v-else>
      <div class="flex flex-col gap-8">
        <h1 class="font-bold text-xl truncate">
          {{ l(name) }}
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
          :disabled="!ready || executing"
          :schema="submitButton"
          @submit="$emit('submit', formData)"
        />
      </div>
    </template>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Dialog } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Button, Inputs } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const { dialogTemplate, inputs, name, submitButton } = schema;

const executing = context.injectExecuting();
const formData = ref({});
const ready = ref(inputs.length === 0);
const submitted = ref(false);
const visible = ref(false);

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  formData.value = values;
}

function execute() {
  submitted.value = true;
  visible.value = false;
}

function submit() {
  if(submitted.value) {
    submitted.value = false;
    emit("submit", formData.value);
  }
}
</script>