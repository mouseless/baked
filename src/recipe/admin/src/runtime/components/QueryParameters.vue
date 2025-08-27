<template>
  <Button
    variant="text"
    icon="pi pi-filter"
    class="lg:hidden"
    rounded
    @click="togglePopover"
  />
  <Popover ref="isPopoverVisible">
    <div
      v-if="isPopoverVisible"
      class="flex flex-col gap-4 flex-start justify-between w-full"
    >
      <Bake
        v-for="parameter in parameters"
        :key="parameter.name"
        v-model="values[parameter.name].model.value"
        :name="`query-parameters/${parameter.name}`"
        :descriptor="parameter.component"
        class="w-full text-sm"
      />
    </div>
  </Popover>
  <Bake
    v-for="parameter in parameters"
    :key="parameter.name"
    v-model="values[parameter.name].model.value"
    :name="`query-parameters/${parameter.name}`"
    :descriptor="parameter.component"
    class="max-lg:hidden flex flex-row"
  />
</template>
<script setup>
import { computed, ref, onMounted, watch, watchEffect } from "vue";
import Button from "primevue/button";
import Popover from "primevue/popover";
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

const isPopoverVisible = ref();

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

watchEffect(() => {
  const queryValues = Object.values(values).map(p => p.query.value);

  // sets ready state when all required parameters are set
  const isReady = parameters
    .filter(p => p.required)
    .every(p => values[p.name].query.value?.length > 0);

  // calculates unique key to help parent redraw components when a parameter
  // value changes
  const uniqueKey = queryValues
    .filter(v => v?.length > 0)
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

    parameters.forEach((param, i) => {
      if(!values[param.name]) { return; }

      values[param.name].model.value = newValues[i] || undefined;
    });
  },
  { deep: true }
);

// when any of the parameter values changed from input components, it reroutes
// to set query param values
watch(
  Object.values(values).map(p => p.model),
  async(newValues, oldValues) => {
    if(JSON.stringify(newValues) === JSON.stringify(oldValues)) { return; }

    // if any of required parameters that has default doesn't have a value, it
    // means it is currently setting default value so route should be replaced,
    // not pushed
    const shouldReplace = parameters
      .filter(p => p.required && (p.default || p.defaultSelfManaged))
      .some(p => !values[p.name].query.value);

    // build query object from non-empty values
    const query = parameters.reduce((result, param, i) => {
      const value = newValues[i];
      if(value) {
        result[param.name] = value;
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
  for(const p of parameters) {
    const currentValue = values[p.name].query.value;

    // only set value if it exists or parameter has a default
    if(currentValue || p.default) {
      if(!currentValue && p.default) {
        query[p.name] = await dataFetcher.fetch({ data: p.default, injectedData });
      } else {
        query[p.name] = currentValue;
      }
    }

    // clean up null/empty values to avoid empty query string parameters in the
    // address bar
    if(query[p.name] === null || query[p.name] === "") {
      query[p.name] = undefined;
    }
  }

  await router.replace({
    path: route.path,
    query
  });
}

function togglePopover(event) {
  isPopoverVisible.value.toggle(event);
}

</script>
