<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <span v-if="data">{{ text }}</span>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import AwaitLoading from "./AwaitLoading";

const { schema, data: rawData } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { prop, format = "dd/MM/yyyy" } = schema;

const data = computed(() => {
  if(!rawData) { return null; }
  if(prop) { return rawData[prop]; }

  return rawData;
});
const text = computed(() => {
  if(!data.value || !format) { return data.value; }

  const date = new Date(data.value);
  if(isNaN(date.getTime())) { return data.value; }

  const dd = String(date.getDate()).padStart(2, "0");
  const MM = String(date.getMonth() + 1).padStart(2, "0");
  const yyyy = String(date.getFullYear());
  const HH = String(date.getHours()).padStart(2, "0");
  const mm = String(date.getMinutes()).padStart(2, "0");
  const ss = String(date.getSeconds()).padStart(2, "0");

  return format
    .replace("dd", dd)
    .replace("MM", MM)
    .replace("yyyy", yyyy)
    .replace("HH", HH)
    .replace("mm", mm)
    .replace("ss", ss);
});
</script>
