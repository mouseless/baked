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
import { withTrailingSlash } from "ufo";
import { useRoute } from "#imports";
import { usePageStore } from "~/store/pageStore";

const route = useRoute();
const root = `/${route.path.split("/")[1]}`;
const store = usePageStore();

const index = await queryContent(root)
  .where({ _path: { $eq: root } })
  .only(["_path", "title", "pages", "sort"])
  .findOne();

const pages = await queryContent(root)
  .where({ _path: { $ne: root } })
  .only(["_path", "title"])
  .find();

if(index.pages) {
  applyOrder(pages, i => `${index._path}/${index.pages[i]}`);
} else {
  pages.sort((a, b) => compare(a, b, index.sort));
}

index._path = withTrailingSlash(index._path);

const sortedPages = root === "/" ? [index] : [index, ...pages];

store.setPages(sortedPages);

function compare(a, b, { by, order, numeric } = { }) {
  by ||= "title";
  order ||= "asc";
  numeric ||= false;

  const direction = order === "asc" ? 1 : -1;

  let valA;
  let valB;

  if(numeric) {
    for(const part of a[by].replace("v", "").split(".")) {
      valA += leftPad(part, 2);
    }

    for(const part of b[by].replace("v", "").split(".")) {
      valB += leftPad(part, 2);
    }
  } else {
    valA = a[by];
    valB = b[by];
  }

  if(valA < valB) { return -1 * direction; }
  if(valA > valB) { return 1 * direction; }

  return 0;
}

function leftPad(number, targetLength) {
  let output = number + "";
  while(output.length < targetLength) {
    output = "0" + output;
  }
  return output;
}
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
