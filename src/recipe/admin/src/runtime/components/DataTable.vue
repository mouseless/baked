<template>
  <DataTable
    ref="dataTable"
    :value
    class="text-sm min-h-24"
    striped-rows
    :data-key
    :paginator="paginator && value.length > rows"
    :rows
    scrollable
    :scroll-height
    :virtual-scroller-options="scrollHeight ? virtualScrollerOptions : null"
    :csv-separator="exportOptions?.csvSeparator"
    :export-filename="exportOptions?.fileName"
    :export-function
  >
    <template #empty>
      {{ lc("No records found") }}
    </template>
    <Column
      v-for="column in columns"
      :key="column.prop"
      :header="l(column.title)"
      :field="column.prop"
      class="text-nowrap"
      :class="{ 'min-w-40': column.minWidth, 'text-right': column.alignRight }"
      :exportable="column.exportable"
      :export-header="l(column.title)"
      :pt="{ columnHeaderContent: { class: column.alignRight ? 'justify-end' : '' } }"
      :frozen="column.frozen"
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
            ...conditional.find(column.component, row.$getRow()),
            data: {
              type: 'Inline',
              value: row[column.prop].value
            }
          }"
        />
        <span v-else>-</span>
      </template>
    </Column>
    <Column
      v-if="exportOptions"
      :exportable="false"
      class="w-0 py-0"
      frozen
      align-frozen="right"
    >
      <template #header>
        <Button
          type="button"
          icon="pi pi-ellipsis-v"
          severity="secondary"
          variant="text"
          size="small"
          @click="toggleActionsMenu"
        />
        <Menu
          ref="actionsMenu"
          :model="actions"
          :popup="true"
        />
      </template>
    </Column>
    <ColumnGroup
      v-if="footerTemplate"
      type="footer"
    >
      <Row>
        <Column
          :footer="l(footerTemplate.label)"
          :colspan="footerColSpan"
          footer-style="text-align:right"
        />
        <Column
          v-for="column in footerTemplate.columns"
          :key="column.prop"
          :class="{ 'text-right': column.alignRight }"
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
        <Column
          v-if="exportOptions"
          :exportable="false"
          class="w-0"
          frozen
          align-frozen="right"
        />
      </Row>
    </ColumnGroup>
  </DataTable>
</template>
<script setup>
import { computed, ref } from "vue";
import Column from "primevue/column";
import { Button, ColumnGroup, DataTable, Menu, Row, Skeleton } from "primevue";
import { Bake } from "#components";
import { useComposableResolver, useConditional, useContext, useLocalization } from "#imports";

const conditional = useConditional();
const context = useContext();
const composableResolver = useComposableResolver();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization("DataTable");

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, virtualScrollerOptions } = schema;

const dataTable = ref();
const actionsMenu = ref();
const actions = ref([ ]);
const loading = context.loading();
const value = computed(() => {
  const items = data
    ? itemsProp
      ? data[itemsProp]
      : data
    : new Array(rowsWhenLoading || 5).fill({ });

  const result = [];
  for(const itemRow of items) {
    const resultRow = { $getRow: () => itemRow };

    for(const column of columns) {
      resultRow[column.prop] = { value: itemRow[column.prop], $getRow: () => itemRow };
    }

    result.push(resultRow);
  }

  return result;
});
const footerColSpan = computed(() => columns.length - footerTemplate?.columns.length);
const formatter = exportOptions?.formatter ? (await composableResolver.resolve(exportOptions.formatter)).default() : undefined;

if(exportOptions) {
  actions.value.push({
    label: l(exportOptions.buttonLabel),
    icon: exportOptions.buttonIcon,
    command: () => dataTable.value.exportCSV()
  });
}

function toggleActionsMenu(event) {
  actionsMenu.value.toggle(event);
}

function exportFunction({ data, field }) {
  if(!formatter) { return data.value; }

  return formatter.format(data.value, { prop: field, row: data.$getRow() });
}
</script>
