<template>
  <Panel
    ref="panel"
    :header="localizeTitle ? l(title) : title"
    :collapsed="collapsedState"
    toggleable
    :pt="
      {
        headerActions: 'flex gap-2 items-center',
        title: 'max-sm:truncate'
      }
    "
    @update:collapsed="onCollapsed"
  >
    <template
      v-if="$slots.inputs || inputs.length > 0"
      #icons
    >
      <template v-if="isMd">
        <Inputs
          v-if="inputs.length > 0"
          :inputs="inputs"
          class="text-xs"
          @ready="onReady"
          @changed="onChanged"
        />
        <slot
          v-if="$slots.inputs"
          name="inputs"
        />
      </template>
      <template v-else>
        <Button
          v-if="inputs.length > 0 || $slots.inputs"
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
            <Inputs
              v-if="inputs.length > 0"
              :inputs="inputs"
              class="text-xs"
              @ready="onReady"
              @changed="onChanged"
            />
            <slot
              v-if="$slots.inputs"
              name="inputs"
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
import { Bake, Inputs, PersistentPopover } from "#components";
import { useBreakpoints, useContext, useDataFetcher, useUiStates, useLocalization } from "#imports";

const { value: { panelStates } } = useUiStates();
const { isMd } = useBreakpoints();
const context = useContext();
const dataFetcher = useDataFetcher();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "DataPanel" });
const panel = useTemplateRef("panel");

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { collapsed, content, inputs, localizeTitle, title: titleData } = schema;

const parentContext = context.injectParentContext();
const path = context.injectPath();
const collapsedState = computed(() => panelStates[path] ?? collapsed);
const loaded = ref(!collapsedState.value);
const ready = ref(inputs.length === 0); // it is ready when there is no parameter
const uniqueKey = ref("");
const popover = ref();

const values = ref({});
if(inputs.length > 0) {
  parentContext["parameters"] = values;
}

const title = ref(dataFetcher.get({ data: titleData, contextData: { parent: parentContext } }));
const shouldLoadTitle = dataFetcher.shouldLoad(titleData.type);

onMounted(async() => {
  if(shouldLoadTitle) {
    title.value = await dataFetcher.fetch({ data: titleData, contextData: { parent: parentContext } });
  }
});

function togglePopover(event) {
  popover.value.toggle(event);
}

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
