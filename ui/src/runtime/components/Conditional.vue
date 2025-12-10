<template>
  <Bake
    :name="`${path}/${component.type}`"
    :descriptor="component"
  />
</template>
<script setup>
import { computed, useContext } from "#imports";
import { Bake } from "#components";

const context = useContext();

const { data, schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const path = context.injectPath();
const { conditions, fallback } = schema;
const component = computed(() =>find(conditions, data, fallback));

function find(conditions, data, fallback) {
  const comps = conditions.filter(condition => {
    console.log(data[condition.prop]);
    return data[condition.prop] === condition.value;
  });
  if(comps.length > 0) {
    return comps[0].component;
  }

  return fallback;
}
</script>