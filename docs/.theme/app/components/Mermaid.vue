<template>
  <div ref="mermaidContainer" class="mermaid">
    <slot />
  </div>
</template>
<script setup>
import { useNuxtApp } from "nuxt/app";
import { onMounted, ref, useSlots } from "vue";

const { $mermaid } = useNuxtApp();
const slots = useSlots();

const mermaidContainer = ref(null);
const slot = slots.default;

onMounted(async() => {
  const content = slot()[0].children;
  if(!$mermaid) { return; }
  if(!content) { return; }

  try {
    mermaidContainer.value.textContent = content;

    await $mermaid.run({ nodes: [mermaidContainer.value] });
  } catch {
    mermaidContainer.value.innerHTML = "";
  }
});
</script>
<style scoped>
.mermaid:not([data-processed]) {
  color: transparent;
}

.mermaid {
  text-align: center;
  padding: var(--space-sm);
  background-color: var(--color-darkgreen-800);
  border-radius: var(--border-radius);
}
</style>