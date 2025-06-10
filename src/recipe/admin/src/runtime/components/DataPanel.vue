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
      <Message
        v-else-if="!ready"
        severity="info"
      >
        <i class="pi pi-info-circle" />
        <span class="ml-3">{{ l("Data_panel.Select_required_values_to_view_this_data") }}</span>
      </Message>
    </template>
  </Panel>
</template>
<script setup>
import { computed, onMounted, ref, useTemplateRef } from "vue";
import { Message, Panel } from "primevue";
import { Bake, Parameters } from "#components";
import { useContext, useDataFetcher, useUiStates, useLocalization } from "#imports";

const { value: { panelStates } } = useUiStates();
const context = useContext();
const dataFetcher = useDataFetcher();
const { localize: l } = useLocalization();
const panel = useTemplateRef("panel");

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { collapsed, content, parameters, title: titleData } = schema;

const injectedData = context.injectedData();
const path = context.path();
const collapsedState = computed(() => panelStates[path] ?? collapsed);
const loaded = ref(!collapsedState.value);
const ready = ref(parameters.length === 0); // it is ready when there is no parameter
const uniqueKey = ref("");

const values = ref({});
if(parameters.length > 0) {
  context.setInjectedData(values, "Custom");
}

const title = ref(dataFetcher.get({ data: titleData, injectedData }));
const shouldLoadTitle = dataFetcher.shouldLoad(titleData.type);

onMounted(async() => {
  if(shouldLoadTitle) {
    title.value = await dataFetcher.fetch({ data: titleData, injectedData });
  }
});

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
