<template>
  <AwaitLoading
    :skeleton="{
      height: label.mode === 'ifta' ? '3.6rem' : '2.6rem',
      class: 'min-w-60'
    }"
  >
    <Validation>
      <Labeler
        :label
        :path
        class="w-full"
      >
        <InputText
          :id="path"
          v-model="inputModel"
          type="email"
          v-bind="$attrs"
          placeholder="___@___.___"
          class="min-w-60"
          @input="onInput"
        />
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { InputText } from "primevue";
import { useContext, useInputValidator } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();
const { mailAddress: validator } = useInputValidator();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label } = schema;

const path = context.injectPath();

const inputModel = ref("");

watch(model, newModel => {
  inputModel.value = validator.validate(newModel, { silent: true }) ? newModel : inputModel.value;
}, { immediate: true });

function onInput(event) {
  const valid = validator.validate(event.target.value);
  model.value = valid ? event.target.value : null;
}
</script>
