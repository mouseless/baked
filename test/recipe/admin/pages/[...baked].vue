<template>
  <Page :route-params="$route.params.baked" />
</template>
<script setup>
import { onMounted } from "vue";
import { useToast } from "#imports";
import { useRoute } from "vue-router";

const toast = useToast();
const route = useRoute();

onMounted(()=>{
  toast.add({ severity: "info", summary: "Info", detail: `You are currently viewing ${route.fullPath}`, life: 3000 });
});
</script>
