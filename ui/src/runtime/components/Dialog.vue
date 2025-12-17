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
    @after-hide="submit"
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
        @click="execute"
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
const emit = defineEmits(["submit"]);

const { actionButton, content, dialogButton, header } = schema;
const visible = ref(false);
const submitted = ref(false);

function execute() {
  visible.value = false;
  submitted.value = true;
}

function submit() {
  if(submitted.value) {
    submitted.value = false;

    emit("submit");
  }
}
</script>