<template>
  <RouterLink :to="item.route">
    <Button
      v-tooltip="{ value: item.title, showDelay: 300 }"
      :text="!selected"
      :icon="item.icon"
      :severity="selected ? 'primary':'secondary'"
      size="large"
      class="!px-6 !py-2"
      :disabled="item.soon"
    />
  </RouterLink>
</template>
<script setup>
import { RouterLink } from "vue-router";
import { Button } from "primevue";

const route = useRoute();

const { item } = defineProps({
  item: {
    type: Object,
    required: true
  }
});

const selected = computed(() => item.route === route.path || (item.route !== "/" && route.path.includes(item.route)));
</script>
