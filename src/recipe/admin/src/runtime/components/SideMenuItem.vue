<template>
  <RouterLink :to="item.route">
    <Button
      v-tooltip="{ value: l(item.title), showDelay: 300 }"
      :text="!selected"
      :pt="{
        label: { class: 'hidden 2xl:inline' }
      }"
      :icon="item.icon"
      :severity="selected ? 'primary':'secondary'"
      size="large"
      class="
        py-3 px-4 gap-4 justify-start
        2xl:w-full 2xl:py-2 max-2xl:p-button-icon-only
      "
      :label="l(item.title)"
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
