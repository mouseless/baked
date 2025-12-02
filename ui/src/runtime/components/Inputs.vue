<template>
  <div class="flex gap-2 max-md:flex-col max-md:min-w-24">
    <Bake
      v-for="input in inputs"
      :key="input.name"
      v-model="values[input.name]"
      :name="`inputs/${input.name}`"
      :descriptor="input.component"
      class="max-md:w-full"
      :class="inputClass"
    />
  </div>
</template>
<script setup>
import { onMounted, ref, watch, reactive } from "vue";
import { Bake } from "#components";
import { useContext, useDataFetcher } from "#imports";

const dataFetcher = useDataFetcher();
const context = useContext();

const { inputs } = defineProps({
  inputs: { type: Array, required: true },
  inputClass: { type: String, default: "" }
});
const emit = defineEmits(["ready", "changed"]);

const injectedData = context.injectData();
const values = reactive({});

for(const input of inputs) {
  values[input.name] = ref(dataFetcher.get({ data: input.default, contextData: injectedData }));
}

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}

onMounted(async() => {
  for(const input of inputs) {
    if(!dataFetcher.shouldLoad(input.default?.type)) { continue; }

    values[input.name] = await dataFetcher.fetch({ data: input.default, contextData: injectedData });
  }

  emitChanged();
  emitReady();
});

// when any of the inputs values changed from input components, it emits
// ready and changed
watch(values, async() => {
  emitChanged();
  emitReady();
}, { deep: true });

function emitReady() {
  emit("ready",
    inputs
      .filter(i => i.required)
      .reduce((result, i) => result && checkValue(values[i.name]), true)
  );
}

function emitChanged() {
  emit("changed", {
    uniqueKey: inputs
      .map(i => values[i.name])
      .filter(checkValue)
      .join("-"),
    values
  });
}
</script>
