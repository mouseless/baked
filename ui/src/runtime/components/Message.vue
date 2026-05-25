<template>
  <div
    class="b-message"
    :class="severityClass"
  >
    <div class="min-h-0">
      <div
        :class="sizeClass"
        class="
          b-message-content
          flex items-center gap-2
          py-[0.50rem] px-[0.75rem]
          min-w-[10rem] min-h-[1.5rem]
        "
      >
        <slot
          v-if="icon"
          name="icon"
        >
          <i
            class="b-message-icon"
            :class="`pi ${icon}`"
          />
        </slot>
        <AwaitLoading :skeleton="{ height: '1.5rem', width: '100%' }">
          <span v-if="data">{{ localizeMessage ? l(data) : data }}</span>
          <span v-else>-</span>
        </AwaitLoading>
      </div>
    </div>
  </div>
</template>
<script setup>
import { computed, useLocalization } from "#imports";
import { AwaitLoading } from "#components";

const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});
const { icon, severity = "info", localizeMessage, size } = schema;

const severityClass = computed(() => {
  switch (severity) {
  case "info":
    return "message-info";
  case "error":
    return "message-error";
  case "warn":
    return "message-warn";
  default:
    return "message-info";
  }
});

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
.b-message {
  @apply grid rounded-md outline outline-1;
}
.message-info {
  @apply bg-blue-50/95 text-blue-600 outline-blue-200;
}
.message-error {
  @apply bg-red-50/95 text-red-600 outline-red-200;
}
.message-warn {
  @apply bg-yellow-50/95 text-yellow-600 outline-yellow-200;
}

</style>