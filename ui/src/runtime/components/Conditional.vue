<template>
  <AwaitLoading>
    <Bake
      v-if="data"
      :name="`${path}/${component.type}`"
      :descriptor="component"
    />
  </AwaitLoading>
</template>
<script setup>
import { computed, useContext } from "#imports";
import { AwaitLoading, Bake } from "#components";

const context = useContext();

const { data, schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const path = context.injectPath();
const { conditions, fallback } = schema;

const component = computed(() => {
  if(!conditions) { return fallback; }

  const successConditions = conditions.filter(condition => condition.prop && data[condition.prop] === condition.value);
  if(successConditions.length <= 0) { return fallback; }

  return successConditions[0].component;
});
</script>
