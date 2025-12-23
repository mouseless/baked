<template>
  <UiSpec
    title="Data Table"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";
import { provide, useLocalization } from "#imports";

provide("use-localization", useLocalization({ scope: "local" }));

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true, component: giveMe.anExpected({ testId: "label", data: giveMe.aContextData({ key: "parent", prop: "row.label" }) }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1", component: giveMe.anExpected({ testId: "prop-1", data: giveMe.aContextData({ key: "parent", prop: "row.data1" }) }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 2", key: "data2", component: giveMe.anExpected({ testId: "prop-2", data: giveMe.aContextData({ key: "parent", prop: "row.data2" }) }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 3", key: "data3", component: giveMe.anExpected({ testId: "prop-3", data: giveMe.aContextData({ key: "parent", prop: "row.data3" }) }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 4", key: "data4", component: giveMe.anExpected({ testId: "prop-4", data: giveMe.aContextData({ key: "parent", prop: "row.data4" }) }) })
      ],
      rowsWhenLoading: 3,
      data: [
        { label: "Row 1", data1: "Cell 1.1", data2: "Cell 1.2", data3: "Cell 1.3", data4: "Cell 1.4" },
        { label: "Row 2", data1: "Cell 2.1", data2: "Cell 2.2", data3: "Cell 2.3", data4: "Cell 2.4" },
        { label: "Row 3", data1: "Cell 3.1", data2: "Cell 3.2", data3: "Cell 3.3", data4: "Cell 3.4" },
        { label: "Row 4", data1: "Cell 4.1", data2: "Cell 4.2", data3: "Cell 4.3", data4: "Cell 4.4" },
        { label: "Row 5", data1: "Cell 5.1", data2: "Cell 5.2", data3: "Cell 5.3", data4: "Cell 5.4" }
      ]
    })
  },
  {
    name: "Pagination",
    descriptor: giveMe.aDataTable({
      paginator: true,
      dataKey: "data",
      rows: 2,
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Data", key: "data", component: giveMe.anExpected({ testId: "prop", data: giveMe.aContextData({ key: "parent", prop: "row.data" }) }) })
      ],
      data: [
        { data: "Row 1" },
        { data: "Row 2" },
        { data: "Row 3" }
      ]
    })
  },
  {
    name: "Auto Hide Pagination",
    descriptor: giveMe.aDataTable({
      paginator: true,
      rows: 1,
      columns: [ giveMe.aDataTableColumn({ key: "data" }) ],
      data: [ { data: "Row 1" } ]
    })
  },
  {
    name: "Footer",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true, component: giveMe.anExpected({ testId: "label" }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1", component: giveMe.anExpected({ testId: "prop-1" }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 2", key: "data2", component: giveMe.anExpected({ testId: "prop-2" }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 3", key: "data3", component: giveMe.anExpected({ testId: "prop-3" }) }),
        giveMe.aDataTableColumn({ title: "Spec: Data 4", key: "data4", component: giveMe.anExpected({ testId: "prop-4" }) })
      ],
      footerTemplate: {
        label: "Spec: Total",
        columns: [
          giveMe.aDataTableColumn({ key: "footer1", footer: true }),
          giveMe.aDataTableColumn({ key: "footer2", footer: true })
        ]
      },
      itemsProp: "children",
      rowsWhenLoading: 2,
      data: {
        children: [
          { label: "Row 1", data1: "Cell 1.1", data2: "Cell 1.2", data3: "1", data4: "10" },
          { label: "Row 2", data1: "Cell 2.1", data2: "Cell 2.2", data3: "2", data4: "20" }
        ],
        footer1: "3",
        footer2: "30"
      }
    })
  },
  {
    name: "Alignment",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data", key: "data", alignRight: true })
      ],
      footerTemplate: {
        label: "Total",
        columns: [
          giveMe.aDataTableColumn({ key: "label", alignRight: true, footer: true })
        ]
      },
      itemsProp: "children",
      rowsWhenLoading: 2,
      data: {
        children: [
          { label: "Row 1", data: "On" },
          { label: "Row 2", data: "Your" },
          { label: "Row 3", data: "Right" }
        ],
        label: "On Your Right"
      }
    })
  },
  {
    name: "Export",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true, exportable: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1", exportable: true })
      ],
      exportOptions: giveMe.aDataTableExport({
        csvSeparator: ";",
        formatter: "useCsvFormatter",
        buttonIcon: "pi pi-file-export",
        buttonLabel: "CSV"
      }),
      data: [
        { label: "Row 1", data1: "Cell 1.1" },
        { label: "Row 2", data1: "Cell 2.1" }
      ]
    })
  },
  {
    name: "Scroll",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1" })
      ],
      exportOptions: giveMe.aDataTableExport({
        csvSeparator: ";",
        formatter: "useCsvFormatter",
        buttonIcon: "pi pi-file-export",
        buttonLabel: "CSV"
      }),
      scrollable: true,
      scrollHeight: "200px",
      data: new Array(10).fill({ label: "Row Label", data1: "Cell Data" })
    })
  },
  {
    name: "Frozen Columns",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true, frozen: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 2", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 3", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 4", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 5", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 6", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 7", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 8", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 9", key: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 10", key: "data1" })
      ],
      exportOptions: giveMe.aDataTableExport({
        csvSeparator: ";",
        formatter: "useCsvFormatter",
        buttonIcon: "pi pi-file-export",
        buttonLabel: "CSV"
      }),
      scrollable: true,
      scrollHeight: "200px",
      data: new Array(2).fill({ label: "Row Label", data1: "Cell Data" })
    })
  },
  {
    name: "Virtual Scroll",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1" })
      ],
      scrollable: true,
      scrollHeight: "300px",
      virtualScrollerOptions: { itemSize: 45 },
      data: new Array(100).fill({ label: "Row Label", data1: "Cell Data" })
    })
  },
  {
    name: "No Virtual Scroll",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1" })
      ],
      scrollable: true,
      virtualScrollerOptions: { itemSize: 45 },
      data: new Array(20).fill({ label: "Row Label", data1: "Cell Data" })
    })
  },
  {
    name: "No Record Found",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label", component: giveMe.anExpected({ testId: "no-records" }) })
      ],
      data: []
    })
  },
  {
    name: "Row Actions",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", key: "label" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", key: "data1" })
      ],
      actions: giveMe.aDataTableColumn({
        component: giveMe.aButton({
          label: "Spec: Action",
          action: giveMe.aLocalAction({
            composable: "useShowMessage",
            options: giveMe.aContextData({ key: "parent", prop: "row", targetProp: "message" })
          }),
          variant: "text",
          rounded: true
        })
      }),
      rowsWhenLoading: 3,
      data: [
        { label: "Row 1", data1: "Cell 1.1" },
        { label: "Row 2", data1: "Cell 2.1" }
      ]
    })
  }
];
</script>
