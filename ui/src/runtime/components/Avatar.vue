<template>
  <AwaitLoading :skeleton="{ height }">
    <Avatar
      :icon
      :image
      :label
      :size
    />
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Avatar } from "primevue";
import { AwaitLoading } from "#components";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { icon, size } = schema;

const heights = {
  "xlarge": "4rem",
  "large": "3rem"
};
const height = heights[size] ?? "2rem";
const image = computed(() => data && isURL(data) ? data : undefined);
const label = computed(() => data && !isURL(data) ? data : undefined);

function isURL(value) {
  return /^(https?:\/\/|\/\/|\/)/.test(value);
}
</script>