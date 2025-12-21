<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-60">
        <Skeleton class="min-h-10" />
      </div>
    </template>
    <FloatLabel variant="on">
      <InputText
        v-model="model"
        v-bind="$attrs"
        class="min-w-60"
      />
      <label :for="path">{{ l(label) }}</label>
    </FloatLabel>
  </AwaitLoading>
</template>
<script setup>
import { FloatLabel, InputText, Skeleton } from "primevue";
import { useContext, useLocalization } from "#imports";
import { AwaitLoading } from "#components";

const context = useContext();
const { localize: l } = useLocalization();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label } = schema;

const path = context.injectPath();
</script>
