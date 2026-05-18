<template>
  <Labeler
    :label
    :path
    :mode="labelMode"
    :variant="labelVariant"
    :validate-label
  >
    <InputText
      v-model="model"
      :name="testId"
      :data-testid="testId"
      :placeholder="testId"
      class="w-32"
      @input="onInput"
    />
    <Message
      v-show="validator.message && validator.persist"
      :severity="validator.severity"
      variant="simple"
      size="small"
      class="ml-2"
    >
      {{ validator.message || "" }}
    </Message>
  </Labeler>
</template>
<script setup>
import { onMounted, watch } from "vue";
import { InputText, Message } from "primevue";
import { Labeler } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { testId, defaultValue, labeler, validator } = schema;
const { label, path, labelMode, labelVariant } = labeler;

watch(model, newValue => {
  if(newValue !== undefined && newValue !== null) { return; }
  if(newValue === defaultValue) { return; }

  model.value = defaultValue;
});

onMounted(() => {
  if(!model.value) {
    model.value = defaultValue;
  }
});

function onInput(event) {
  model.value = event.value;
}
</script>
