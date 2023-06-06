<template>
  <ContentDoc
    v-if="!trailingSlash"
  >
    <template #default="{ doc }">
      <div class="container">
        <ContentRenderer
          :value="doc"
          class="content toc-root"
        />
        <Toc
          v-if="doc.body.toc.links.length > 0"
          :value="doc.body.toc"
        />
      </div>
    </template>
    <template #not-found>
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
<style lang="scss" scoped>
.container {
  display: flex;

  .content {
    width: 100%;
  }
}
</style>
