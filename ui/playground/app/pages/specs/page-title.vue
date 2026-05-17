<template>
  <UiSpec
    :variants
    no-loading-variant
    test-id="Inputs"
  >
    <div>
      <h2 class="text-lg font-semibold mt-2">Inputs</h2>
      <Divider />
      <Bake
        name="inputs-variant"
        :descriptor="inputsVariant.descriptor"
      >
        <template #inputs>
          <Inputs :inputs="inputsVariant.inputs" />
        </template>
      </Bake>
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
        giveMe.aButton({ label: "ACTION_2", icon: "pi pi-heart" })
      ],
      earlyWrapActionsAt: 2
    })
  },
  {
    name: "Dynamic",
    descriptor: giveMe.aPageTitle({
      title: { name: "From Data", info1: "info-1", info2: "info-2" },
      titleProp: "name",
      localizeTitle: false,
      icon: giveMe.anExpected({ value: "PT", testId: "icon" }),
      infoFields: [
        giveMe.aField({ key: "info1", label: "Spec: Info 1", testId: "info-1" }),
        giveMe.aField({ key: "info2", label: "Spec: Info 2", testId: "info-2" })
      ],
      actions: [ giveMe.aButton({ label: "ACTION" }) ]
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