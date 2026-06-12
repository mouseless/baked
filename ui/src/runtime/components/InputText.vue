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
        <InputText
          v-model="input"
          v-bind="$attrs"
          class="min-w-60"
          @update:model-value="onUpdate"
        />
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { InputText } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label, targetProp } = schema;

const path = context.injectPath();

const input = ref("");

watch(model, newValue => {
  input.value = newValue;
}, { immediate: true });

function onUpdate(value) {
  input.value = value;
  model.value = value && targetProp
    ? { [targetProp]: value }
    : value || undefined;
}
</script>