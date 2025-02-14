<template>
  <PanelMenu v-if="schema" :model="items" class="w-full md:w-80">
    <template #item="{ item }">
      <NuxtLink v-ripple class="flex items-center cursor-pointer text-surface-700 dark:text-surface-0 px-4 py-2" :to="item.url">
        <span class="ml-2">{{ item.label }}</span>
        <span v-if="item.items" class="pi pi-angle-down text-primary ml-auto" />
      </NuxtLink>
    </template>
  </PanelMenu>
</template>
<script setup>
const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { data } = schema;

const items = computed(() => [
  {
    label: "Rich Transients",
    icon: "pi pi-palette",
    items: data.map(item => {
      return {
        label: item.title,
        url: item.path
      };
    })
  }
]);
</script>
