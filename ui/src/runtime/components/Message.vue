<template>
  <div
    class="
      b-message
      grid grid-cols-[1fr_auto] gap-2
      items-center
    "
    :class="[`message-${severity}`, `message-${variant}`]"
  >
    <div class="min-h-0">
      <div
        :class="sizeClass"
        class="
          b-message-body
          flex flex-col gap-3
        "
      >
        <div class="flex flex-row items-center gap-2">
          <i
            v-if="icon"
            class="b-message-icon"
            :class="icon"
          />
          <AwaitLoading :skeleton="{ height: '1.5rem', width: '100%' }">
            <span v-if="data">{{ localizeMessage ? l(data) : data }}</span>
            <span v-else>-</span>
          </AwaitLoading>
        </div>
        <slot name="content" />
      </div>
    </div>
    <div
      v-if="action"
      class="b-message-action"
    >
      <Bake
        name="action"
        :descriptor="{...action, schema: { ...action.schema, severity } }"
      />
    </div>
  </div>
</template>
<script setup>
import { computed, useLocalization } from "#imports";
import { AwaitLoading, Bake } from "#components";

const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { action, icon, localizeMessage, severity = "info", size, variant = "outlined" } = schema;

const sizeClass = computed(() => {
  switch (size) {
  case "small":
    return "text-sm font-normal";
  case "large":
    return "text-lg font-semibold";
  default:
    return "text-base font-medium";
  }
});
</script>
<style scoped>
.b-message-action {
  @apply px-2;
}
.message-outlined {
  @apply border rounded-md;

  .b-message-body {
    @apply py-[0.50rem] px-[0.75rem] min-w-[10rem] min-h-[1.5rem];
  }
}
.message-simple {
  @apply !bg-transparent;
}
.message-simple .b-message-body {
  @apply  px-0;
}
.message-info {
  @apply bg-blue-50/95 text-blue-600 border-blue-200
    dark:bg-blue-950/95 dark:text-blue-500 dark:border-blue-700/65;
}
.message-error {
  @apply bg-red-50/95 text-red-600 border-red-200
    dark:bg-red-950/95 dark:text-red-500 dark:border-red-700/65;
}
.message-warn {
  @apply bg-yellow-50/95 text-yellow-600 border-yellow-200
    dark:bg-yellow-950/95 dark:text-yellow-500 dark:border-yellow-700/65;
}
.message-success {
  @apply bg-green-50/95 text-green-600 border-green-200
    dark:bg-green-950/95 dark:text-green-500 dark:border-green-700/65;
}
.message-secondary {
  @apply bg-transparent text-slate-500 border-slate-500
    dark:text-slate-400 dark:border-slate-400;
}
.message-contrast {
  @apply bg-transparent text-slate-950 border-slate-950
    dark:text-white dark:border-white;
}
</style>