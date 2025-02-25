<template>
  <Menubar :model="data">
    <template #item="{ item, props, hasSubmenu }">
      <router-link
        v-if="item.route"
        :to="item.route"
        v-bind="props.action"
      >
        <span>{{ item.label }}</span>
      </router-link>
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

const { data } = defineProps({
  data: { type: null, required: true }
});
</script>
