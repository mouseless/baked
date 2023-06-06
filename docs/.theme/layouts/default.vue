<template>
  <div>
    <Header />
    <div v-if="$route.path === '/'">
      <article class="full">
        <slot />
      </article>
    </div>
    <div
      v-else
      class="content"
    >
      <Side class="side" />
      <article>
        <slot />
      </article>
    </div>
  </div>
</template>
<style lang="scss" scoped>
.content, .full {
  max-width: $width-page;
  margin: auto;
}

.content {
  display: grid;
  grid-template-areas:
      "side content"
      "side content";
  grid-template-rows: 65px 1fr;
  grid-template-columns: 250px;
}

.side {
  grid-area: side;
}

article {
  grid-area: content;
  padding: 0 10px;
}
</style>
