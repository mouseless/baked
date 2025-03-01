<template>
  <Button
    :to="route"
    :as="disabled ? 'button' : 'router-link'"
    outlined
    plain
    class="w-auto !p-6"
    :disabled="disabled"
  >
    <div class="flex flex-col gap-4 text-start items-start w-full h-full">
      <div class="flex gap-4">
        <span
          v-if="icon"
          :class="icon"
          class="!text-4xl"
        />
        <Tag
          v-if="disabled"
          severity="secondary"
        >
          {{ disabledReason }}
        </Tag>
      </div>
      <h2 class="text-lg">
        {{ title }}
      </h2>
      <div class="text-sm">
        {{ description }}
      </div>
    </div>
  </Button>
</template>
<script setup>
import { Button, Tag } from "primevue";

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const {
  route,
  icon,
  title,
  description,
  disabled,
  disabledReason
} = schema;
</script>
