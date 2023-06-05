<template>
  <nav>
    <h4>ON THIS PAGE</h4>
    <ul>
      <li
        v-for="link in value.links"
        :key="link.id"
      >
        <NuxtLink :to="`#${link.id}`">
          {{ link.text }}
        </NuxtLink>
        <ul>
          <li
            v-for="child in link.children"
            :key="child.id"
          >
            <NuxtLink :to="`#${child.id}`">
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

defineProps<{
  value: Toc
}>();
</script>
<style lang="scss" scoped>
nav {
  width: 250px;
  margin-top: 1.5em;
  margin-left: 4em;
  font-size: 80%;

  h4 {
    margin-top: 0;
    margin-bottom: 0.25em;
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
        text-decoration: none;
        color: $color-passive;

        &:hover {
          color: $color-brand;
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
