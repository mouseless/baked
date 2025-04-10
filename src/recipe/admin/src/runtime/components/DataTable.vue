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
        <Skeleton
          v-if="loading"
          class="min-h-5"
        />
        <Bake
          v-else-if="data"
          :name="`rows/${index}/${column.prop}`"
          :descriptor="{
            ...findComponent(column, row),
            data: {
              type: 'Inline',
              value: row[column.prop]
            }
          }"
        />
        <span v-else>-</span>
      </template>
    </Column>
  </DataTable>
</template>
<script setup>
import { computed } from "vue";
import Column from "primevue/column";
import { DataTable, Skeleton } from "primevue";
import { Bake } from "#components";
import { useContext } from "#imports";

const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { columns, dataKey, paginator, rows, rowsWhenLoading } = schema;

const loading = context.loading();
const value = computed(() => data ?? new Array(rowsWhenLoading || 5).fill({ }));

function findComponent(column, row) {
  const conditionalComponent = column.conditionalComponents.filter(component => row[component.prop] === component.value);
  if(conditionalComponent.length > 0) {
    return conditionalComponent[0].component;
  }

  return column.component;
}
</script>
