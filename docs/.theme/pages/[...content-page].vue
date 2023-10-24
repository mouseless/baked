<template>
  <ContentDoc>
    <template #default="{ doc }">
      <div class="container">
        <div class="content">
          <ContentRenderer
            :value="doc"
            class="toc-root"
          />
          <BottomNavigation />
        </div>
        <Toc :value="doc.body.toc" />
      </div>
    </template>
    <template #not-found>
      <ContentDoc path="/not-found" :head="false" />
    </template>
  </ContentDoc>
</template>
<script setup>
import { useRoute } from "#imports";
import { usePageStore } from "~/store/pageStore";

const route = useRoute();
const root = `/${route.path.split("/")[1]}`;
const store = usePageStore();

const index = await queryContent(root)
  .where({ _path: { $eq: root } })
  .only(["_path", "title", "pages", "sort"])
  .findOne();

let pages = await queryContent(root)
  .where({ _path: { $ne: root } })
  .only(["_path", "title"])
  .find();

index.pages
  ? pages = pageSorter(index, pages)
  : index.sort
    ? pages.sort((a, b) => autoSorter(a, b, index.sort.by, index.sort.order))
    : pages.sort();

const sortedPages = root === "/" ? [index] : [index, ...pages];

store.setPages(sortedPages);
</script>
<style lang="scss" scoped>
.container {
  display: flex;

  .content {
    width: 100%;
    max-width: $width-content;
    margin: 0 $width-content-margin;
  }
}

@media (max-width: $width-page-l) {
  .container {
    flex-direction: column-reverse;
    margin-left: $width-content-margin;

    .content {
      margin: 1em 0 0 0;
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
    margin-left: 0 !important;
    margin-right: 0 !important;
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
