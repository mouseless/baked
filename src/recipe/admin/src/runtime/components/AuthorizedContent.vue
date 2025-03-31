<template>
  <slot v-if="authorized" />
</template>
<script setup>
import { onMounted, ref } from "vue";
import { useToken } from "#imports";

const token = useToken();
const authorized = ref(false);

onMounted(async() => {
  const result = await token.current(false);

  authorized.value = result !== null;
});
</script>
