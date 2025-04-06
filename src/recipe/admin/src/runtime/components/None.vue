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
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const { data } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, required: true }
});

const loading = context.loading();
const json = computed(() => JSON.stringify(data));
</script>
