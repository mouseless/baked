<template>
  <DataTable
    ref="dataTable"
    :value
    class="text-sm min-h-20"
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
    <div
      v-if="sort || (!paginator && serverPaginatorOptions)"
      class="
        b-Header
        flex flex-row items-center justify-end
        gap-4 mb-2 py-4 px-2
      "
    >
      <div
        v-if="sort"
        class="flex items-end justify-end"
      >
        <Bake
          name="sort"
          :descriptor="sort"
        />
      </div>
      <div
        v-if="!paginator && serverPaginatorOptions"
        class="flex justify-end"
      >
        <ServerPaginator
          :schema="serverPaginatorOptions"
          :data="data"
        />
      </div>
    </div>
    <Column
      v-for="column in visibleColumns"
      :key="column.key"
      :header="l(column.title)"
      :field="column.key"
      class="text-nowrap"
      :exportable="column.exportable"
      :export-header="l(column.title)"
      :pt="{
        columnHeaderContent: { class: column.alignRight ? 'justify-end' : '' },
        bodyCell: { class: bodyCellClass(column) },
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
import { useComposableResolver, useContext, useDataFetcher, useLocalization } from "#imports";
import { AwaitLoading, Bake, ProvideParentContext, ServerPaginator } from "#components";

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

const { actions, columns, dataKey, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, serverPaginatorOptions, sort } = schema;
const exportOptions = schema.exportOptions && {
  buttonIcon: "pi pi-download",
  ...schema.exportOptions
};
const virtualScrollerOptions = schema.virtualScrollerOptions && {
  appendOnly: true,
  numToleratedItems: 10,
  ...schema.exportOptions
};

const contextData = context.injectContextData();
const dataDescriptor = context.injectDataDescriptor();

const LABEL_COLUMN_BP = 5;
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

const visibleColumns = computed(() => columns.filter(c => !c.hidden));
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
      formatter = composableResolver.resolve(formatterName).default();
    }

    let parameterFormatter = null;
    if(parameterFormatterName) {
      parameterFormatter = composableResolver.resolve(parameterFormatterName).default();
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

function bodyCellClass(column) {
  const classes = {
    "max-xs:!inset-auto": column.frozen,
    "min-w-40": column.minWidth,
    "text-right": column.alignRight
  };
  const total = visibleColumns.value.length;
  if(total === 1) { return classes; }

  const labelColumn = visibleColumns.value?.filter(vc => vc.frozen)?.slice(-1)[0];
  if(!labelColumn) { return classes; }

  const columnClass = total <= LABEL_COLUMN_BP
    ? "b-label-column--narrow"
    : "b-label-column--wide";

  if(column.key == labelColumn.key) {
    classes[columnClass] = true;
  }

  return classes;
}

function exportFunction({ data, field }) {
  if(!formatter) { return data.value; }

  return formatter.format(data.value, { prop: field, row: data.$getRow() });
}
</script>
<style>
/* If Datatable in a DataPanel, clear border and radius */
.p-panel-content:has(.b-component--DataTable) {
  .b-component--DataTable {
    @apply border-none rounded-none;

    .b-Header {
      @apply rounded-none;
    }
    .p-datatable-table-container {
      @apply border-none;
    }
  }
}

/* Datatable standalone styles */
.b-component--DataTable {
  @apply
    border border-slate-200 dark:border-zinc-700
    justify-self-center w-full
    rounded-[--p-border-radius-md]
  ;

  .p-datatable-table-container {
    @apply border-none rounded-b-[--p-border-radius-md];
  }

  &:has(.b-Header) {
    .b-Header {
      @apply
        border-b dark:border-zinc-800
        dark:bg-zinc-900 mb-0
        rounded-t-[--p-border-radius-md]
      ;
    }
    .p-datatable-table-container {
      @apply rounded-b-[--p-border-radius-md];
    }
  }

  &:has(:not(.b-Header)) {
    .p-datatable-table-container {
      @apply rounded-[--p-border-radius-md];
    }
  }

  &:has(.p-datatable-paginator-bottom) {
    .p-datatable-paginator-bottom {
      @apply
        border-t rounded-t-none
        rounded-b-[--p-border-radius-md] border-b-0
      ;
    }
    .p-datatable-table-container {
      @apply rounded-b-none;
    }
  }

  /* we clear the last tr border because of double border with
  dt and row. and add tfoot border if has any */
  tbody > tr:last-child > td {
    @apply border-b-0;
  }
  tfoot > tr > td{
    @apply border-t;
  }

  .b-label-column--wide {
    @apply 3xl:w-[30%] 2xl:w-[20%] xl:w-[15%];
  }
  .b-label-column--narrow {
    @apply 3xl:w-[40%] 2xl:w-[30%] xl:w-[20%];
  }

  a {
    @apply text-sm;
  }

  .p-button {
    @apply -my-2;
  }

  td.p-datatable-frozen-column {
    @apply z-[1];
  }
  .p-datatable-thead {
    @apply z-[2];
  }
}
</style>