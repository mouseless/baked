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
    name: "Base",
    descriptor: giveMe.anErrorPage({
      errorInfos: [
        giveMe.anErrorPageInfo({
          statusCode: "403",
          title: "Access_Denied",
          message: "You_do_not_have_the_permision_to_view_the_address_or_data_specified"
        })
      ],
      footerInfo: "Footer_info",
      safeLinks: [
        giveMe.anExpected({ testId: "LINK_1", value: "VALUE_1"}),
        giveMe.anExpected({ testId: "LINK_2", value: "VALUE_2"})
      ],
      safeLinksMessage: "Safe_links_message",
      data: computed(() => ref(createError({ name:"NuxtError", statusCode: 403 })))
    })
  }
];
</script>
