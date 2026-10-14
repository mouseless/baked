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
  >
    <Bake
      name="dialog/content"
      :descriptor="content"
    />
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
defineEmits(["submit"]);

const { content, header, open } = schema;

const visible = ref(false);
</script>