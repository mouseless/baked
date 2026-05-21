<template>
  <AwaitLoading>
    <template #loading>
      <div class="min-w-60">
        <Skeleton class="min-h-10" />
      </div>
    </template>
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
import { InputText, Skeleton } from "primevue";
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