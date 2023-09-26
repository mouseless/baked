<template>
  <nav>
    <h4><a @click="toggle">On This Page</a></h4>
    <ul :class="{ active: shown }">
      <li
        v-for="link in value.links"
        :key="link.id"
      >
        <NuxtLink
          :to="`#${link.id}`"
          :class="{ active: link.id === activePageId }"
          @click="close"
        >
          {{ link.text }}
        </NuxtLink>
        <ul>
          <li
            v-for="child in link.children"
            :key="child.id"
          >
            <NuxtLink
              :to="`#${child.id}`"
              :class="{ active: child.id === activePageId }"
              @click="close"
            >
              {{ child.text }}
            </NuxtLink>
          </li>
        </ul>
      </li>
      <li>
        <a
          class="return-to-top"
          href="#"
          @click="close"
        >Return to top</a>
      </li>
    </ul>
  </nav>
</template>
<script lang="ts" setup>
import type { Toc } from "@nuxt/content/dist/runtime/types";
import { onMounted, onBeforeUnmount } from "#imports";

defineProps<{
  value: Toc
}>();

let observer: IntersectionObserver;
const activePageId = ref<string>("");
const shown = ref<boolean>(false);

function toggle() { shown.value = !shown.value; }
function close() { shown.value = false; }

onMounted(() => {
  observer = new IntersectionObserver(onIntersection, { root: document, rootMargin: "-75px" });

  const pageCounts: { [id:string]: number } = { };
  const pageIndices: { [id:string]: number } = { };

  document
    .querySelectorAll(".toc-root > h2[id], .toc-root > h3[id]")
    .forEach((page, index) => {
      const id = page.getAttribute("id") as string;
      pageIndices[id] = index;
    });

  document
    .querySelectorAll(".toc-root > *")
    .forEach(page => observer.observe(page));

  function onIntersection(entries: IntersectionObserverEntry[]) {
    const entriesWithId = [];
    for(const entry of entries) {
      const id = findPageId(entry.target);
      if(id) {
        entriesWithId.push({ id, entry });
      }
    }

    for(const { entry, id } of entriesWithId) {
      pageCounts[id] ||= 0;
      if(entry.isIntersecting) {
        pageCounts[id] += 1;
      } else {
        pageCounts[id] -= 1;
      }
    }

    for(const id of Object.keys(pageCounts)) {
      if(pageCounts[id] <= 0) {
        delete pageCounts[id];
      }
    }

    const activePages = Object.keys(pageCounts);
    activePages.sort((l, r) => pageIndices[l] - pageIndices[r]);

    activePageId.value = activePages[0] || "";
  }

  function findPageId(element: Element): (string | null) {
    while(pageIndices[element.getAttribute("id") || ""] === undefined) {
      if(!element.previousElementSibling) {
        return null;
      }

      element = element.previousElementSibling;
    }

    return element.getAttribute("id");
  }
});

onBeforeUnmount(() => {
  observer.disconnect();
});
</script>
<style lang="scss" scoped>
h4 {
  margin-bottom: 0.5em;
  padding-left: 1em;
}

nav {
  position: sticky;
  align-self: start;
  top: 1.5em;
  width: 250px;
  margin-top: 2.5em;
  font-size: 12.8px;
  overflow: hidden;
  text-wrap: nowrap;

  ul {
    margin: 0;
    padding-left: 0;

    li {
      margin: 0;
      list-style: none;
      line-height: 24px;

      a {
        @include theme('color', $theme-color-fg-passive);
        text-decoration: none;
        cursor: pointer;
        display: block;
        margin-top: 0.25em;
        overflow: hidden;
        text-overflow: ellipsis;
        padding-left: 1em;
        border-left: solid 3px #00000000;

        &:hover {
          @include theme('color', $theme-color-brand);
        }

        &.active {
          @include theme('border-left-color', $theme-color-brand);
          @include theme('color', $theme-color-fg-active);
        }

        &.return-to-top {
          margin-top: 0.50em;
        }
      }

      ul {
        margin-bottom: 0.25em;

        a {
          padding-left: 2em;
        }
      }
    }
  }
}

@media (max-width: $width-page) {
  a {
    font-size: 12.8px !important;
  }
}

@media (max-width: $width-page-l) {
  nav {
    @include theme('background-color', $theme-color-bg-body);
    @include theme('color', $theme-color-bg-body);

    text-align: right;
    width: 100%;
    top: 0;
    margin: 0;
    box-shadow: 0 5px 5px 0;
    margin-bottom: -2.5em;

    h4 a {
      display: inline-block;
      padding-right: 0;
      padding-left: 0;
    }

    & > ul {
      @include box;
      display: none;
      text-align: left;
      padding: 1em;
      padding-left: 0em;
      margin-bottom: 2.5em;

      &.active {
        display: block;
      }
    }

    ul li a.active:before {
      left: 0em;
    }
  }
}
</style>
