<template>
  <div>
    <Button
      v-tooltip="{ value: 'Languages', showDelay: 300 }"
      class="px-4 py-2"
      type="button"
      size="large"
      severity="secondary"
      aria-haspopup="true"
      aria-controls="overlay_menu"
      :text="true"
      :icon="'pi pi-language'"
      @click="toggle"
    />
    <Menu
      id="overlay_menu"
      ref="menu"
      :model
      :popup="true"
    />
  </div>
</template>
<script setup>
import { computed, ref } from "vue";
import { Button, Menu } from "primevue";
import { useLocalization } from "#imports";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { locale, locales, setLocale } = useLocalization();

const menu = ref();

const model = computed(() =>
  locales.map(l => ({
    label: `${l.name} (${l.code.toUpperCase()})`,
    class: locale.value === l.code ? "opacity-50" : "",
    command: () => setLocale(l.code)
  }))
);

function toggle(event) {
  menu.value.toggle(event);
}
</script>