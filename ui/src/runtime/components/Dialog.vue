<template>
  <Button
    :schema="open"
    v-bind="$attrs"
    @submit="visible = true"
  />
  <Dialog
    v-model:visible="visible"
    :header="l(header)"
    class="w-max max-w-[90vw]"
    closable
    modal
    dismissable-mask
    :draggable="false"
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
import { useLocalization } from "#imports";
import { Bake, Button } from "#components";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: Object, required: true }
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
