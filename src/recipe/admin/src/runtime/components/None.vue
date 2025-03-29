<template>
  <span class="bg-red-500 p-1 rounded-md">
    <Skeleton
      v-if="loading"
      width="3rem"
      class="inline-block"
    />
    <code v-else>
      {{ json }}
    </code>
  </span>
</template>
<script setup>
import { computed, defineAsyncComponent } from "vue";
const Skeleton = defineAsyncComponent(() => import("primevue/skeleton"));

const { data } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const json = computed(() => JSON.stringify(data));
</script>
