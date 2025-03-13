<template>
  <Bake
    v-if="pageDescriptor"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import { ref, onMounted } from "vue";
import { Bake } from "#components";
import { usePages } from "#imports";

const pages = usePages();
const pageDescriptor = ref();

onMounted(async() => pageDescriptor.value = await pages.fetch("errorPage"));
</script>