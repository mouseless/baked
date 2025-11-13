<template>
  <Panel header="Page with route">
    <p data-testid="fullPath">{{ $route.fullPath }}</p>
    <p data-testid="params">{{ $route.params }}</p>
  </Panel>
</template>
<script setup>
import { Panel } from "primevue";
</script>
