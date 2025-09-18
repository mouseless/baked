<template>
  <Panel
    ref="panel"
    :header="localizeTitle ? l(title) : title"
    :collapsed="collapsedState"
    toggleable
    :pt="
      {
        headerActions: {
          class: 'flex gap-2 items-center'
        },
        title: 'max-sm:truncate'
      }
    "
    @update:collapsed="onCollapsed"
  >
    <template
      v-if="$slots.parameters || parameters.length > 0"
      #icons
    >
      <template v-if="isMd">
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
      <template v-else>
        <Button
          v-if="parameters.length > 0 || $slots.parameters"
          variant="text"
          icon="pi pi-sliders-h"
          class="lg:hidden"
          rounded
          @click="togglePopover"
        />
        <PersistentPopover ref="popover">
          <div
            class="
              flex flex-row flex-start
              justify-between w-full
              gap-4 text-xs px-2 py-2
            "
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
          </div>
        </PersistentPopover>
      </template>
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
        <span class="ml-3">{{ lc("Select required values to view this data") }}</span>
      </Message>
    </template>
  </Panel>
</template>
<script setup>
import { computed, onMounted, ref, useTemplateRef } from "vue";
import { Message, Panel, Button } from "primevue";
import { Bake, Parameters, PersistentPopover } from "#components";
import { useBreakpoints, useContext, useDataFetcher, useUiStates, useLocalization } from "#imports";

const { value: { panelStates } } = useUiStates();
const { isMd } = useBreakpoints();
const context = useContext();
const dataFetcher = useDataFetcher();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "DataPanel" });
const panel = useTemplateRef("panel");
const popover = ref();

function togglePopover(event) {
  popover.value.toggle(event);
}

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { collapsed, content, localizeTitle, parameters, title: titleData } = schema;

const injectedData = context.injectData();
const path = context.injectPath();
const collapsedState = computed(() => panelStates[path] ?? collapsed);
const loaded = ref(!collapsedState.value);
const ready = ref(parameters.length === 0); // it is ready when there is no parameter
const uniqueKey = ref("");

const values = ref({});
if(parameters.length > 0) {
  context.provideData(values, "Custom");
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
