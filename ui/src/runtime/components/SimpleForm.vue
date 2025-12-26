<template>
  <div>
    <template v-if="dialogOptions">
      <Button
        :schema="dialogOptions.open"
        @click="visible = true"
      />
      <Dialog
        v-model:visible="visible"
        :header="l(title)"
        :style="{ width: 'min(700px, 90vw)' }"
        :pt="{ content: 'flex flex-col gap-8' }"
        closable
        modal
        dismissable-mask
        @after-hide="emitSubmit"
      >
        <div v-if="dialogOptions.message">
          {{ l(dialogOptions.message) }}
        </div>
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
      <div class="flex flex-col gap-8">
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
          :schema="submit"
          :ready
          @submit="$emit('submit', formData)"
        />
      </div>
    </template>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Dialog } from "primevue";
import { useLocalization } from "#imports";
import { Button, Inputs } from "#components";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const { dialogOptions, inputs, submit, title } = schema;

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

function emitSubmit() {
  if(submitted.value) {
    submitted.value = false;
    emit("submit", formData.value);
  }
}
</script>
