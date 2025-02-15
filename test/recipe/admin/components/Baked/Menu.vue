<template>
  <PanelMenu v-if="schema" :model="items" class="w-full md:w-80">
    <template #item="{ item }">
      <Button v-if="!item.items" variant="link" :label="item.label" as="router-link" :to="item.url" />
      <Button v-else variant="link" :label="item.label" />
    </template>
  </PanelMenu>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

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
