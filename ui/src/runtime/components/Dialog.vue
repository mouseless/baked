<template>
  <Button
    :schema="open"
    @submit="visible = true"
  />
  <Dialog
    v-model:visible="visible"
    closable
    :header="l(header)"
    modal
    :style="{ width: 'min(450px, 90vw)' }"
    @after-hide="emitSubmit"
  >
    <Bake
      name="dialog/content"
      :descriptor="content"
    />
    <template
      v-if="submit"
      #footer
    >
      <Button
        :schema="submit"
        class="w-full"
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

const { content, header, open, submit } = schema;
const visible = ref(false);
const submitted = ref(false);

function execute() {
  visible.value = false;
  submitted.value = true;
}

function emitSubmit() {
  if(submitted.value) {
    submitted.value = false;

    emit("submit");
  }
}
</script>