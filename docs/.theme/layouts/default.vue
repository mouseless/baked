<template>
  <div>
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
            :class="menu.position < 100 ? 'left' : 'right'"
          >
            {{ menu.title }}
          </NuxtLink>
        </ContentQuery>
      </nav>
    </header>
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
header, article {
  max-width: 1768px;
  margin: auto;
  padding: 0 10px;
}

article {
  padding-top: 10px;
}

div.logo {
  margin: 20px 0px;
}

nav a {
  margin: 5px;
  @media (max-width: 800px) {
    & {
      display: block;
    }
  }
}

a.left+a.right {
  padding-left: 10px;
  @media (max-width: 800px) {
    & {
      padding-top: 10px;
      padding-left: 0px;
    }
  }
}
</style>
