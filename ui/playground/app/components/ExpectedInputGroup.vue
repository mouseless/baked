<template>
  <Validation>
    <InputGroup :name="testId">
      <InputText
        v-model="model"
        :schema="{
          label: {
            mode:'ifta',
            text: 'input-1',
            showOptionality: true
          }
        }"
      />
      <InputGroupAddon>
        <i class="pi pi-microchip w-10 text-center" />
      </InputGroupAddon>
    </InputGroup>
  </Validation>
</template>
<script setup>
import { InputGroup, InputGroupAddon } from "primevue";
import { Validation, InputText } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { testId } = schema;
</script>