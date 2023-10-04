<template>
  <div class="table-container">
    <table>
      <slot />
    </table>
  </div>
</template>
<style lang="scss" scoped>
.table-container {
  overflow-x: auto;
  margin: 2em 0;

  table {
    @include radius();
    @include border();
    border-spacing: 0;
    width: 100%;
  }
}
</style>
<style lang="scss">
table {
  tr {
    td, th {
      padding: 1em;
    }

    th, &:nth-child(even) td {
      background-color: $color-bg-table-nth-child;
    }
  }

  * {
    text-align: left;
  }
}
</style>
