<template>
  <div>
    <Header />
    <div v-if="$route.path === '/'">
      <article>
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
.content {
  width: $width-page;
  display: grid;
  grid-template-areas:
      "side content"
      "side content";
  grid-template-rows: 65px 1fr;
  margin: auto;
  grid-template-columns: 350px;
}

.side {
  grid-area: side;
}

article {
  grid-area: content;
  padding: 0 10px;
}
</style>
