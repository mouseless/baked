<template>
  <div class="space-y-4">
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
  </div>
</template>
<script setup>
import { Panel } from "primevue";
import Bake from "./Bake.vue";

defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
