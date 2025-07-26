<template>
  <UiSpec title="Toast">
    <Message severity="info">
      <span class="text-xl">
        ↗️  Check if page has a success toast at top right ↗️
      </span>
    </Message>
  </UiSpec>
</template>
<script setup>
import { onMounted } from "vue";
import { Message } from "primevue";
import { useToast, useLocalization } from "#imports";

const toast = useToast();
const { localize: l } = useLocalization();

onMounted(() => {
  toast.add({
    severity: "success",
    summary: l("Spec.Summary"),
    detail: l("Spec.This_is_a_test_detail_for_a_success_toast_message")
  });
});
</script>
