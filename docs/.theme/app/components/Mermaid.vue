<template>
  <div
    ref="mermaidContainer"
    class="
      mermaid flex justify-center p-sm bg-darkgreen-800
      rounded-xs [&:not([data-processed])]:text-transparent
      bg-bg-box border-bg-second
      pt-[30px] pb-[18px]
    "
  >
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