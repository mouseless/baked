<template>
  <DataTable
    :value
    class="text-sm min-h-24"
    striped-rows
    :data-key
    :paginator="paginator && value.length > rows"
    :rows
    :scrollable
    :scroll-height
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
            ...conditional.find(column.component, row),
            data: {
              type: 'Inline',
              value: row[column.prop]
            }
          }"
        />
        <span v-else>-</span>
      </template>
    </Column>
    <ColumnGroup
      v-if="footerTemplate"
      type="footer"
    >
      <Row>
        <Column
          :footer="footerTemplate.label"
          :colspan="footerColSpan"
          footer-style="text-align:right"
        />
        <Column
          v-for="column in footerTemplate.columns"
          :key="column.prop"
        >
          <template #footer>
            <Skeleton
              v-if="loading"
              class="min-h-5"
            />
            <Bake
              v-else-if="data"
              :name="`rows/footer/${column.prop}`"
              :descriptor="{
                ...conditional.find(column.component, data),
                data: {
                  type: 'Inline',
                  value: data[column.prop]
                }
              }"
            />
            <span v-else>-</span>
          </template>
        </Column>
      </Row>
    </ColumnGroup>
  </DataTable>
</template>
<script setup>
import { computed } from "vue";
import Column from "primevue/column";
import { ColumnGroup, DataTable, Row, Skeleton } from "primevue";
import { Bake } from "#components";
import { useConditional, useContext } from "#imports";

const conditional = useConditional();
const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { columns, dataKey, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight } = schema;

const loading = context.loading();
const value = computed(() =>
  data
    ? itemsProp
      ? data[itemsProp]
      : data
    : new Array(rowsWhenLoading || 5).fill({ })
);
const footerColSpan = computed(() => columns.length - footerTemplate?.columns.length);
const scrollable = scrollHeight !== undefined;
</script>
