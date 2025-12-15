<template>
  <Button
    :icon
    :label="l(label)"
    :loading="executing"
    :variant
    :rounded
    @click="$emit('submit')"
  />
</template>
<script setup>
import { useContext, useLocalization } from "#imports";
import { Button } from "primevue";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

defineEmits(["submit"]);

const { icon, label, variant, rounded } = schema;

const executing = context.injectExecuting();
</script>
