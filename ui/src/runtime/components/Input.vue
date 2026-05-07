<template>
  <Bake
    v-model="model"
    :name="schema.name"
    :descriptor="schema.component"
  />
</template>
<script setup>
import { computed, watch } from "vue";
import { useRoute, useRouter } from "#app";
import { useDataMounter } from "#imports";
import { Bake } from "#components";

const { mount: mountData, onAfterMount: onAfterMountData } = useDataMounter();
const route = useRoute();
const router = useRouter();

const { schema } = defineProps({
  schema: { type: Object, required: true }
});
const model = defineModel({ type: null, required: true });

const defaultValue = mountData(schema.default);
const query = schema.queryBound ? computed(() => route.query[schema.name]) : undefined;

onAfterMountData(async() => {
  // parent component might set model to null during setup, because of that on
  // mounted is used to set model value if it doesn't check
  if(!checkValue(model.value)) {
    if(schema.queryBound && checkValue(query.value)) {
      setModel(query.value);
    } else {
      await set(defaultValue.value);
    }
  }

  // to prevent watch callbacks run before setting query values to model, watch
  // hooks are added in on mounted
  if(schema.queryBound) {
    // when there are more than one inputs only last route push takes effect,
    // because of that it needs to watch for any query change
    watch(() => route.query, async newQuery => {
      const newValue = newQuery[schema.name];
      if(!checkValue(newValue) && schema.required && checkValue(defaultValue.value)) {
        await set(defaultValue.value);

        return;
      }

      setModel(newValue);
    }, { immediate: true });
  }

  watch(model, async newValue => {
    if(!checkValue(newValue)) {
      newValue = schema.required ? defaultValue.value : undefined;
    }

    await set(newValue);
  });
});

async function set(value) {
  if(!schema.queryBound) {
    // prevents setting model to undefined infinitely
    if(!checkValue(value)) { return; }

    setModel(value);
  }
  else {
    // prevents an unnecessary router push to avoid cancelation on other
    // inputs' router pushes
    if(String(value) === String(query.value)) { return; }

    await router.push({
      path: route.path,
      query: {
        ...route.query,
        [schema.name]: value
      },
      // prevents extra browser history when setting default value of input
      replace: schema.required && (schema.default || schema.defaultSelfManaged) && !query.value
    });
  }
}

function checkValue(value) {
  if(typeof value === "string") {
    return value !== "";
  }

  return value !== undefined && value !== null;
}

function setModel(value) {
  model.value = schema.numeric ? Number(value) : value;
}
</script>
