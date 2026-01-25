<template>
  <Input
    v-for="input in inputs"
    :key="input.name"
    v-model="models[input.name]"
    :schema="input"
    :class="inputClass"
  />
</template>
<script setup>
import { computed, reactive, watch } from "vue";
import { useContext, useRoute } from "#imports";
import { Input } from "#components";

const context = useContext();
const route = useRoute();

const { inputs } = defineProps({
  inputs: { type: Array, required: true },
  inputClass: { type: String, default: "" }
});
const emit = defineEmits(["ready", "changed"]);

const parentPath = context.injectPath();
const models = reactive({});
const values = computed(() =>
  inputs.reduce((result, input) => {
    result[input.name] = getValue(input);

    return result;
  }, {})
);

context.providePath(`${parentPath}/inputs`);

watch(values, newValues => {
  if(inputs
    .filter(i => i.default || i.defaultSelfManaged)
    .some(i => !checkValue(newValues[i.name]))
  ) { return; }

  emitChanged();
  emitReady();
}, { immediate: true });

function emitReady() {
  const ready = inputs
    .filter(i => i.required)
    .map(getValue)
    .reduce((result, value) => result && checkValue(value), true);

  emit("ready", ready);
}

function emitChanged() {
  const uniqueKey = inputs
    .map(getValue)
    .filter(checkValue)
    .join("-");

  emit("changed", {
    uniqueKey,
    values: values.value
  });
}

function checkValue(value) {
  if(typeof value === "string") {
    return (value !== "");
  } else {
    return (value !== undefined && value !== null);
  }
}

function getValue(input) {
  if(input.queryBound) {
    return route.query[input.name];
  } else {
    return models[input.name];
  }
}
</script>
