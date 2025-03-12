<template>
  <Bake
    v-if="pageDescriptor"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import { Bake } from "#components";
import { usePages } from "#imports";
import { ref, onMounted } from "vue";

const pages = usePages();
const pageDescriptor = ref();

onMounted(async() => pageDescriptor.value = await pages.fetch("errorPage"));
</script>