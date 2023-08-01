<template>
  <ContentDoc v-if="!trailingSlash">
    <template #default="{ doc }">
      <div class="container">
        <div class="content">
          <ContentRenderer
            :value="doc"
            class="toc-root"
            :class="{ 'no-toc': doc.body.toc.links <= 0 }"
          />
          <BottomNavigation />
        </div>
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
    margin: 0 4em;
  }
}

@media (max-width: $width-page-l) {
  .container {
    flex-direction: column-reverse;
    margin-left: 4em;

    .content {
      margin: 0;
    }

    .no-toc {
      margin-top: 1em;
    }
  }
}

@media (max-width: $width-page-m) {
  .container {
    margin-left: 0;
  }
}
</style>
<style lang="scss">
.full {
  .content {
    margin: 0 !important;
  }
}

@media (max-width: $width-page-l) {
  .full {
    .container {
      margin-left: 0;
    }
  }
}

</style>
