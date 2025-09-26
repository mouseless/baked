<template>
  <NuxtLayout name="default">
    <NuxtPage />
  </NuxtLayout>
</template>
<script setup>
import { joinURL } from "ufo";

const { public: { baseUrl } } = useRuntimeConfig();

useHead({
  link: [
    {
      rel: "icon",
      type: "image/x-icon",
      href: joinURL(baseUrl, "/favicon.ico")
    }
  ]
});
</script>