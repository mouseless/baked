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
        giveMe.aDataTableColumn({ title: "Label", prop: "label", minWidth: true, content: giveMe.anExpected({ testId: "label" })}),
        giveMe.aDataTableColumn({ title: "Data 1", prop: "data1", content: giveMe.anExpected({ testId: "prop-1" })}),
        giveMe.aDataTableColumn({ title: "Data 2", prop: "data2", content: giveMe.anExpected({ testId: "prop-2" })}),
        giveMe.aDataTableColumn({ title: "Data 3", prop: "data3", content: giveMe.anExpected({ testId: "prop-3" })}),
        giveMe.aDataTableColumn({ title: "Data 4", prop: "data4", content: giveMe.anExpected({ testId: "prop-4" })})
      ],
      rowCountWhenLoading: 3,
      data: [
        { label: "Row 1", data1: "Cell 1.1", data2: "Cell 1.2", data3: "Cell 1.3", data4: "Cell 1.4" },
        { label: "Row 2", data1: "Cell 2.1", data2: "Cell 2.2", data3: "Cell 2.3", data4: "Cell 2.4" },
        { label: "Row 3", data1: "Cell 3.1", data2: "Cell 3.2", data3: "Cell 3.3", data4: "Cell 3.4" },
        { label: "Row 4", data1: "Cell 4.1", data2: "Cell 4.2", data3: "Cell 4.3", data4: "Cell 4.4" },
        { label: "Row 5", data1: "Cell 5.1", data2: "Cell 5.2", data3: "Cell 5.3", data4: "Cell 5.4" }
      ]
    })
  }
];
</script>
