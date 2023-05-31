<template>
  <ContentDoc v-if="!trailingSlash">
    <template #not-found>
      <!-- To avoid duplicate og meta data -->
      <ContentDoc path="/not-found" :head="false" />
    </template>
  </ContentDoc>
</template>
<script setup>
import { useRoute, navigateTo, onMounted } from "#imports";

const route = useRoute();
const trailingSlash = route.path !== "/" && route.path.endsWith("/");

onMounted(async () => {
  if(trailingSlash) {
    const { path, query, hash } = route;
    const nextPath = path.replace(/\/+$/, "");
    const nextRoute = { path: nextPath, query, hash };

    await navigateTo(nextRoute, { replace: true });
  }
});
</script>
