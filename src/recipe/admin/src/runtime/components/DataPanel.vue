<template>
  <Panel
    ref="panel"
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
        name="content"
        :descriptor="content"
      />
    </template>
  </Panel>
</template>
<script setup>
import { computed, inject, useTemplateRef, ref } from "vue";
import { Panel } from "primevue";
import Bake from "./Bake.vue";
import { useUiStates } from "#imports";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { value: { panelStates } } = useUiStates();
const panel = useTemplateRef("panel");

const uiContext = inject("uiContext");

const { collapsed, content, title } = schema;
const collapsedState = computed(() => panelStates[uiContext] ?? collapsed);
const loaded = ref(!collapsedState.value);

function onCollapsed(collapsed) {
  panelStates[uiContext] = collapsed;

  if(!collapsed && !loaded.value) {
    loaded.value = true;
  }

  if(!collapsed) {
    setTimeout(() => {
      panel.value.$el.scrollIntoView({ behavior: "smooth", block: "nearest" });
    }, 750);
  }
}
</script>
