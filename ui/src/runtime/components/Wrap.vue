<template>
  <component :is="content()" />
</template>
<script setup>
import { Comment, Fragment, h, useSlots } from "vue";

const slots = useSlots();

const { when, as } = defineProps({
  when: { type: null, default: false },
  as: { type: String, default: "span" }
});

function content() {
  const children = (slots.default?.() ?? []).filter(c => c.type !== Comment);

  if(when) { return h(as, children); }

  return children.length === 1 ? children[0] : h(Fragment, children);
}
</script>