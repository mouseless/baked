<template>
  <AwaitLoading
    :skeleton="{
      height: label?.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-60'
    }"
  >
    <Validation>
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
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { InputNumber } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label, noGrouping } = schema;

const path = context.injectPath();

function onInput(event) {
  model.value = event.value;
}
</script>