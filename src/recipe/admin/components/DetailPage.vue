<template>
  <Bake
    v-if="schema.header"
    :descriptor="schema.header"
  />
  <Panel
    v-if="data"
    header="Details"
    toggleable
  >
    <div class="grid grid-cols-2 gap-4">
      <div
        v-for="prop in schema.props"
        :key="prop.key"
        class="flex gap-2"
      >
        <div class="text-nowrap">
          <strong>{{ prop.title }}:</strong>
        </div>
        <Bake
          :descriptor="{
            ...prop.component,
            'data': data[prop.key]
          }"
          class="w-full"
        />
      </div>
    </div>
  </Panel>
</template>
<script setup>
import Bake from "./Bake.vue";
import PageTitle from "./PageTitle.vue";
import { Panel } from "primevue";

defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
