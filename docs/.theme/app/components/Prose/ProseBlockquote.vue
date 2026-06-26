<template>
  <blockquote
    :class="[
      type.class,
      {
        'border-blue-p3 bg-blue-p3/10': type.class === 'info',
        'border-orange-p2 bg-orange-p2/10': type.class === 'warning',
        'border-green-p2 bg-green-p1/10 dark:bg-darkgreen-p2/30': type.class === 'tip',
        'border-red-p3 bg-red-p3/20': type.class === 'danger',
        'border-gray-p2 bg-gray-p2/10': type.class === 'default',
      }
    ]"
    class="
      flex flex-row items-start justify-start
      p-0 my-sm mx-0 max-w-content px-sm
      rounded border-1 dark:border-0
    "
  >
    <i
      v-if="type.icon"
      :class="[
        'fa', type.icon,
        'leading-(--line-height)! mt-sm',
        {
          'text-blue-0': type.class === 'info',
          'text-orange-n1': type.class === 'warning',
          'text-green-n1': type.class === 'tip',
          'text-red-600': type.class === 'danger',
          'text-fg': type.class === 'default',
        }
      ]"
    />
    <div class="w-full pl-sm">
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
