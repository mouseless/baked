<template>
  <Bake
    v-if="error"
    name="error"
    :descriptor
  >
    <template #content>
      {{ error.detail }}
    </template>
  </Bake>
</template>
<script setup>
import { computed } from "vue";
import { useRuntimeConfig } from "#imports";

const { public: { inlineError } } = useRuntimeConfig();

const { error } = defineProps({
  error: { type: null, default: undefined }
});

const descriptor = computed(() => ({
  ...inlineError,
  data: {
    type: "Inline",
    value: error.title
  }
}));
</script>