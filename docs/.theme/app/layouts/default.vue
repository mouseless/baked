<template>
  <Header />
  <div
    v-if="$route.path === '/'"
    class="full"
   >
    <article>
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
</template>
<script setup>
import { withLeadingSlash } from "ufo";
import { useSectionStore } from "~/store/sectionStore";

const {sections: order} = await queryCollection("sectionOrder").first();
const menus = await queryCollection("sections").where("path", "<>", "/").all();

applyOrder(menus, i => withLeadingSlash(order[i]));
// get title from markdown h1 (first item (?.[0])'s value (?.[2]))
menus.forEach(menu => {
  menu.title = menu?.meta?.body?.value?.[0]?.[2] ?? menu.title;
});

useSectionStore().setSections(menus);
</script>
<style lang="scss" scoped>
.content, .full {
  @include width;

  & {
    margin: auto;
    margin-top: 0;
    margin-bottom: auto;
  }
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

@media (max-width: $width-page-s) {
  .content {
    display: block;
  }
}
</style>
