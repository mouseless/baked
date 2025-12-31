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
    <AwaitLoading :skeleton="{ height: '1.5rem', width: '100%' }">
      <span v-if="data">{{ localizeMessage ? l(data) : data }}</span>
      <span v-else>-</span>
    </AwaitLoading>
  </Message>
</template>
<script setup>
import { Message } from "primevue";
import { useLocalization } from "#imports";
import { AwaitLoading } from "#components";

const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, severity, localizeMessage } = schema;
</script>
