<template>
  <UiSpec
    title="Error Page"
    :variants="variants"
    no-loading-variant="true"
  />
</template>
<script setup>
import { ref } from "vue";
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
      errorResults: [
        { statusCode: 403, title: "Access Denied", message: "You do not have the permision to view the address or data specified." },
        { statusCode: 404, title: "Page Notfound", message: "The page you want to view is etiher deleted or outdated." },
        { statusCode: 500, title: "Unexpected Error", message: "Please contact system administrator." }
      ],
      data: ref(createError({ statusCode: 403 }))
    })
  }
];
</script>