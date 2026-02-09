<template>
  <Panel
    ref="panel"
    :header="localizeTitle ? l(title) : title"
    :collapsed="collapsedState"
    toggleable
    :pt="
      {
        headerActions: 'flex gap-2 items-center',
        title: 'max-sm:truncate',
        contentContainer: 'block'
      }
    "
    @update:collapsed="onCollapsed"
  >
    <template
      v-if="$slots.inputs || inputs.length > 0"
      #icons
    >
      <template v-if="isMd">
        <div class="flex gap-2 text-xs">
          <Inputs
            v-if="inputs.length > 0"
            :inputs="inputs"
            @ready="onReady"
            @changed="onChanged"
          />
          <slot
            v-if="$slots.inputs"
            name="inputs"
          />
        </div>
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
              flex flex-col gap-2 min-w-24
              w-full px-2 py-2 text-xs
            "
          >
            <Inputs
              v-if="inputs.length > 0"
              :inputs="inputs"
              input-class="max-md:w-full"
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
import { computed, ref, useTemplateRef } from "vue";
import { Message, Panel, Button } from "primevue";
import { Bake, Inputs, PersistentPopover } from "#components";
import { useBreakpoints, useContext, useDataMounter, useUiStates, useLocalization } from "#imports";

const { value: { panelStates } } = useUiStates();
const { isMd } = useBreakpoints();
const context = useContext();
const { mount: mountData } = useDataMounter();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "DataPanel" });
const panel = useTemplateRef("panel");

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { collapsed, content, inputs, localizeTitle, title: titleData } = schema;

const contextData = context.injectContextData();
const path = context.injectPath();
const collapsedState = computed(() => panelStates[path] ?? collapsed);
const loaded = ref(!collapsedState.value);
const ready = ref(inputs.length === 0); // it is ready when there is no parameter
const uniqueKey = ref("");
const popover = ref();

const values = ref({});
if(inputs.length > 0) {
  contextData.parent["parameters"] = values;
}

const title = mountData(titleData);

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
