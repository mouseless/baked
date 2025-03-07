<template>
</template>
<script setup>
import { onMounted } from "vue";
import { useToast } from "#imports";

const toast = useToast();

onMounted(() => {
  toast.add({
    severity: "success",
    summary: "Summary",
    detail: "This is a test detail for a success toast message"
  });
});
</script>
