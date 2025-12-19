<template>
  <div class="flex flex-col gap-8">
    <div class="flex flex-col gap-4">
      <Inputs
        v-if="inputs"
        :inputs="inputs"
        input-class="w-full"
        @ready="onReady"
        @changed="onChanged"
      />
    </div>
    <Button
      :icon="buttonIcon"
      :loading="executing"
      :disabled="!ready || executing"
      :label="l(buttonLabel)"
      @click="$emit('submit', formData)"
    />
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Button } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Inputs } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
defineEmits(["submit"]);

const { buttonIcon, buttonLabel, inputs } = schema;

const executing = context.injectExecuting();
const formData = ref({});
const ready = ref(inputs.length === 0);

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  formData.value = values;
}
</script>
