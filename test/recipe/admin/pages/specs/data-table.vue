<template>
  <UiSpec
    title="Data Table"
    :variants="variants"
  />
</template>
<script setup>
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aDataTable({
      columns: [
        giveMe.aDataTableColumn({ title: "Label", prop: "label", minWidth: true, component: giveMe.anExpected({ testId: "label" })}),
        giveMe.aDataTableColumn({ title: "Data 1", prop: "data1", component: giveMe.anExpected({ testId: "prop-1" })}),
        giveMe.aDataTableColumn({ title: "Data 2", prop: "data2", component: giveMe.anExpected({ testId: "prop-2" })}),
        giveMe.aDataTableColumn({ title: "Data 3", prop: "data3", component: giveMe.anExpected({ testId: "prop-3" })}),
        giveMe.aDataTableColumn({ title: "Data 4", prop: "data4", component: giveMe.anExpected({ testId: "prop-4" })})
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
          conditionalComponents: [
            giveMe.aDataTableColumnConditionalComponent({ prop: "type", value: "type-1", testId: "component-1" }),
            giveMe.aDataTableColumnConditionalComponent({ prop: "type", value: "type-2", testId: "component-2" })
          ]
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
        giveMe.aDataTableColumn({ title: "Data", prop: "data", component: giveMe.anExpected({ testId: "prop" })})
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
  }
];
</script>
