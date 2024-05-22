<template>
  <div class="table-container">
    <table>
      <slot />
    </table>
  </div>
</template>
<style lang="scss" scoped>
.table-container {
  border-radius: $space-xs;
  overflow-x: auto;
  margin-top: $space-sm;
  margin-bottom: $space-md;

  table {
    border-spacing: 0;
    width: 100%;
  }
}
</style>
<style lang="scss">
table {

  tr {
    td, th {
      padding: $space-sm;
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
