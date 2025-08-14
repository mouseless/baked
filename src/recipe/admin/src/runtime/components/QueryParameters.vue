<template>
  <!--
    [!NOTE]

    unlike usual model passing, `.model` is not enough here in below. for
    some reason vue rewraps the model which is already a ref, causing a
    double ref. that's why `.model.value` is passed instead of `.model`
  -->
  <Bake
    v-for="parameter in parameters"
    :key="parameter.name"
    v-model="values[parameter.name].model.value"
    :name="`query-parameters/${parameter.name}`"
    :descriptor="parameter.component"
  />
</template>
<script setup>
import { computed, ref, onMounted, watch, watchEffect } from "vue";
import { useRoute, useRouter } from "#app";
import { Bake } from "#components";
import { useContext, useDataFetcher } from "#imports";

const route = useRoute();
const router = useRouter();
const dataFetcher = useDataFetcher();
const context = useContext();

const { parameters } = defineProps({
  parameters: { type: Array, required: true }
});
const emit = defineEmits(["ready", "changed"]);

const injectedData = context.injectedData();
const values = {};
for(const parameter of parameters) {
  const query = computed(() => route.query[parameter.name]);
  const model = ref(query.value);

  values[parameter.name] = { query, model };
}

// set defaults when first landed on page
onMounted(async() => await setDefaults());

// Compute ready state and unique key in a single watchEffect
watchEffect(() => {
  const queryValues = Object.values(values).map(p => p.query.value);

  const isReady = parameters
    .filter(p => p.required)
    .every(p => values[p.name].query.value?.length > 0);

  const uniqueKey = queryValues
    .filter(v => v?.length > 0)
    .join("-");

  emit("ready", isReady);
  emit("changed", uniqueKey);
});

// Watch query parameters for changes and update models accordingly
watch(
  Object.values(values).map(p => p.query),
  async(newValues, oldValues) => {
    if(JSON.stringify(newValues) === JSON.stringify(oldValues)) { return; }

    await setDefaults();

    parameters.forEach((param, i) => {
      if(values[param.name]) {
        values[param.name].model.value = newValues[i] || undefined;
      }
    });
  },
  { deep: true }
);

// Watch model values for changes and update route query parameters
watch(
  Object.values(values).map(p => p.model),
  async(newValues, oldValues) => {
    if(JSON.stringify(newValues) === JSON.stringify(oldValues)) { return; }

    // Determine whether to push or replace based on required parameters with defaults
    const shouldReplace = parameters
      .filter(p => p.required && (p.default || p.defaultSelfManaged))
      .some(p => !values[p.name].query.value);

    const action = shouldReplace ? "replace" : "push";

    // Build query object from non-empty values
    const query = parameters.reduce((result, param, i) => {
      const value = newValues[i];
      if(value) {
        result[param.name] = value;
      }
      return result;
    }, {});

    await router[action]({
      path: route.path,
      query
    });
  },
  { deep: true }
);

async function setDefaults() {
  const query = { };
  for(const p of parameters) {
    const currentValue = values[p.name].query.value;

    // Only set value if it exists or parameter has a default
    if(currentValue || p.default) {
      if(!currentValue && p.default) {
        query[p.name] = await dataFetcher.fetch({ data: p.default, injectedData });
      } else {
        query[p.name] = currentValue;
      }
    }

    // Clean up null/empty values
    if(query[p.name] === null || query[p.name] === "") {
      query[p.name] = undefined;
    }
  }

  await router.replace({
    path: route.path,
    query
  });
}
</script>
