<template>
  <Button
    :icon="`pi ${icon}`"
    :label="label"
    :loading="loading"
    @click="onClick"
  />
</template>
<script setup>
import { ref } from "vue";
import { useActionExecuter } from "#imports";
import { Button } from "primevue";

const actionExecuter = useActionExecuter();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { action, icon, label, postAction } = schema;
const loading = ref(false);

async function onClick() {
  loading.value = true;
  await actionExecuter.execute(action);

  if(postAction) {
    await actionExecuter.execute(postAction);
  }
  loading.value = false;
}
</script>