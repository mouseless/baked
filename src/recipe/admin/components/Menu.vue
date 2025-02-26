<template>
  <Menubar :model="data">
    <template #item="{ item, props, hasSubmenu }">
      <RouterLink
        v-if="item.route"
        :to="item.route"
        v-bind="props.action"
      >
        <span>{{ item.label }}</span>
      </RouterLink>
      <a
        v-else
        :href="item.url"
        :target="item.target"
        v-bind="props.action"
      >
        <span>{{ item.label }}</span>
        <span
          v-if="hasSubmenu"
          class="pi pi-fw pi-angle-down"
        />
      </a>
    </template>
  </Menubar>
</template>
<script setup>
import { Menubar } from "primevue";
import { RouterLink } from "vue-router";

defineProps({
  data: { type: null, required: true }
});
</script>
