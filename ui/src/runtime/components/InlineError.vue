<template>
  <Bake
    v-if="error"
    name="error"
    :descriptor="inlineError"
  >
    <template #content>
      {{ error.detail }}
    </template>
  </Bake>
</template>
<script setup>
import { computed } from "vue";
import { useRuntimeConfig } from "#imports";

const { public: { inlineError: inlineErrorRaw } } = useRuntimeConfig();

const { error } = defineProps({
  error: { type: null, default: undefined }
});

const inlineError = computed(() => ({
  ...inlineErrorRaw,
  data: {
    type: "Inline",
    value: error.title
  }
}));
</script>