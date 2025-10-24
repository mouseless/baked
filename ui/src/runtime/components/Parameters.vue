<template>
  <div class="flex gap-2 max-md:flex-col max-md:min-w-24">
    <Bake
      v-for="parameter in parameters"
      :key="parameter.name"
      v-model="values[parameter.name]"
      :name="`parameters/${parameter.name}`"
      :descriptor="parameter.component"
      class="max-md:w-full"
    />
  </div>
</template>
<script setup>
import { onMounted, ref, watch, reactive } from "vue";
import { Bake } from "#components";
import { useContext, useDataFetcher } from "#imports";

const dataFetcher = useDataFetcher();
const context = useContext();

const { parameters } = defineProps({
  parameters: { type: Array, required: true }
});
const emit = defineEmits(["ready", "changed"]);

const injectedData = context.injectData();
const values = reactive({});

for(const parameter of parameters) {
  values[parameter.name] = ref(dataFetcher.get({ data: parameter.default, injectedData }));
}

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}

onMounted(async() => {
  for(const parameter of parameters) {
    if(!dataFetcher.shouldLoad(parameter.default?.type)) { continue; }

    values[parameter.name] = await dataFetcher.fetch({ data: parameter.default, injectedData });
  }

  emitChanged();
  emitReady();
});

// when any of the parameter values changed from input components, it emits
// ready and changed
watch(values, async() => {
  emitChanged();
  emitReady();
}, { deep: true });

function emitReady() {
  emit("ready",
    parameters
      .filter(p => p.required)
      .reduce((result, p) => result && checkValue(values[p.name]), true)
  );
}

function emitChanged() {
  emit("changed", {
    uniqueKey: parameters
      .map(p => values[p.name])
      .filter(checkValue)
      .join("-"),
    values
  });
}
</script>
