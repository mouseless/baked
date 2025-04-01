<template>
  <DataTable
    :value
    style="min-height: 100px"
    class="text-sm"
    striped-rows
    :data-key
    :paginator="paginator && value.length > rows"
    :rows
  >
    <Column
      v-for="column in columns"
      :key="column.prop"
      :field="column.prop"
      :header="column.title"
      class="text-nowrap"
      :class="{ 'min-w-40': column.minWidth }"
    >
      <template #body="{ data: row, index }">
        <Skeleton v-if="loading" />
        <Bake
          v-else
          :name="`rows/${index}/${column.prop}`"
          :descriptor="{
            ...column.component,
            data: {
              type: 'Inline',
              value: row[column.prop]
            }
          }"
        />
      </template>
    </Column>
  </DataTable>
</template>
<script setup>
import { computed, defineAsyncComponent } from "vue";
import Column from "primevue/column";
const DataTable = defineAsyncComponent(() => import("primevue/datatable"));
const Skeleton = defineAsyncComponent(() => import("primevue/skeleton"));
import Bake from "./Bake.vue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true },
  loading: { type: Boolean, default: false }
});

const { columns, dataKey, paginator, rows, rowsWhenLoading } = schema;

const value = computed(() => data ?? new Array(rowsWhenLoading || 5).fill({ }));
</script>
