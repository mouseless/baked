<template>
  <Bake
    v-for="input in inputs"
    :key="input.name"
    v-model="models[input.name]"
    :name="`query-bound-inputs/${input.name}`"
    :descriptor="input.component"
  />
</template>
<script setup>
import { computed, onMounted, reactive, watch, watchEffect } from "vue";
import { useRoute, useRouter } from "#app";
import { Bake } from "#components";
import { useContext, useDataFetcher } from "#imports";

const context = useContext();
const dataFetcher = useDataFetcher();
const route = useRoute();
const router = useRouter();

const { inputs } = defineProps({
  inputs: { type: Array, required: true }
});
const emit = defineEmits(["ready", "changed"]);

const contextData = context.injectContextData();
const queries = {};
const models = reactive({});
for(const input of inputs) {
  queries[input.name] = computed(() => route.query[input.name]);
  models[input.name] = route.query[input.name];
}

// set defaults when first landed on page
onMounted(async() => await setDefaults());

// binds query params to models, needed when query parameters change due to a
// navigation from side menu or header etc.
watch(
  Object.values(queries),
  async(newValues, oldValues) => {
    if(JSON.stringify(newValues) === JSON.stringify(oldValues)) { return; }

    // sets defaults when already on the page, but query parameters changed due
    // to navigation clicks
    await setDefaults();

    inputs.forEach((input, i) => {
      models[input.name] = newValues[i] || undefined;
    });
  },
  { deep: true }
);

// when any of the input values changed from input components, it reroutes
// to set query param values
watch(
  models,
  async newValues => {
    // if any of required inputs that has default doesn't have a value, it
    // means it is currently setting default value so route should be replaced,
    // not pushed
    const shouldReplace = inputs
      .filter(input => input.required && (input.default || input.defaultSelfManaged))
      .some(input => !queries[input.name].value);

    // build query object from non-empty values
    const query = inputs.reduce((result, input) => {
      const value = newValues[input.name];
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

watchEffect(() => {
  // sets ready state when all required parameters are set
  const isReady = inputs
    .filter(i => i.required)
    .every(i => checkValue(queries[i.name].value));

  // calculates unique key to help parent redraw components when a parameter
  // value changes
  const uniqueKey = Object.values(queries).map(p => p.value)
    .filter(checkValue)
    .join("-");

  emit("ready", isReady);
  emit("changed", uniqueKey);
});

async function setDefaults() {
  const query = { };
  for(const input of inputs) {
    const currentValue = queries[input.name].value;

    // only set value if it exists or input has a default
    if(currentValue || input.default) {
      if(!currentValue && input.default) {
        query[input.name] = await dataFetcher.fetch({ data: input.default, contextData });
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

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}
</script>
