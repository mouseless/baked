<template>
  <blockquote
    :class="type.class"
    class="
      flex flex-row items-start justify-start
      p-0 my-(--space-sm) mx-0 max-w-(--width-content)
    "
  >
    <i
      v-if="type.icon"
      :class="[
        'fa', type.icon,
        'leading-(--line-height)! mt-(--space-sm)',
        {
          'text-(--color-blue-0)': type.class === 'info',
          'text-(--color-orange-n1)': type.class === 'warning',
          'text-(--color-green-n1)': type.class === 'tip',
          'text-(--color-red-600)': type.class === 'danger',
          'text-(--color-fg)': type.class === 'default',
        }
      ]"
    />
    <div class="w-full pl-(--space-sm)">
      <component :is="() => body" />
    </div>
  </blockquote>
</template>
<script setup>
import { computed, useSlots } from "#imports";

const slots = useSlots();

const slot = computed(() => {
  return slots.default();
});

const firstLine = computed(() => {
  return slot.value[0].children.default()[0].children[0].children;
});

const types = {
  "!NOTE": { class: "info", icon: "fa-circle-info" },
  "!WARNING": { class: "warning", icon: "fa-warning" },
  "!TIP": { class: "tip", icon: "fa-lightbulb" },
  "!CAUTION": { class: "danger", icon: "fa-circle-xmark" },
  default: { class: "default", icon: "fa-angle-right" }
};

const type = computed(() => {
  return types[firstLine.value] ?? types.default;
});

const body = computed(() => {
  let result = slot.value;

  if(types[firstLine.value]) {
    result = result.slice(1);
  }

  return result;
});
</script>
