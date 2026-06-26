<template>
  <div
    ref="mermaidContainer"
    class="
      mermaid flex justify-center p-sm
      bg-darkgreen-800 rounded-xs bg-bg-box border-bg-second
      pt-[30px] pb-[28px]
    "
  >
    <slot />
  </div>
</template>
<script setup>
import { useNuxtApp } from "nuxt/app";
import { onMounted, ref } from "vue";

const { $mermaid } = useNuxtApp();
const mermaidContainer = ref(null);
const diagramId = `mermaid-${Math.random().toString(36).slice(2, 9)}`;

onMounted(async() => {
  const content = mermaidContainer.value.textContent?.trim();
  if(!$mermaid || !content) { return; }

  try {
    const { svg } = await $mermaid.render(diagramId, content);
    mermaidContainer.value.innerHTML = svg;
  } catch {
    mermaidContainer.value.innerHTML = "";
  }
});
</script>