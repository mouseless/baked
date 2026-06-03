<template>
  <Validation>
    <InputNumber
      v-if="number"
      v-model="model"
      v-bind="$attrs"
      :name="testId"
      :data-testid="testId"
      :placeholder="testId"
      class="w-32"
      @input="onInput"
    />
    <InputText
      v-else
      v-model="model"
      v-bind="$attrs"
      :name="testId"
      :data-testid="testId"
      :placeholder="testId"
      class="w-32"
    />
  </Validation>
</template>
<script setup>
import { onMounted, watch } from "vue";
import { InputNumber, InputText } from "primevue";
import { useValidation } from "#imports";
import { Validation } from "#components";

const validation = useValidation();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { testId, defaultValue, number, restrictedValue } = schema;

const mutableValidation = validation.injectMutable();

watch(model, newValue => {
  if(newValue === "" && !defaultValue) {
    model.value = null;

    return;
  }

  if(newValue === null || newValue === undefined) {
    model.value = newValue = defaultValue;
  }

  if(restrictedValue) {
    if(newValue === restrictedValue) {
      mutableValidation?.setError(`${restrictedValue} is restricted`);
    } else {
      mutableValidation?.clear();
    }
  }
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