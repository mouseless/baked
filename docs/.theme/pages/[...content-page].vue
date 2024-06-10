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
        <Toc v-if="$route.path !== '/'" :value="doc.body.toc" />
      </div>
    </template>
    <template #not-found>
      <ContentDoc path="/not-found" :head="false" />
    </template>
  </ContentDoc>
</template>
<script setup>
import { clean, compare } from "semver";
import { withTrailingSlash } from "ufo";
import { useRoute } from "#imports";
import { usePageStore } from "~/store/pageStore";

const route = useRoute();
const store = usePageStore();

const root = `/${route.path.split("/")[1]}`;

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
  pages.sort((a, b) => comparePages(a, b, index.sort));
}

index._path = withTrailingSlash(index._path);

const sortedPages = root === "/" ? [index] : [index, ...pages];

store.setPages(sortedPages);

function comparePages(a, b, { by, order, version } = { }) {
  by ||= "title";
  order ||= "asc";
  version ||= false;

  const direction = order === "asc" ? 1 : -1;

  if(version) {
    return compare(clean(a[by] + ".0"), clean(b[by] + ".0")) * direction;
  } else {
    if(a[by] < b[by]) { return -1 * direction; }
    if(a[by] > b[by]) { return 1 * direction; }
  }

  return 0;
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
      margin: $space-sm 0 0 0;
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
  .container {
    justify-content: center;

    .content {
      max-width: $width-page;
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
      }
    }
  }
}

@media (max-width: $width-page-l) {
  .full {
    .container {
      flex-direction: row;
      margin-left: 0;
    }
  }
}
</style>
