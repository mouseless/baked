<template>
  <Bake
    v-if="pageDescriptor"
    :descriptor="pageDescriptor"
  />
  <Toast />
</template>
<script setup>
import Toast from "primevue/toast";
import { computed, onMounted, provide, ref } from "vue";
import { createError } from "#app";
import Bake from "./Bake.vue";

const { routeParams } = defineProps({
  routeParams: {
    type: null,
    required: true
  }
});

const pageDescriptor = ref();
const pageName = computed(() => routeParams[0] ?? "index");
const { $pages } = useNuxtApp();

provide("routeParams", routeParams);

onMounted(async() => {
  if(!$pages[pageName.value]){
    throw createError({
      statusCode: 404,
      statusMessage: `'${pageName.value}' Page Not Found`,
      fatal: true
    });
  }

  pageDescriptor.value = await $pages[pageName.value]();
});
</script>
