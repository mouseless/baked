<template>
  <div>
    <Button
      v-tooltip="{
        value: lc('Languages'),
        showDelay: 300,
        class: 'max-md:!hidden 2xl:!hidden'
      }"
      :pt="{
        label: { class: 'hidden max-md:inline 2xl:inline' }
      }"
      class="
        py-3 px-4 gap-4 justify-start
        max-md:w-full max-md:py-2
        2xl:w-full 2xl:py-2
      "
      type="button"
      size="large"
      severity="secondary"
      aria-haspopup="true"
      aria-controls="overlay_menu"
      :text="true"
      :icon="'pi pi-language'"
      :label="lc('Languages')"
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

const { locale, getLocales, setLocale, localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "LanguageSwitcher" });

defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const menu = ref();

const model = computed(() =>
  getLocales().map(currentLocale => ({
    label: `${l(currentLocale.name)} (${currentLocale.code.toUpperCase()})`,
    class: locale.value === currentLocale.code ? "opacity-50" : "",
    command: () => setLocale(currentLocale.code)
  }))
);

function toggle(event) {
  menu.value.toggle(event);
}
</script>
