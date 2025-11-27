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
    <Loading :skeleton="{ height: '1.5rem', width: '100%' }">
      <span v-if="data">{{ localizeMessage ? l(data) : data }}</span>
      <span v-else>-</span>
    </Loading>
  </Message>
</template>
<script setup>
import { Message } from "primevue";
import { useLocalization } from "#imports";
import { Loading } from "#components";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});

const { icon, severity, localizeMessage } = schema;
</script>
