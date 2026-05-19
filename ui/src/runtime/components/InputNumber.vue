<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-60">
        <Skeleton class="min-h-10" />
      </div>
    </template>
    <Labeler
      :label
      :path
      :mode="labelMode"
      :variant="labelVariant"
      :validate-label
    >
      <InputNumber
        v-model="model"
        v-bind="$attrs"
        :use-grouping="!noGrouping"
        class="min-w-60"
        @input="onInput"
      />
      <Message
        v-show="validation.message && validation.persist"
        :severity="validation.severity"
        variant="simple"
        size="small"
        class="ml-2"
      >
        {{ validation.message || "" }}
      </Message>
    </Labeler>
  </AwaitLoading>
</template>
<script setup>
import { InputNumber, Message, Skeleton } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label, labelMode, labelVariant, validateLabel, noGrouping } = schema;

const path = context.injectPath();
const validation = context.injectValidations();

function onInput(event) {
  model.value = event.value;
}
</script>