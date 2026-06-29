<template>
  <Select
    v-if="isXs"
    v-model="model"
    :schema
    :data
    class="
      shadow-none rounded-md
      bg-slate-100 border-slate-100
      dark:bg-zinc-950 dark:border-zinc-950
    "
  />
</template>
<script setup>
import { useBreakpoints } from "#imports";
import { Select } from "#components";

const { isXs } = useBreakpoints();

defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });
</script>
<!-- <style>
.b-component--PageSize {
  .p-select-label {
    font-size: inherit;
  }
}
</style> -->