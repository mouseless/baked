<template>
  <UiSpec
    title="Error Page"
    :variants="variants"
    :no-loading-variant="true"
  />
</template>
<script setup>
import { computed, ref } from "vue";
import { createError } from "#app";
import giveMe from "~/utils/giveMe";

const variants = [
  {
    name: "Basic",
    descriptor: giveMe.anErrorPage({
      safeLinks: [
        giveMe.aCardLink({ route: "/", title: "Home" }),
        giveMe.aCardLink({ route: "/specs", title: "Specs" })
      ],
      errorInfos: { "403": { title: "Access Denied", message: "You do not have the permision to view the address or data specified." } },
      data: computed(() => ref(createError({ name:"NuxtError", statusCode: 403 })))
    })
  }
];
</script>