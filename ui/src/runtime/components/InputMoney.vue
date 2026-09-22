<template>
  <AwaitLoading
    :skeleton="{
      height: label?.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-60'
    }"
  >
    <Validation>
      <InputGroup class="w-auto">
        <Labeler
          :label
          :path
        >
          <InputNumber
            ref="number"
            v-model="model"
            v-bind="$attrs"
            class="min-w-60"
            min="0"
            :disabled
            @input="onInput"
          />
        </Labeler>
        <InputGroupAddon
          class="
              bg-slate-200 text-slate-500
              dark:bg-zinc-700 dark:text-zinc-300
            "
        >
          <i class="pi pi-dollar" />
        </InputGroupAddon>
      </InputGroup>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { InputNumber, InputGroup, InputGroupAddon } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label } = schema;

const path = context.injectPath();

function onInput({ value }) {
  model.value = value;
}
</script>
