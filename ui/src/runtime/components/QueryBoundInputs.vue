<template>
  <!--
    [!NOTE]

    unlike usual model passing, `.model` is not enough here in below. for
    some reason vue rewraps the model which is already a ref, causing a
    double ref. that's why `.model.value` is passed instead of `.model`
  -->
  <Bake
    v-for="input in inputs"
    :key="input.name"
    v-model="values[input.name].model.value"
    :name="`query-bound-inputs/${input.name}`"
    :descriptor="input.component"
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

const { inputs } = defineProps({
  inputs: { type: Array, required: true }
});
const emit = defineEmits(["ready", "changed"]);

const injectedData = context.injectData();
const values = {};
for(const input of inputs) {
  const query = computed(() => route.query[input.name]);
  const model = ref(query.value);

  values[input.name] = { query, model };
}

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}

// set defaults when first landed on page
onMounted(async() => await setDefaults());

watchEffect(() => {
  const queryValues = Object.values(values).map(p => p.query.value);

  // sets ready state when all required parameters are set
  const isReady = inputs
    .filter(i => i.required)
    .every(i => checkValue(values[i.name].query.value));

  // calculates unique key to help parent redraw components when a parameter
  // value changes
  const uniqueKey = queryValues
    .filter(checkValue)
    .join("-");

  emit("ready", isReady);
  emit("changed", uniqueKey);
});

// binds query params to models, needed when query parameters change due to a
// navigation from side menu or header etc.
watch(
  Object.values(values).map(p => p.query),
  async(newValues, oldValues) => {
    if(JSON.stringify(newValues) === JSON.stringify(oldValues)) { return; }

    // sets defaults when already on the page, but query parameters changed due
    // to navigation clicks
    await setDefaults();

    inputs.forEach((input, i) => {
      if(!values[input.name]) { return; }

      values[input.name].model.value = newValues[i] || undefined;
    });
  },
  { deep: true }
);

// when any of the input values changed from input components, it reroutes
// to set query param values
watch(
  Object.values(values).map(p => p.model),
  async(newValues, oldValues) => {
    if(JSON.stringify(newValues) === JSON.stringify(oldValues)) { return; }

    // if any of required inputs that has default doesn't have a value, it
    // means it is currently setting default value so route should be replaced,
    // not pushed
    const shouldReplace = inputs
      .filter(input => input.required && (input.default || input.defaultSelfManaged))
      .some(input => !values[input.name].query.value);

    // build query object from non-empty values
    const query = inputs.reduce((result, input, i) => {
      const value = newValues[i];
      if(checkValue(value)) {
        result[input.name] = value;
      }

      return result;
    }, {});

    await router[shouldReplace ? "replace" : "push"]({
      path: route.path,
      query
    });
  },
  { deep: true }
);

async function setDefaults() {
  const query = { };
  for(const input of inputs) {
    const currentValue = values[input.name].query.value;

    // only set value if it exists or input has a default
    if(currentValue || input.default) {
      if(!currentValue && input.default) {
        query[input.name] = await dataFetcher.fetch({ data: input.default, injectedData });
      } else {
        query[input.name] = currentValue;
      }
    }

    // clean up null/empty values to avoid empty query string parameters in the
    // address bar
    if(query[input.name] === null || query[input.name] === "") {
      query[input.name] = undefined;
    }
  }

  await router.replace({
    path: route.path,
    query
  });
}
</script>
