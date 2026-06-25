<template>
  <div
    v-if="doc"
    class="
      doc flex
      max-lg:flex-col-reverse max-lg:ml-content-margin
      max-md:ml-0
    "
  >
    <div
      class="
        c--inner-content
        w-content my-0 mx-content-margin
        max-lg:mt-sm max-lg:mb-0 max-lg:mx-0
        max-sm:w-full
      "
    >
      <ContentRenderer
        :value="doc"
        class="toc-root"
      />
      <BottomNavigation />
    </div>
    <Toc v-if="$route.path !== '/'" :value="doc.body.toc" />
  </div>
  <ContentRenderer v-else :value="notFound" />
</template>
<script setup>
import { usePageStore } from "~/store/pageStore";
import { withoutTrailingSlash, withLeadingSlash } from "ufo";

const route = useRoute();
const root = computed(() => withLeadingSlash(route.path.split("/")[1]));

const doc = await queryCollection("content").path(route.path).first();
const notFound = await queryCollection("notFound").first();

const index = await queryCollection("pageData")
  .path(withLeadingSlash(root.value))
  .first();
const unorderedMenu = await queryCollection("pageData")
  .andWhere(query => query
    .where("id", "LIKE", `pageData${root.value}/%`)
    .where("path", "<>", root.value))
  .order("title", "ASC")
  .all();

// get title from markdown h1 (first item (?.[0])'s value (?.[2]))
index.title = index?.meta?.body?.value?.[0]?.[2] ?? index.title;
unorderedMenu.forEach(menu => {
  menu.title = menu?.meta?.body?.value?.[0]?.[2] ?? menu.title;
});

if(index?.pages)
{
  applyOrder(
    unorderedMenu,
    i => withoutTrailingSlash(`${root.value}/${index.pages[i]}`)
  );
} else {
  unorderedMenu.sort((a, b) => comparePages(a, b, index.sort ?? undefined));
}

usePageStore().setPages([index, ...unorderedMenu]);
</script>