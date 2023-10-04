<template>
  <div>
    <Header />
    <div v-if="$route.path === '/'">
      <article class="full">
        <slot />
      </article>
    </div>
    <div
      v-else
      class="content"
    >
      <Side class="side" />
      <article>
        <slot />
      </article>
    </div>
    <Footer />
  </div>
</template>
<script setup lang="ts">
import { useSectionStore } from "~/store/sectionStore";

const store = useSectionStore();

const index = await queryContent()
  .where({ _path: "/" })
  .only(["sections"])
  .findOne();

let sections = await queryContent("/")
  .only(["_path", "title", "_dir"])
  .where({
    _dir: { $eq: "" },
    _path: { $in: index.sections.map((section: any) => `/${section}`) }
  })
  .find();

sections = sectionSorter(index, sections);

store.setSections(sections);
</script>
<style lang="scss" scoped>
.content, .full {
  @include width;

  margin: auto;
  margin-top: 1em;
}

.content {
  display: grid;
  grid-template-areas:
      "side content"
      "side content";
  grid-template-rows: 65px 1fr;
  grid-template-columns: $width-side;
}

.side {
  grid-area: side;
}

article {
  grid-area: content;
}

@media (max-width: $width-page-l) {
  .content {
    margin-top: 0;
  }
}

@media (max-width: $width-page-m) {
  .side {
    z-index: 2;
    grid-column-end: content;
  }

  article {
    grid-column-start: side;
  }
}
</style>
