<template>
  <nav>
    <h4><a href="#">On This Page</a></h4>
    <ul>
      <li
        v-for="link in value.links"
        :key="link.id"
      >
        <NuxtLink
          :to="`#${link.id}`"
          :class="{ active: link.id === activeSectionId }"
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
            >
              {{ child.text }}
            </NuxtLink>
          </li>
        </ul>
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

onMounted(() => {
  observer = new IntersectionObserver(onIntersection, { root: document });

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
      if(entry.isIntersecting) {
        sectionCounts[id] ||= 0;
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
nav {
  position: sticky;
  align-self: start;
  top: 1.5em;
  width: 250px;
  margin: 0 1em;
  margin-top: 2.5em;
  font-size: 80%;

  @media(max-width: 1280px) {
    width: 150px;
    font-size: 60%;
  }

  h4 {
    margin-top: 0;
    margin-bottom: 0.25em;
    text-transform: uppercase;

    a {
      color: $color-brand;

      &:hover {
        text-decoration: underline;
      }
    }
  }

  a {
    color: $color-fg-passive;
    text-decoration: none;

    &:hover {
      color: $color-brand;
    }
  }

  ul {
    margin: 0;
    padding-left: 0;

    li {
      margin: 0;
      list-style: none;

      a {
        display: inline-block;
        margin-top: 0.25em;

        &.active:before {
          position: absolute;
          left: -1em;
          content: "."; /* . acts as a placeholder */
          color: #00000000;
          background-color: $color-brand;
          @include radius();
          width: 3px;
        }
      }

      ul {
        margin-bottom: 0.25em;
        padding-left: 1em;
      }
    }
  }
}
</style>
