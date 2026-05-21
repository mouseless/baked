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
    >
      <InputNumber
        v-model="model"
        v-bind="$attrs"
        :use-grouping="!noGrouping"
        class="min-w-60"
        @input="onInput"
      />
      <template #message>
        <Message
          v-if="validation"
          v-show="validation.message && validation.persist"
          :severity="validation.severity"
          variant="simple"
          size="small"
          class="ml-2"
        >
          {{ validation.message || "" }}
        </Message>
      </template>
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

const { label, noGrouping } = schema;

const path = context.injectPath();
const validation = context.injectValidations();

function onInput(event) {
  model.value = event.value;
}
</script>