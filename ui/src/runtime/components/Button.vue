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
import { useActionExecuter, useContext } from "#imports";
import { Button } from "primevue";

const actionExecuter = useActionExecuter();
const context = useContext();
const events = context.injectEvents();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { action, actionEventName, icon, label } = schema;
const loading = ref(false);

async function onClick() {
  loading.value = true;
  await actionExecuter.execute(action);
  loading.value = false;

  if(actionEventName) {
    events.emit(actionEventName);
  }
}
</script>