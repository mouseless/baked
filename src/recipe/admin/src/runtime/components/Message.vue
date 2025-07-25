<template>
  <Message
    :severity="severity"
    :pt="{ text: { class: 'min-w-[10rem] min-h-[1.5rem]' } }"
  >
    <template
      v-if="icon"
      #icon
    >
      <i :class="`pi ${icon}`" />
    </template>
    <Skeleton
      v-if="loading"
      height="1.5rem"
      width="100%"
    />
    <span v-else-if="data">{{ l(data) }}</span>
    <span v-else>-</span>
  </Message>
</template>
<script setup>
import { Message, Skeleton } from "primevue";
import { useContext, useLocalization } from "#imports";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});

const { icon, severity } = schema;

const loading = context.loading();
</script>
