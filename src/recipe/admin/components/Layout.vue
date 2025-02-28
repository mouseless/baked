<template>
  <Bake
    v-if="layoutDescriptor"
    :descriptor="layoutDescriptor"
  >
    <slot />
  </Bake>
</template>
<script setup>
import Bake from "./Bake.vue";

const { name } = defineProps({
  name: { type: String, required: true }
});

const layoutDescriptor = ref();

onMounted(async() => {
  layoutDescriptor.value = await import(`~/.baked/${name}.layout.json`)
    .catch(_ => {
      throw createError({
        statusCode: 404,
        statusMessage: `'${name}' Layout Not Found`,
        fatal: true
      });
    });
});
</script>
