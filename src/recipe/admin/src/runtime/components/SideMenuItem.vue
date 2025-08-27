<template>
  <RouterLink :to="item.route">
    <Button
      :text="!selected"
      :icon="item.icon"
      :severity="selected ? 'primary':'secondary'"
      size="large"
      class="hidden 2xl:flex py-2 px-6 w-full justify-start"
      :label="l(item.title)"
      :disabled="item.disabled"
    />
    <Button
      v-tooltip="{ value: l(item.title), showDelay: 300 }"
      :text="!selected"
      :icon="item.icon"
      :severity="selected ? 'primary':'secondary'"
      size="large"
      class="2xl:hidden py-2 px-6"
      :disabled="item.disabled"
    />
  </RouterLink>
</template>
<script setup>
import { computed } from "vue";
import { RouterLink } from "vue-router";
import { Button } from "primevue";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { item, path } = defineProps({
  item: { type: Object, required: true },
  path: { type: String, required: true }
});

const selected = computed(() => item.route === path || (item.route !== "/" && path.startsWith(item.route)));
</script>
