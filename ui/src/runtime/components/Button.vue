<template>
  <Button
    :icon="`pi ${icon}`"
    :label="l(label)"
    :loading
    @click="onClick"
  />
</template>
<script setup>
import { ref } from "vue";
import { useActionExecuter, useContext, useLocalization } from "#imports";
import { Button } from "primevue";

const actionExecuter = useActionExecuter();
const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { action, icon, label, postAction } = schema;

const loading = ref(false);
const injectedData = context.injectData();
const events = context.injectEvents();

async function onClick() {
  loading.value = true;
  await actionExecuter.execute({ action, injectedData, events });

  if(postAction) {
    await actionExecuter.execute({ action: postAction, injectedData, events });
  }
  loading.value = false;
}
</script>