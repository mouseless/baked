<template>
  <Header />
  <div
    v-if="$route.path === '/'"
    class="
      full
      box-border m-auto mt-0
      min-w-page-min max-w-page-xl
      max-xl:max-w-page-xl
      max-lg:max-w-page-l
      max-md:max-w-page-m
      max-sm:max-w-page-s
    "
   >
    <article>
      <slot />
    </article>
  </div>
  <div
    v-else
    class="
      c--content content
      box-border m-auto mt-0
      min-w-page-min max-w-page-xl
      max-xl:max-w-page-xl
      max-lg:max-w-page-l max-lg:mt-0
      max-md:max-w-page-m
      max-sm:max-w-page-s max-sm:block
    "
  >
    <Side class="side max-md:z-[2]" />
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