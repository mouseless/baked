<template>
  <Bake
    v-if="pageDescriptor"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import { ref, onMounted } from "vue";
import { useRuntimeConfig } from "#app";
import { Bake } from "#components";

const pageDescriptor = ref();
const config = useRuntimeConfig().public.error;

onMounted(() => pageDescriptor.value = config);
</script>