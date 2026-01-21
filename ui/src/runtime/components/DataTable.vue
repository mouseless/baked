<template>
  <DataTable
    ref="dataTable"
    :value
    class="text-sm min-h-24"
    striped-rows
    :data-key
    :paginator="paginator && value.length > rows"
    :paginator-template="{
      [screens['2xs']]: 'JumpToPageDropdown',
      [screens.xs]: 'FirstPageLink PrevPageLink JumpToPageDropdown NextPageLink LastPageLink',
      default: 'FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink'
    }"
    :rows
    scrollable
    :scroll-height
    :virtual-scroller-options="scrollHeight ? virtualScrollerOptions : null"
    :csv-separator="exportOptions?.csvSeparator"
    :export-filename
    :export-function
  >
    <template #empty>
      {{ lc("No records found") }}
    </template>
    <Column
      v-for="column in columns"
      :key="column.key"
      :header="l(column.title)"
      :field="column.key"
      class="text-nowrap"
      :class="{ 'min-w-40': column.minWidth, 'text-right': column.alignRight }"
      :exportable="column.exportable"
      :export-header="l(column.title)"
      :pt="{
        columnHeaderContent: { class: column.alignRight ? 'justify-end' : '' },
        bodyCell: { class: { 'max-xs:!inset-auto': column.frozen } },
        headerCell: { class: { 'max-xs:!inset-auto': column.frozen } }
      }"
      :frozen="column.frozen"
    >
      <template #body="{ data: row, index }">
        <AwaitLoading :skeleton="{ class: 'min-h-5' }">
          <ProvideParentContext
            v-if="data"
            :data="row.$getRow()"
            data-key="row"
          >
            <Bake
              :name="`rows/${index}/${column.key}`"
              :descriptor="column.component"
            />
          </ProvideParentContext>
          <span v-else>-</span>
        </AwaitLoading>
      </template>
    </Column>
    <Column
      v-if="exportOptions || actions"
      :pt="{
        bodyCell: { class: 'max-xs:!inset-auto' },
        headerCell: { class: 'max-xs:!inset-auto' },
        columnHeaderContent: 'justify-end'
      }"
      :exportable="false"
      class="w-0 py-0"
      frozen
      align-frozen="right"
    >
      <template
        v-if="exportOptions"
        #header
      >
        <Button
          type="button"
          icon="pi pi-ellipsis-v"
          severity="secondary"
          variant="text"
          size="small"
          @click="toggleHeaderActionsMenu"
        />
        <Menu
          ref="headerActionsMenu"
          :model="headerActions"
          :popup="true"
        />
      </template>
      <template
        v-if="actions"
        #body="{ data: row, index }"
      >
        <AwaitLoading :skeleton="{ class: 'min-h-5' }">
          <ProvideParentContext
            v-if="data"
            :data="row.$getRow()"
            data-key="row"
          >
            <div class="flex">
              <Bake
                :name="`rows/${index}/actions`"
                :descriptor="actions.component"
              />
            </div>
          </ProvideParentContext>
        </AwaitLoading>
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
          :key="column.key"
          :class="{ 'text-right': column.alignRight }"
        >
          <template #footer>
            <AwaitLoading :skeleton="{ class: 'min-h-5' }">
              <Bake
                v-if="data"
                :name="`rows/footer/${column.key}`"
                :descriptor="column.component"
              />
              <span v-else>-</span>
            </AwaitLoading>
          </template>
        </Column>
        <Column
          v-if="exportOptions || actions"
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
import { computed, onMounted, ref } from "vue";
import Column from "primevue/column";
import { Button, ColumnGroup, DataTable, Menu, Row } from "primevue";
import { useRuntimeConfig } from "#app";
import { AwaitLoading, Bake, ProvideParentContext } from "#components";
import { useComposableResolver, useContext, useDataFetcher, useLocalization } from "#imports";

const context = useContext();
const composableResolver = useComposableResolver();
const dataFetcher = useDataFetcher();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "DataTable" });
const { public: { composables: { useBreakpoints: { screens } } } } = useRuntimeConfig();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { actions, columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, virtualScrollerOptions } = schema;

const contextData = context.injectContextData();
const dataDescriptor = context.injectDataDescriptor();

const dataTable = ref();
const headerActionsMenu = ref();
const headerActions = ref([ ]);
const value = computed(() => {
  const items = data
    ? itemsProp
      ? data[itemsProp]
      : data
    : new Array(rowsWhenLoading || 5).fill({ });

  const result = [];
  for(const itemRow of items) {
    const resultRow = { $getRow: () => itemRow };

    for(const key in itemRow) {
      resultRow[key] = { value: itemRow[key], $getRow: () => itemRow };
    }

    result.push(resultRow);
  }

  return result;
});
const footerColSpan = computed(() => columns.length - footerTemplate?.columns.length);
const exportFilename = ref(exportOptions?.fileName ? l(exportOptions.fileName) : null);
let formatter = null;

if(exportOptions) {
  headerActions.value.push({
    label: l(exportOptions.buttonLabel),
    icon: exportOptions.buttonIcon,
    command: () => dataTable.value.exportCSV()
  });
}

onMounted(async() => {
  if(exportOptions) {
    const {
      formatter: formatterName,
      appendParameters,
      parameterSeparator,
      parameterFormatter: parameterFormatterName
    } = exportOptions;

    if(formatterName) {
      formatter = (await composableResolver.resolve(formatterName)).default();
    }

    let parameterFormatter = null;
    if(parameterFormatterName) {
      parameterFormatter = (await composableResolver.resolve(parameterFormatterName)).default();
    }

    if(appendParameters && dataDescriptor) {
      let parameters = await dataFetcher.fetchParameters({ data: dataDescriptor, contextData });
      if(parameterFormatter) {
        parameters = parameters.map((p, i) => parameterFormatter.format(p, i));
      }

      exportFilename.value = [exportFilename.value, ...parameters].join(parameterSeparator ?? "-");
    }
  }
});

function toggleHeaderActionsMenu(event) {
  headerActionsMenu.value.toggle(event);
}

function exportFunction({ data, field }) {
  if(!formatter) { return data.value; }

  return formatter.format(data.value, { prop: field, row: data.$getRow() });
}
</script>
<style>
.b-component--DataTable {
  a {
    @apply text-sm;
  }

  .p-button {
    @apply -my-2;
  }
}
</style>
