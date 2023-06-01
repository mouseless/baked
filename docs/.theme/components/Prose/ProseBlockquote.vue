<template>
  <blockquote :class="type">
    <component :is="() => body" />
  </blockquote>
</template>
<script setup>
import { computed, useSlots } from "#imports";

const slots = useSlots();

const slot = computed(() => {
  return slots.default();
});

const firstLine = computed(() => {
  return slot.value[0].children.default()[0].children;
});

const types = {
  ":information_source:": "info",
  ":warning:": "warning",
  ":bulb:": "success",
  ":x:": "danger"
};

const type = computed(() => {
  return types[firstLine.value] ?? "default";
});

const body = computed(() => {
  let result = slot.value;

  if(types[firstLine.value]) {
    result = result.slice(1);
  }

  return result;
});
</script>
<style scoped>
blockquote {
  padding-left: 1em;
  border-left: solid 5px;
  margin-left: 1em;
}
.info {
  border-color: lightblue;
}
.warning {
  border-color: orange;
}
.success {
  border-color: lightgreen;
}
.danger {
  border-color: red;
}
.default {
  border-color: lightgray;
}
</style>
