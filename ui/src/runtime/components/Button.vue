<template>
  <Button
    :icon="`pi ${icon}`"
    :label="l(label)"
    :loading
    @click="onClick"
  />
</template>
<script setup>
import { useContext, useLocalization } from "#imports";
import { Button } from "primevue";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});
const emit = defineEmits(["submit"]);

const { icon, label } = schema;

const loading = context.injectLoading();

function onClick() {
  emit("submit");
}
</script>