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
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.anErrorPage({
      errorInfos: [
        giveMe.anErrorPageInfo({
          statusCode: "403",
          title: "Spec: Access denied",
          message: "Spec: You do not have the permission to view the address or data specified",
          showSafeLinks: true
        })
      ],
      footerInfo: "Spec: Footer info",
      safeLinks: [
        giveMe.anExpected({ testId: "LINK_1", value: "VALUE_1" }),
        giveMe.anExpected({ testId: "LINK_2", value: "VALUE_2" })
      ],
      safeLinksMessage: "Spec: Safe links message",
      data: computed(() => ref(createError({ name:"NuxtError", statusCode: 403 })))
    })
  },
  {
    name: "503 custom exception",
    descriptor: giveMe.anErrorPage({
      errorInfos: [
        giveMe.anErrorPageInfo({
          statusCode: "503",
          title: "Spec: Service Unavailable",
          message: "Spec: The service is currently unavailable. Please try again later.",
          showSafeLinks: false,
          customMessage: true
        })
      ],
      footerInfo: "Spec: Footer info",
      safeLinks: [
        giveMe.anExpected({ testId: "LINK_1", value: "VALUE_1" }),
        giveMe.anExpected({ testId: "LINK_2", value: "VALUE_2" })
      ],
      safeLinksMessage: "Spec: Safe links message",
      data: computed(() => ref(createError({ name:"NuxtError", statusCode: 503, data: { detail: "Spec: Custom Exception Message" } })))
    })
  }
];
</script>
