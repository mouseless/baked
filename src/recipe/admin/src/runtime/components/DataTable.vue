<template>
  <DataTable
    ref="dataTable"
    :value
    class="text-sm min-h-24"
    striped-rows
    :data-key
    :paginator="paginator && value.length > rows"
    :rows
    :scrollable
    :scroll-height
    :virtual-scroller-options="scrollHeight ? virtualScrollerOptions : null"
    :csv-separator="exportOptions?.csvSeparator"
    :export-filename="exportOptions?.fileName"
    :export-function
  >
    <Column
      v-for="(column, columnIndex) in columns"
      :key="column.prop"
      :field="column.prop"
      class="text-nowrap"
      :class="{ 'min-w-40': column.minWidth }"
      :exportable="column.exportable"
      :export-header="column.title"
    >
      <template #header>
        <div
          v-if="exportOptions && (columnIndex == columns.length - 1)"
          class="flex w-full items-center"
        >
          <span
            class="p-datatable-column-title w-full"
            data-pc-section="columntitle"
          >
            {{ column.title }}
          </span>
          <Button
            v-tooltip.left="exportOptions?.buttonLabel"
            severity="secondary"
            variant="text"
            aria-label="exportOptions?.buttonLabel"
            size="small"
            :icon="exportOptions?.buttonIcon"
            @click="exportDataTable"
          />
        </div>
        <span
          v-else
          class="p-datatable-column-title"
          data-pc-section="columntitle"
        >
          {{ column.title }}
        </span>
      </template>
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
import { computed, ref } from "vue";
import Column from "primevue/column";
import { Button, ColumnGroup, DataTable, Row, Skeleton } from "primevue";
import { Bake } from "#components";
import { useComposableResolver, useConditional, useContext } from "#imports";

const conditional = useConditional();
const context = useContext();
const composableResolver = useComposableResolver();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollable, scrollHeight, virtualScrollerOptions } = schema;

const dataTable = ref();
const loading = context.loading();
const value = computed(() =>
  data
    ? itemsProp
      ? data[itemsProp]
      : data
    : new Array(rowsWhenLoading || 5).fill({ })
);
const footerColSpan = computed(() => columns.length - footerTemplate?.columns.length);
const formatter = exportOptions?.formatter ? (await composableResolver.resolve(exportOptions.formatter)).default() : undefined;

function exportDataTable() {
  dataTable.value.exportCSV();
}

function exportFunction({ data, field }) {
  if(!formatter) { return data; }

  return formatter.format(data, field);
}
</script>