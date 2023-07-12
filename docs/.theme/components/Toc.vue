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
          :class="{ active: link.id === activeSectionId }"
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
              :class="{ active: child.id === activeSectionId }"
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
const activeSectionId = ref<string>("");
const shown = ref<boolean>(false);

function toggle() { shown.value = !shown.value; }
function close() { shown.value = false; }

onMounted(() => {
  observer = new IntersectionObserver(onIntersection, { root: document, rootMargin: "-75px" });

  const sectionCounts: { [id:string]: number } = { };
  const sectionIndices: { [id:string]: number } = { };

  document
    .querySelectorAll(".toc-root > h2[id], .toc-root > h3[id]")
    .forEach((section, index) => {
      const id = section.getAttribute("id") as string;
      sectionIndices[id] = index;
    });

  document
    .querySelectorAll(".toc-root > *")
    .forEach(section => observer.observe(section));

  function onIntersection(entries: IntersectionObserverEntry[]) {
    const entriesWithId = [];
    for(const entry of entries) {
      const id = findSectionId(entry.target);
      if(id) {
        entriesWithId.push({ id, entry });
      }
    }

    for(const { entry, id } of entriesWithId) {
      sectionCounts[id] ||= 0;
      if(entry.isIntersecting) {
        sectionCounts[id] += 1;
      } else {
        sectionCounts[id] -= 1;
      }
    }

    for(const id of Object.keys(sectionCounts)) {
      if(sectionCounts[id] <= 0) {
        delete sectionCounts[id];
      }
    }

    const activeSections = Object.keys(sectionCounts);
    activeSections.sort((l, r) => sectionIndices[l] - sectionIndices[r]);

    activeSectionId.value = activeSections[0] || "";
  }

  function findSectionId(element: Element): (string | null) {
    while(sectionIndices[element.getAttribute("id") || ""] === undefined) {
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
        color: $color-fg-passive;
        text-decoration: none;
        cursor: pointer;
        display: block;
        margin-top: 0.25em;
        overflow: hidden;
        text-overflow: ellipsis;
        padding-left: 1em;
        border-left: solid 3px #00000000;

        &:hover {
          color: $color-brand;
        }

        &.active {
          border-left-color: $color-brand;
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
    text-align: right;
    width: 100%;
    top: 0;
    background-color: $color-bg-body;
    margin: 0;
    box-shadow: 0 5px 5px 0 $color-bg-body;
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
