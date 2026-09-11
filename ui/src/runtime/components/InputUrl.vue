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
          type="url"
          v-bind="$attrs"
          class="min-w-60"
          placeholder="https://"
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
const { url: validator } = useInputValidator();

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
  const normalized = normalize(event.target.value);
  model.value = validator.validate(normalized) ? normalized : null;
}

function normalize(value) {
  if(!value) { return value; }

  const trimmed = value.trim();
  if(/^(?:https?|ftp):\/\//i.test(trimmed)) { return trimmed; }

  return `https://${trimmed}`;
}
</script>
