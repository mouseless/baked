<template>
  <Button
    :schema="dialogButton"
    @submit="visible = true"
  />
  <Dialog
    v-model:visible="visible"
    modal
    :header="l(header)"
    :style="{ width: '25rem' }"
    @after-hide="$emit('submit')"
  >
    <Bake
      name="dialog/content"
      :descriptor="content"
    />
    <template
      v-if="actionButton"
      #footer
    >
      <Button
        :schema="actionButton"
        @click="visible = false;"
      />
    </template>
  </Dialog>
</template>
<script setup>
import { ref } from "vue";
import { Dialog } from "primevue";
import { Bake, Button } from "#components";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

defineEmits(["submit"]);

const { actionButton, content, dialogButton, header } = schema;
console.log(actionButton);

const visible = ref(false);
</script>