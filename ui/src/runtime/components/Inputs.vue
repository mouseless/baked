<template>
  <Input
    v-for="input in inputs"
    :key="input.name"
    v-model="values[input.name]"
    :schema="input"
    :class="inputClass"
  />
</template>
<script setup>
import { onMounted, watch, reactive } from "vue";
import { useContext } from "#imports";
import { Input } from "#components";

const context = useContext();
const route = useRoute();

const { inputs } = defineProps({
  inputs: { type: Array, required: true },
  inputClass: { type: String, default: "" }
});
const emit = defineEmits(["ready", "changed"]);

const parentPath = context.injectPath();
const values = reactive({});

context.providePath(`${parentPath}/inputs`);

watch(values, async() => {
  emitChanged();
  emitReady();
}, { deep: true });

if(inputs.some(i => i.queryBound)) {
  watch(route, async() => {
    emitChanged();
    emitReady();
  });
}

onMounted(async() => {
  emitChanged();
  emitReady();
});

function emitReady() {
  emit("ready",
    inputs
      .filter(i => i.required)
      .reduce((result, i) => result && checkValue(values[i.name], i), true)
  );
}

function emitChanged() {
  emit("changed", {
    uniqueKey: inputs
      .filter(i => checkValue(values[i.name], i))
      .map(i => values[i.name])
      .join("-"),
    values
  });
}

function checkValue(value, input) {
  if(input.queryBound && route.query[input.name] !== `${value}`) {
    return false;
  }

  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}
</script>
