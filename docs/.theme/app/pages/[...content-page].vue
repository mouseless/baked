<template>
  <div v-if="doc" class="container">
    <div class="content">
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
const unOrderedMenus = await queryCollection("pageData")
  .andWhere(query => query
    .where("id", "LIKE", `pageData${root.value}/%`)
    .where("path", "<>", root.value))
  .order("title", "ASC")
  .all();

if(index?.pages)
{
  applyOrder(
    unOrderedMenus,
    i => withoutTrailingSlash(`${root.value}/${index.pages[i]}`)
  );
} else {
  unOrderedMenus.sort((a, b) => comparePages(a, b, index.sort ?? undefined));
}

usePageStore().setPages([index, ...unOrderedMenus]);
</script>
<style lang="scss">
.container {
  display: flex;

  .content {
    width: $width-content;
    margin: 0 $width-content-margin;
  }
}

.full {
  .container {
    justify-content: center;

    .content {
      width: $width-page;
      margin-left: 0;
      margin-right: 0;

      h1+h2 {
        margin-top: -0.5em;
      }

      h1, h2 { line-height: 1.2em; }

      h1 { color: var(--color-red-0); }
      h2 { color: var(--color-red-n3); }
      h3 { color: var(--color-red-n1); }

      a:not(.external) {
        display: inline-block;
        padding: var(--space-xs) var(--space-sm);
        background-color: var(--color-darkgreen-700);
        color: var(--color-gray-200);
        border-radius: var(--space-xs);
        text-decoration: none;

        &:hover {
          color: var(--color-green-0);
        }

        &:not(:first-child) {
          margin-left: $space-xs;
        }
      }
    }
  }
}

@media (max-width: $width-page-l) {
  .container {
    flex-direction: column-reverse;
    margin-left: $width-content-margin;

    .content {
      margin: $space-sm 0 0 0;
    }
  }

  .full {
    .container {
      flex-direction: row;
      margin-left: 0;
    }
  }
}

@media (max-width: $width-page-m) {
  .container {
    margin-left: 0;
  }
}
</style>
