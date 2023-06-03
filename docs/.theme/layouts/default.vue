<template>
  <div>
    <div class="top">
      <header>
        <div class="logo">
          <NuxtLink to="/">
            <img class="do logo">
          </NuxtLink>
        </div>
        <nav>
          <ContentQuery
            v-slot="{ data: menus }"
            path="/"
            :only="[ '_path', 'title', 'position' ]"
            :where="{ _dir: { $eq: '' }, _path: { $ne: '/' }, position: { $exists: true } }"
            :sort="sort"
          >
            <NuxtLink
              v-for="menu in menus"
              :key="menu.title"
              :to="menu._path == $route.path ? '' : menu._path"
            >
              {{ menu.title }}
            </NuxtLink>
          </ContentQuery>
        </nav>
      </header>
    </div>
    <article>
      <slot />
    </article>
  </div>
</template>
<script setup lang="ts">
const sort = {
  position: 1,
  $numeric: true
};
</script>
<style scoped lang="scss">
div.top {
  @include border(bottom);
}

header, article {
  max-width: 1768px;
  margin: auto;
  padding: 0 10px;
}

header {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

article {
  padding-top: 10px;
}

div.logo {
  margin: 20px 0px;

  a:has(img.logo) {
    display: block;
    height: 25px;

    img.do {
      &:is(.logo),
      &:is(.logo:is(.full)) {
        height: 25px;
        content: url(./logo-full-primary.svg);
        display: inline-block;
      }

      &:is(.logo:is(.mark)) {
        content: url(./logo-mark-primary.svg);
      }

      @media (max-width: 800px) {
        &:is(.logo) {
          height: 15px;
        }
      }
    }
  }
}

nav a {
  margin: 10px;
  text-decoration: none;

  &:hover, &:not([href]) {
    color: $color-brand;
  }

  @media (max-width: 800px) {
    & {
      display: block;
    }
  }
}
</style>
