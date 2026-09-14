<template>
  <AwaitLoading
    :skeleton="{
      height: label.mode === 'ifta' ? '10rem' : '9.6rem',
      class: 'min-w-60'
    }"
  >
    <Validation>
      <Labeler
        :label
        :path
      >
        <Textarea
          :id="path"
          v-model="model"
          v-bind="$attrs"
          rows="5"
          fluid
        />
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { Textarea } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label } = schema;

const path = context.injectPath();
</script>
