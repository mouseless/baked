<template>
  <!--
    [!NOTE]

    unlike the usual way to pass model, `.model` is not enough here in below.
    for some reason vue rewraps the model which is already a ref, causing a
    double ref. that's why `.model.value` is passed instead of `.model`
  -->
  <div class="flex gap-2 max-md:flex-col max-md:min-w-24">
    <Bake
      v-for="parameter in parameters"
      :key="parameter.name"
      v-model="values[parameter.name].value"
      :name="`parameters/${parameter.name}`"
      :descriptor="parameter.component"
      class="max-md:w-full"
    />
  </div>
</template>
<script setup>
import { onMounted, ref, watch } from "vue";
import { Bake } from "#components";
import { useContext, useDataFetcher } from "#imports";

const dataFetcher = useDataFetcher();
const context = useContext();

const { parameters } = defineProps({
  parameters: { type: Array, required: true }
});
const emit = defineEmits(["ready", "changed"]);

const injectedData = context.injectedData();
const values = {};
for(const parameter of parameters) {
  values[parameter.name] = ref(dataFetcher.get({ data: parameter.default, injectedData }));
}

onMounted(async() => {
  for(const parameter of parameters) {
    if(!dataFetcher.shouldLoad(parameter.default?.type)) { continue; }

    values[parameter.name].value = await dataFetcher.fetch({ data: parameter.default, injectedData });
  }
});

// when any of the parameter values changed from input components, it emits
// ready and changed
watch(Object.values(values), async() => {
  emitChanged();
  emitReady();
}, { immediate: true });

function emitReady() {
  emit("ready",
    parameters
      .filter(p => p.required)
      .reduce((result, p) => result && values[p.name].value?.length > 0, true)
  );
}

function emitChanged() {
  emit("changed", {
    uniqueKey: Object
      .values(values)
      .map(v => v.value)
      .filter(v => v?.length > 0)
      .join("-"),
    values
  });
}
</script>
