<template>
  <SpeedDial
    :model
    :direction
  >
    <template #button="{ toggleCallback }">
      <Button
        @click="toggleCallback"
      >
        <i
          class="pi pi-globe text-xl"
          style="color: slateblue"
        />
      </Button>
    </template>
    <template #item="{ item, toggleCallback }">
      <div
        class="
          flex flex-col items-center justify-between
          gap-2
          w-10
          cursor-pointer
        "
        @click="e => { toggleCallback(e); setLocale(item.label); }"
      >
        <img
          :src="`https://flagcdn.com/${item.label === 'en' ? 'us' : item.label}.svg`"
          class="w-10 h-5"
        >
      </div>
    </template>
  </SpeedDial>
</template>
<script setup>
import { SpeedDial } from "primevue";
import { computed, useLocalization } from "#imports";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { locales, setLocale } = useLocalization();

const model = computed(() =>
  locales.value.map(l => ({
    label: l
  }))
);
const { direction } = schema;
</script>
    <!-- /**
     * Property name or getter function to use as the label of an item.
     */
    label?: string | ((...args: any) => string) | undefined;
    /**
     * Icon of the item.
     */
    icon?: string | undefined;
    /**
     * Callback to execute when item is clicked.
     * @param {MenuItemCommandEvent} event - Custom command event.
     * @todo next release should be able to change
     */
    command?: (event: MenuItemCommandEvent) => void;
    /**
     * External link to navigate when item is clicked.
     */
    url?: string | undefined;
    /**
     * 	An array of children menuitems.
     */
    items?: MenuItem[] | undefined;
    /**
     * A boolean or a function to return a boolean to specify if the item is disabled.
     * @defaultValue false
     */
    disabled?: boolean | ((...args: any) => boolean) | undefined;
    /**
     * A boolean or a function to return a boolean to specify if the item is visible.
     * @defaultValue true
     */
    visible?: boolean | ((...args: any) => boolean) | undefined;
    /**
     * Specifies where to open the linked document.
     */
    target?: string | undefined;
    /**
     * Defines the item as a separator.
     * @defaultValue false
     */
    separator?: boolean | undefined;
    /**
     * Inline style of the menuitem.
     */
    style?: any;
    /**
     * Style class of the menuitem.
     */
    class?: any;
    /**
     * Unique identifier of an item.
     */
    key?: string | undefined;
    /**
     * Optional
     */
    [key: string]: any; -->