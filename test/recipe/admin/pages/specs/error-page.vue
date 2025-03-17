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
      errorInfos: [
        giveMe.anErrorPageInfo({
          statusCode: "403",
          title: "Access Denied",
          message: "You do not have the permision to view the address or data specified."
        })
      ],
      footerInfo: "Footer info",
      safeLinks: [
        giveMe.anExpected({ testId: "LINK_1", value: "VALUE_1"}),
        giveMe.anExpected({ testId: "LINK_2", value: "VALUE_2"})
      ],
      safeLinksMessage: "Safe links message",
      data: computed(() => ref(createError({ name:"NuxtError", statusCode: 403 })))
    })
  }
];
</script>