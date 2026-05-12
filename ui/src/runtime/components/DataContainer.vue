<template>
  <div
    class="
      bg-transparent dark:bg-zinc-900
      border rounded-md border-1
      border-slate-200 dark:border-zinc-700
    "
  >
    <div
      v-if="inputs.length > 0"
      class="
        inputs
        py-2 px-4
        bg-transparent
        rounded-none text-sm
        flex gap-4 items-center justify-end
      "
    >
      <Inputs
        :inputs="inputs"
        @ready="onReady"
        @changed="onChanged"
      />
    </div>
    <div class="content p-4 [contain:inline-size]">
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
    </div>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Message } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Bake, Inputs } from "#components";

const context = useContext();
const { localize: lc } = useLocalization({ group: "DataContainer" });

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { content, inputs } = schema;

const contextData = context.injectContextData();

const loaded = ref(true);
const ready = ref(inputs.length === 0); // it is ready when there is no parameter
const uniqueKey = ref("");

const values = ref({});
if(inputs.length > 0) {
  contextData.parent["container-parameters"] = values;
}

function onReady(value) {
  ready.value = value;
}

function onChanged(event) {
  uniqueKey.value = event.uniqueKey;
  values.value = event.values;
}
</script>
<style>
/* If DataContainer in a DataPanel, clear border and radius */
.p-panel-content:has(.b-component--DataContainer) {
  .b-component--DataContainer {
    @apply border-none rounded-none;
  }
}

.b-component--DataContainer {
  div {
    @apply [&:has(.p-datatable)]:p-0;
  }
}

.b-component--DataContainer:has(.inputs) {
  div.content {
    @apply pt-0;
  }
}
</style>