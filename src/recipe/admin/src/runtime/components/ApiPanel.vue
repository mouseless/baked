<template>
  <Panel
    :header="title"
    :collapsed="collapsedState"
    toggleable
    :pt="{ headerActions: { class: 'flex gap-2 items-center' } }"
    @update:collapsed="onCollapsed"
  >
    <!--
    <template
      v-if="$slots.parameters"
      #icons
    >
      <slot name="parameters" />
    </template>
    -->
    <template #default>
      <Bake
        v-if="loaded"
        :descriptor="viewer"
      />
    </template>
  </Panel>
</template>
<script setup>
import { computed, ref } from "vue";
import { Panel } from "primevue";
import Bake from "./Bake.vue";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { collapsed, title, viewer } = schema;

// const collapsedState = computed(() => panelStates[statePath] ?? collapsed);
const collapsedState = computed(() => collapsed);
const loaded = ref(!collapsedState.value);

function onCollapsed(collapsed) {
  // panelStates[statePath] = collapsed;

  if(!collapsed && !loaded.value) {
    loaded.value = true;
  }

  /*
  if(!collapsed) {
    setTimeout(() => {
      panel.value.$el.scrollIntoView({ behavior: "smooth" });
    }, 750);
  }
  */
}
</script>
