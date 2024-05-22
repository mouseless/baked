<template>
  <nav>
    <h4>
      <a v-if="value.links.length > 0" @click="toggle">On This Page</a>
      <a v-else>&nbsp;</a>
    </h4>
    <ul
      v-if="value.links.length > 0"
      :class="{ active: shown }"
    >
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
  margin-bottom: $space-xs;
  padding-left: $space-sm;
  font-size: 1em;
}

nav {
  position: sticky;
  align-self: start;
  top: $space-sm;
  width: $width-side;
  margin-top: $space-md;
  font-size: 0.9em;
  overflow: hidden;
  white-space: nowrap;

  ul {
    margin: 0;
    padding-left: 0;

    li {
      margin: 0;
      list-style: none;
      line-height: 24px;

      a {
        color: $color-fg-second;
        text-decoration: none;
        cursor: pointer;
        display: block;
        margin-top: $space-xs;
        overflow: hidden;
        text-overflow: ellipsis;
        padding-left: $space-sm;
        border-left: solid 2px transparent;

        &:hover {
          color: $color-brand;
        }

        &.active {
          border-left-color: $color-bg-third;
        }

        &.return-to-top {
          margin-top: $space-sm;
        }
      }

      ul {
        margin-bottom: $space-xs;

        a {
          padding-left: calc($space-sm + 1em);
        }
      }
    }
  }
}

@media (max-width: $width-page-l) {
  nav {
    text-align: right;
    width: 100%;
    top: 0;
    background-color: $color-bg;
    margin: 0;
    margin-bottom: -52px;
    font-size: 0.9em;

    h4 a {
      display: inline-block;
      padding-right: 0;
      padding-left: 0;

      &:hover {
        color: $color-brand;
      }
    }

    & > ul {
      border-radius: $space-sm;
      background-color: $color-bg-nav;
      display: none;
      text-align: left;
      padding: $space-sm;
      padding-left: 0;
      margin-bottom: $space-md;

      &.active {
        display: block;
      }
    }

    ul li {
      a {
        padding-left: $space-sm;

        &.active:before {
          left: 0;
        }
      }

      ul li a {
        padding-left: calc($space-sm + 1em);
      }
    }
  }
}
</style>
