<template>
  <Panel
    ref="panel"
    :header="title"
    :collapsed="collapsedState"
    toggleable
    :pt="{ headerActions: { class: 'flex gap-2 items-center' } }"
    @update:collapsed="onCollapsed"
  >
    <template
      v-if="$slots.parameters || parameters.length > 0"
      #icons
    >
      <Parameters
        v-if="parameters.length > 0"
        :parameters="parameters"
        class="text-xs"
        @ready="onReady"
        @changed="onChanged"
      />
      <slot
        v-if="$slots.parameters"
        name="parameters"
      />
    </template>
    <template #default>
      <Bake
        v-if="loaded && ready"
        :key="uniqueKey"
        name="content"
        :descriptor="content"
      />
    </template>
  </Panel>
</template>
<script setup>
import { computed, defineAsyncComponent, ref, useTemplateRef } from "vue";
const Panel = defineAsyncComponent(() => import("primevue/panel"));
import Parameters from "./Parameters.vue";
import { useContext, useUiStates } from "#imports";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { collapsed, content, parameters, title } = schema;

const { value: { panelStates } } = useUiStates();
const context = useContext();
const panel = useTemplateRef("panel");

const path = context.path();
const collapsedState = computed(() => panelStates[path] ?? collapsed);
const loaded = ref(!collapsedState.value);
const ready = ref(parameters.length === 0); // it is ready when there is no parameter
const uniqueKey = ref("");

const values = ref();
context.setInjectedData(values);

function onCollapsed(collapsed) {
  panelStates[path] = collapsed;

  if(!collapsed && !loaded.value) {
    loaded.value = true;
  }

  if(!collapsed) {
    setTimeout(() => {
      panel.value.$el.scrollIntoView({ behavior: "smooth", block: "nearest" });
    }, 750);
  }
}

function onReady(value) {
  ready.value = value;
}

function onChanged(event) {
  uniqueKey.value = event.uniqueKey;
  values.value = event.values;
}
</script>
