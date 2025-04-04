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

const { parameters } = defineProps({
  parameters: { type: Array, required: true }
});

const emit = defineEmits(["ready", "changed"]);

const route = useRoute();
const router = useRouter();
const dataFetcher = useDataFetcher();
const context = useContext();

const injectedData = context.injectedData();
const values = {};
for(const parameter of parameters) {
  const query = computed(() => route.query[parameter.name]);
  const model = ref(query.value);

  values[parameter.name] = { query, model };
}

// set defaults when first landed on page
onMounted(async() => await setDefaults());

// sets ready state when all required parameters are set
watchEffect(() => {
  emit("ready",
    parameters
      .filter(p => p.required)
      .reduce((result, p) => result && values[p.name].query.value?.length > 0, true)
  );
});

// calculates unique key to help parent redraw components when a parameter
// value changes
watchEffect(() => {
  emit("changed",
    Object.values(values)
      .map(p => p.query.value)
      .filter(v => v?.length > 0)
      .join("-")
  );
});

// binds query params to models, needed when query parameters change due to a
// navigation from side menu or header etc.
watch(Object.values(values).map(p => p.query), async newValues => {
  // sets defaults when already on the page, but query parameters changed due
  // to navigation clicks
  await setDefaults();

  for(let i = 0; i < newValues.length; i++) {
    values[parameters[i].name].model.value = newValues[i];
  }
});

// when any of the parameter values changed from input components, it reroutes
// to set query param values
watch(Object.values(values).map(p => p.model), async newValues => {
  // if any of required parameters that has default doesn't have a value, it
  // means it's setting default value and route should be replaced, not pushed
  const action = parameters
    .filter(p => p.required && p.default)
    .map(p => values[p.name].query)
    .every(q => q.value)
    ? "push"
    : "replace"
  ;
  
  await router[action]({
    path: route.path,
    query: Object.keys(values).reduce((result, name, i) => {
      if(newValues[i]) {
        result[name] = newValues[i];
      }

      return result;
    }, {})
  });
});

async function setDefaults() {
  if(parameters
    .filter(p => p.required)
    .map(p => values[p.name].query)
    .every(q => q.value)
  ) { return; }

  const query = { };
  for(const p of parameters ) {
    query[p.name] = values[p.name].query.value;
    
    if(!query[p.name] && p.default) {
      query[p.name] = await dataFetcher.fetch({ data: p.default, injectedData });
    }

    if(query[p.name] === null) {
      query[p.name] = undefined; // treat null as undefined to avoid empty query string parameters
    }
  }

  await router.replace({
    path: route.path,
    query
  });
}
</script>
