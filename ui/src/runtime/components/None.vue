<template>
  <span class="bg-red-500 p-1 rounded-md">
    <AwaitLoading :skeleton="{ width:'3rem', class:'inline-block' }">
      <code>{{ json }}</code>
    </AwaitLoading>
  </span>
</template>
<script setup>
import { computed } from "vue";
import { AwaitLoading } from "#components";

const { data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const json = computed(() => JSON.stringify(data));
</script>
