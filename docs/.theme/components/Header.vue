<template>
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
</template>
<script lang="ts" setup>
const sort = {
  position: 1,
  $numeric: true
};
</script>
<style lang="scss" scoped>
div.top {
  @include border(bottom);
}

header {
  width: $width-page;
  margin: auto;
  padding: 0 10px;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

div.logo {
  margin: 20px 0px;

  a:has(img.logo) {
    display: block;
    height: 25px;

    img.do {
      &:is(.logo) {
        height: 25px;
        display: inline-block;
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
