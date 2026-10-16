<template>
  <RouterLink :to="item.route">
    <Button
      v-tooltip="{ value: l(item.title), showDelay: 300, class: '2xl:!hidden max-md:!hidden' }"
      :text="!selected"
      :icon="item.icon"
      :label="l(item.title)"
      :disabled="item.disabled"
      severity="secondary"
      variant="text"
      size="large"
      class="
        py-3 text-sm rounded-none
        2xl:rounded-full 2xl:w-full
        2xl:justify-start 2xl:gap-3
      "
      :class="{ 'bg-slate-200 dark:bg-zinc-800': selected }"
      :pt="{
        label: { class: 'max-2xl:hidden leading-none' }
      }"
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