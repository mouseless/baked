<template>
  <component :is="content()" />
</template>
<script setup>
import { cloneVNode, Comment, Fragment, h, useAttrs, useSlots } from "vue";

const attrs = useAttrs();
const slots = useSlots();

defineOptions({ inheritAttrs: false });

const { when, as } = defineProps({
  when: { type: null, default: false },
  as: { type: String, default: "span" }
});

function content() {
  const children = (slots.default?.() ?? [])
    .filter(c => c.type !== Comment)
    .map(c => cloneVNode(c, attrs));

  if(when) { return h(as, { class: "contents" }, children); }

  return children.length === 1 ? children[0] : h(Fragment, children);
}
</script>