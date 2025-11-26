<template>
  <slot
    v-if="$slots.loading && loading"
    name="loading"
  />
  <Skeleton
    v-else-if="loading"
    :height="skeleton?.height"
    :width="skeleton?.width"
  />
  <slot
    v-else-if="when"
    name="content"
  />
  <slot
    v-else-if="$slots.fallback"
    name="fallback"
  />
  <span
    v-else
  >-</span>
</template>
<script setup>
import { useContext } from "#imports";
import { Skeleton } from "primevue";

const context = useContext();

const { skeleton, when } = defineProps({
  skeleton: { type: Object, required: false, default: () => { } },
  when: { type: [Object, Boolean], required: false, default: true }
});

const loading = context.injectLoading();
</script>