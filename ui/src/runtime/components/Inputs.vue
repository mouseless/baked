<template>
  <Input
    v-for="input in inputs"
    :key="input.name"
    v-model="models[input.name]"
    :schema="input"
    :class="inputClass"
    :invalid="setInvalid(input.name)"
    :validation="getValidation(input.name)"
    @blur.prevent="setBlur(input.name)"
  />
</template>
<script setup>
import { computed, reactive, ref, watch } from "vue";
import { useContext, useRoute } from "#imports";
import { Input } from "#components";

const context = useContext();
const route = useRoute();

const { inputs, validator } = defineProps({
  inputs: { type: Array, required: true },
  inputClass: { type: String, default: "" },
  validator: { type: Object, default: () => ({}) }
});
const emit = defineEmits(["ready", "changed"]);

const parentPath = context.injectPath();

const inputEvents = ref({});
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

function setBlur(key) {
  inputEvents.value[key] = {
    blur: true
  };
}

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  } else {
    return value !== undefined && value !== null;
  }
}

function getValue(input) {
  if(input.queryBound) {
    return route.query[input.name];
  } else {
    return models[input.name];
  }
}

function getValidation(input) {
  return validator ? validator[input] : false;
}

function setInvalid(input) {
  // check it, test it
  if(!Object.values(validator).length) { return false; }

  return !validator[input].valid && inputEvents.value[input]?.blur || false;
}
</script>
