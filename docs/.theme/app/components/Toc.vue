<template>
  <nav
    class="
      sticky self-start top-sm
      min-w-side mt-md text-[0.9em]
      font-default whitespace-nowrap
      max-lg:text-right max-lg:text-[0.9em]
      max-lg:w-full max-lg:top-0
      max-lg:m-0 max-lg:mb-[-52px]
      max-lg:bg-bg
    "
  >
    <h4 class="mb-xs pl-sm text-[1em]">
      <a
        v-if="value.links.length > 0"
        class="
          max-lg:inline-block! max-lg:pr-0! max-lg:pl-0!
          hover:max-lg:text-brand!
        "
        @click="toggle"
      >On This Page</a>
      <a v-else>&nbsp;</a>
    </h4>
    <ul
      v-if="value.links.length > 0"
      :class="{ 'max-lg:block!': shown }"
      class="
        max-h-[calc(100vh-5rem)] overflow-y-auto
        [&::-webkit-scrollbar]:hidden
        m-0 pl-0!
        max-lg:rounded-sm max-lg:bg-bg-nav
        max-lg:hidden max-lg:text-left max-lg:p-sm
        max-lg:pl-0 max-lg:mb-md max-lg:max-h-(--max-height-list)
      "
    >
      <li
        v-for="link in value.links"
        :key="link.id"
        class="m-0 list-none"
      >
        <NuxtLink
          :to="`#${link.id}`"
          :class="{ 'border-l-brand! max-lg:before:left-0': link.id === activePageId }"
          class="
            block no-underline pointer
            text-fg-second text-ellipsis
            mt-xs pl-sm overflow-hidden
            border-l-2 border-l-transparent
            hover:text-brand max-lg:pl-sm
          "
          @click="close"
        >
          {{ link.text }}
        </NuxtLink>
        <ul
          v-show="link.id === activePageId || link.children?.some(c => c.id === activePageId)"
          class="m-0 pl-0! mb-xs"
        >
          <li
            v-for="child in link.children"
            :key="child.id"
          >
            <NuxtLink
              :to="`#${child.id}`"
              :class="{ 'border-l-brand! max-lg:before:left-0': child.id === activePageId }"
              class="
                pl-[calc(var(--space-sm)+1em)]!
                block no-underline pointer
                text-fg-second text-ellipsis
                mt-xs pl-sm overflow-hidden
                border-l-2 border-l-transparent
                hover:text-brand max-lg:pl-sm
                max-lg:pl-sm
              "
              @click="close"
            >
              {{ child.text }}
            </NuxtLink>
          </li>
        </ul>
      </li>
      <li class="m-0 list-none">
        <a
          class="
            block no-underline pointer
            text-fg-third text-ellipsis
            mt-sm pl-sm overflow-hidden
            border-l-2 border-l-transparent
            max-lg:pl-sm
          "
          href="#"
          @click="close"
        >Return to top</a>
      </li>
    </ul>
  </nav>
</template>
<script setup>
import { onMounted, onBeforeUnmount } from "#imports";

defineProps({
  value: {
    type: Object,
    default: () => {}
  }
});

let observer;
const activePageId = ref("");
const shown = ref(false);

function toggle() { shown.value = !shown.value; }
function close() { shown.value = false; }

onMounted(() => {
  observer = new IntersectionObserver(onIntersection, { root: document, rootMargin: "-75px" });

  const pageCounts = { };
  const pageIndices = { };

  document
    .querySelectorAll(".toc-root > h2[id], .toc-root > h3[id]")
    .forEach((page, index) => {
      const id = page.getAttribute("id");
      pageIndices[id] = index;
    });

  document
    .querySelectorAll(".toc-root > *")
    .forEach(page => observer.observe(page));

  function onIntersection(entries) {
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

  function findPageId(element) {
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