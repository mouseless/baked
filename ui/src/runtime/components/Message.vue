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
      class="b-message-action p-2"
    >
      <Bake
        name="action"
        :descriptor="{ ...action, schema: { ...action.schema, severity } }"
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