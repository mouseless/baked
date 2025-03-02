<template>
  <Bake
    v-if="pageDescriptor"
    :descriptor="pageDescriptor"
  />
</template>
<script setup>
import Bake from "./Bake.vue";

const { routeParams } = defineProps({
  routeParams: {
    type: null,
    required: true
  }
});

const { public: { title } } = useRuntimeConfig();
useHead({ title });

const pageDescriptor = ref();
const pageName = computed(() => routeParams[0] ?? "index");

provide("routeParams", routeParams);

onMounted(async() => {
  pageDescriptor.value = await import(`~/.baked/${pageName.value}.page.json`)
    .catch(_ => {
      throw createError({
        statusCode: 404,
        statusMessage: `'${pageName.value}' Page Not Found`,
        fatal: true
      });
    });
});
</script>
