<template>
  <div
    v-if="loading"
    class="min-w-60"
  >
    <Skeleton class="min-h-10" />
  </div>
  <FloatLabel>
    <Select
      v-model="selected"
      :input-id="uiContext"
      :options="data"
      :option-label="optionLabel"
      :placeholder="label"
      :show-clear
    />
    <label for="period">{{ label }}</label>
  </FloatLabel>
</template>
<script setup>
import { inject, ref, watch } from "vue";
import { FloatLabel, Select, Skeleton } from "primevue";

const { schema, data, loading } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const model = defineModel({
  type: null,
  required: true
});

const { label, optionLabel, optionValue, showClear } = schema;

const uiContext = inject("uiContext");

const selected = ref({});
watch(selected, newSelected => {
  if(optionValue) {
    model.value = newSelected?.[optionValue];
  } else {
    model.value = newSelected;
  }
});
watch(model, () => selected.value = getOptionOfModel());

if(!loading) { selected.value = getOptionOfModel(); }
watch(() => loading, newLoading => {
  if(newLoading) { return; }

  selected.value = getOptionOfModel();
});

function getOptionOfModel() {
  return optionValue
    ? data.filter(o => o[optionValue] === model.value)[0]
    : model.value;
}
</script>
<style lang="scss">
/*
placeholder gives select the initial width, but it overlaps with label so it is
hidden
*/
.p-placeholder {
  visibility: hidden;
}
</style>
