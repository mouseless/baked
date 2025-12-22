<template>
  <Button
    :disabled="!ready || executing"
    :loading="executing"
    :icon
    :label="l(label)"
    :rounded
    :severity
    :variant
    @click="$emit('submit')"
  />
</template>
<script setup>
import { useContext, useLocalization } from "#imports";
import { Button } from "primevue";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true },
  ready: { type: Boolean, default: true }
});
defineEmits(["submit"]);

const { icon, label, rounded, severity = "secondary", variant } = schema;

const executing = context.injectExecuting();
</script>
