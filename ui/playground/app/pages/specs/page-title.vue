<template>
  <UiSpec
    title="Page Title"
    :variants="variants"
    :no-loading-variant="true"
    test-id="Inputs"
  >
    <div>
      <h2 class="text-lg font-semibold mt-2">Inputs</h2>
      <Divider />
      <PageTitle :schema="inputsVariant.descriptor.schema">
        <template #inputs>
          <Inputs :inputs="inputsVariant.inputs" />
        </template>
      </PageTitle>
    </div>
  </UiSpec>
</template>
<script setup>
import giveMe from "@utils/giveMe";
import { Divider } from "primevue";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aPageTitle({
      title: "Spec: Title",
      description: "Spec: Description"
    })
  },
  {
    name: "Actions",
    descriptor: giveMe.aPageTitle({
      actions: [
        giveMe.aButton({ label: "ACTION_1" }),
        giveMe.aButton({ label: "ACTION_2" })
      ]
    })
  },
  {
    name: "No Description",
    descriptor: giveMe.aPageTitle({
      title: "Spec: Page Title",
      description: null
    })
  },
  {
    name: "Long Description",
    descriptor: giveMe.aPageTitle({
      description: "Spec: Lorem ipsum..."
    })
  }
];

const inputsVariant = {
  descriptor: giveMe.aPageTitle({
    actions: [giveMe.aButton({ label: "ACTION" })]
  }),
  inputs: [giveMe.anInput()]
};
</script>
