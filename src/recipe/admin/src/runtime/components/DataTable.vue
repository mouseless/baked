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
    :csv-separator="exportOptions?.csvSeparator"
    :export-filename="exportOptions?.fileName"
    :export-function
  >
    <template
      v-if="exportOptions"
      #header
    >
      <div class="text-end pb-4">
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
    </template>
    <Column
      v-for="column in columns"
      :key="column.prop"
      :field="column.prop"
      :header="column.title"
      class="text-nowrap"
      :class="{ 'min-w-40': column.minWidth }"
      :exportable="column.exportable"
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
      exportparts="true"
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

const { columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight } = schema;

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
const scrollable = scrollHeight !== undefined;
const formatter = ref();

function exportDataTable() {
  composableResolver.resolve(exportOptions.formatter).then(result => {
    formatter.value = result.default();
    dataTable.value.exportCSV();
  });
}

function exportFunction({ data, field }) {
  if(!formatter?.value) { return data; }

  return formatter.value.format(data, field);;
}
</script>