<template>
  <PanelMenu v-if="schema" :model="items" class="w-full md:w-80">
    <template #item="{ item }">
      <Button as="router-link" :to="item.url" variant="link">
        {{ item.label }}
        <span v-if="item.items" class="pi pi-angle-down text-primary ml-auto" />
      </Button>
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
