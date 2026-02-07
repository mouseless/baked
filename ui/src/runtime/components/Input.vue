<template>
  <Bake
    v-model="model"
    :name="schema.name"
    :descriptor="schema.component"
  />
</template>
<script setup>
import { computed, onMounted, watch } from "vue";
import { useRoute, useRouter } from "#app";
import { useContext, useDataFetcher } from "#imports";
import { Bake } from "#components";

const context = useContext();
const dataFetcher = useDataFetcher();
const route = useRoute();
const router = useRouter();

const { schema } = defineProps({
  schema: { type: Object, required: true }
});
const model = defineModel({ type: null, required: true });

const contextData = context.injectContextData();
let defaultValue = schema.default ? dataFetcher.get({ data: schema.default, contextData }) : undefined;
const query = schema.queryBound ? computed(() => route.query[schema.name]) : undefined;

onMounted(async() => {
  if(schema.default && dataFetcher.shouldLoad(schema.default)) {
    defaultValue = await dataFetcher.fetch({ data: schema.default, contextData });
  }

  // parent component might set model to null during setup, because of that on
  // mounted is used to set model value if it doesn't check
  if(!checkValue(model.value)) {
    if(schema.queryBound && checkValue(query.value)) {
      model.value = query.value;
    } else {
      await set(defaultValue);
    }
  }

  // to prevent watch callbacks run before setting query values to model, watch
  // hooks are added in on mounted
  if(schema.queryBound) {
    // when there are more than one inputs only last route push takes effect,
    // because of that it needs to watch for any query change
    watch(() => route.query, async newQuery => {
      const newValue = newQuery[schema.name];
      if(!checkValue(newValue) && schema.required && defaultValue) {
        await set(defaultValue);

        return;
      }

      model.value = newValue;
    }, { immediate: true });
  }

  watch(model, async newValue => {
    if(!checkValue(newValue)) {
      newValue = schema.required ? defaultValue : undefined;
    }

    await set(newValue);
  });
});

async function set(value) {
  if(schema.queryBound) {
    // prevents an unnecessary router push to avoid cancelation on other
    // inputs' router pushes
    if(value === query.value) { return; }

    await router.push({
      path: route.path,
      query: {
        ...route.query,
        [schema.name]: value
      },
      // prevents extra browser history when setting default value of input
      replace: schema.required && (schema.default || schema.defaultSelfManaged) && !query.value
    });
  } else {
    // prevents setting model to undefined infinitely
    if(!value) { return; }

    model.value = value;
  }
}

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}
</script>
