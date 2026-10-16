<template>
  <Popover
    ref="popoverRef"
    class="before:content-none after:content-none"
  >
    <div class="flex flex-col gap-2">
      <h3 class="mb-1 flex items-center gap-2 text-sm text-red-700 dark:text-red-400">
        <i class="pi pi-exclamation-circle" />
        <template v-if="messages.length === 1">
          {{ lc('1 issue to fix') }}
        </template>
        <template v-else>
          {{ lc('{count} issues to fix', { count: messages.length }) }}
        </template>
      </h3>
      <div
        v-for="m in messages ?? []"
        :key="m.message"
        class="flex items-center gap-2 text-sm"
      >
        <span class="inline-block bg-red-700 dark:bg-red-400 rounded-full size-2" />
        <span>{{ m.message }}</span>
      </div>
    </div>
  </Popover>
</template>
<script setup>
import { ref } from "vue";
import { Popover } from "primevue";
import { useLocalization } from "#imports";

const { localize: lc } = useLocalization({ group: "ValidationSummaryPopover" });

defineExpose({ show, hide });
defineProps({
  messages: { type: Array, required: true }
});

const popoverRef = ref();

function show(event) {
  popoverRef.value.show(event);
}

function hide() {
  popoverRef.value.hide();
}
</script>