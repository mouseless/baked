<template>
  <ComponentSpec title="Toast" :variants="[]">
    <Message severity="info">
      <span class="text-xl">
        ↗️  Check if page has a success toast at top right ↗️
      </span>
    </Message>
  </ComponentSpec>
</template>
<script setup>
import { onMounted } from "vue";
import { Message } from "primevue";
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
