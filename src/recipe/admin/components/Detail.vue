<template>
  <Panel :header="schema.title">
    <div
      v-if="schema.header"
      class="w-full"
    >
      <Bake :descriptor="schema.header" />
    </div>
    <div
      v-if="data"
      class="grid grid-cols-2 gap-4"
    >
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
import { Panel } from "primevue";
import Bake from "./Bake.vue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
</script>
