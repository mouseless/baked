<template>
  <div class="table-container">
    <table>
      <slot />
    </table>
  </div>
</template>
<script setup>
import { onMounted } from "#imports";

onMounted(() => {
  const tableContainer = document.querySelector(".table-container");
  const table = document.querySelector("table");

  tableContainer.style.height = getComputedStyle(table).height;
});
</script>
<style lang="scss" scoped>
.table-container {
  overflow-x: auto;
  position: relative;
  width: 100%;
  table {
    position: absolute;
  }
}
</style>
