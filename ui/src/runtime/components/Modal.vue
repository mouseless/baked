<template>
  <Button
    :label="l(label)"
    @click="visible = true"
  />
  <Dialog
    v-model:visible="visible"
    modal
  >
    <template
      v-if="header"
      #header
    >
      <Bake
        name="modal/header"
        :descriptor="header"
      />
    </template>
    <Bake
      name="modal/content"
      :descriptor="content"
    />
    <template
      v-if="footer"
      #footer
    >
      <Bake
        name="modal/footer"
        :descriptor="footer"
      />
    </template>
  </Dialog>
</template>
<script setup>
import { ref } from "vue";
import { Button, Dialog } from "primevue";
import { Bake } from "#components";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const { content, header, footer, label } = schema;

const visible = ref(false);
</script>