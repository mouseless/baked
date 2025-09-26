<template>
  <UiSpec
    title="Data Table"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";
import { provide, useLocalization } from "#imports";

provide("use-localization", useLocalization({scope: "local"}));

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true, component: giveMe.aConditional({ testId: "label" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1", component: giveMe.aConditional({ testId: "prop-1" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 2", prop: "data2", component: giveMe.aConditional({ testId: "prop-2" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 3", prop: "data3", component: giveMe.aConditional({ testId: "prop-3" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 4", prop: "data4", component: giveMe.aConditional({ testId: "prop-4" })})
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
    name: "Row Based Component",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({
          prop: "data",
          component: giveMe.aConditional({
            conditions: [
              giveMe.aConditionalCondition({ prop: "type", value: "type-1", testId: "component-1" }),
              giveMe.aConditionalCondition({ prop: "type", value: "type-2", testId: "component-2" })
            ]
          })
        })
      ],
      data: [
        { type: "type-1", data: "Data 1" },
        { type: "type-2", data: "Data 2" }
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
        giveMe.aDataTableColumn({ title: "Spec: Data", prop: "data", component: giveMe.aConditional({ testId: "prop" })})
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
      columns: [ giveMe.aDataTableColumn({ prop: "data" }) ],
      data: [ { data: "Row 1" } ]
    })
  },
  {
    name: "Footer",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true, component: giveMe.aConditional({ testId: "label" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1", component: giveMe.aConditional({ testId: "prop-1" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 2", prop: "data2", component: giveMe.aConditional({ testId: "prop-2" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 3", prop: "data3", component: giveMe.aConditional({ testId: "prop-3" })}),
        giveMe.aDataTableColumn({ title: "Spec: Data 4", prop: "data4", component: giveMe.aConditional({ testId: "prop-4" })})
      ],
      footerTemplate: {
        label: "Spec: Total",
        columns: [
          giveMe.aDataTableColumn({ prop: "footer1"}),
          giveMe.aDataTableColumn({ prop: "footer2"})
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data", prop: "data", alignRight: true })
      ],
      footerTemplate: {
        label: "Total",
        columns: [
          giveMe.aDataTableColumn({ prop: "label", alignRight: true })
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true, component: giveMe.aConditional({ testId: "label" }), exportable: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1", component: giveMe.aConditional({ testId: "prop-1" }), exportable: true })
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1" })
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true, frozen: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 2", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 3", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 4", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 5", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 6", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 7", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 8", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 9", prop: "data1" }),
        giveMe.aDataTableColumn({ title: "Spec: Data 10", prop: "data1" })
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1" })
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", minWidth: true }),
        giveMe.aDataTableColumn({ title: "Spec: Data 1", prop: "data1" })
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
        giveMe.aDataTableColumn({ title: "Spec: Label", prop: "label", component: giveMe.aConditional({ testId: "no-records" }) })
      ],
      data: []
    })
  }
];
</script>
