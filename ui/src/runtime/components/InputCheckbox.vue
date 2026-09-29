<template>
  <AwaitLoading
    :skeleton="{
      height: label.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-24'
    }"
  >
    <div class="flex items-center gap-2 justify-start min-h-10">
      <Validation>
        <Labeler
          :label
          :path
          class="w-full cursor-pointer text-slate-500 dark:text-zinc-400"
          :dt="{
            colorScheme: {
              light: {
                ['position.x']: '2rem'
              },
              dark: {
                ['position.x']: '2rem'
              }
            }
          }"
        >
          <Checkbox
            v-model="checkboxModel"
            :input-id="path"
            binary
            size="large"
            :indeterminate="indeterminate && checkboxModel === undefined"
          />
        </Labeler>
      </Validation>
    </div>
  </AwaitLoading>
</template>
<script setup>
import { computed, watch } from "vue";
import { Checkbox } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label, indeterminate } = schema;
const path = context.injectPath();
const checkboxModel = computed({
  get: () => model.value,
  set: value => model.value = indeterminate && model.value === false && value === true ? undefined : value
});

watch(model, newValue => {
  if(typeof newValue === "string") {
    model.value = newValue === "true";
  }
});
</script>
