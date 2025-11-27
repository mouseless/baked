<template>
  <div>
    <slot
      v-if="$slots.loading && loading"
      name="loading"
    />
    <Skeleton
      v-else-if="loading"
      v-bind="skeleton"
    />
    <slot
      v-else
    />
  </div>
</template>
<script setup>
import { useContext } from "#imports";
import { Skeleton } from "primevue";

const context = useContext();

const { skeleton } = defineProps({
  skeleton: { type: Object, required: false, default: () => { } }
});

const loading = context.injectLoading();
</script>