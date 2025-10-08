<template>
  <Panel header="Page with route">
    {{ $route.fullPath }}
  </Panel>
</template>
<script setup>
import { Panel } from "primevue";
</script>
