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
      <InputText
        v-model="input"
        v-bind="$attrs"
        class="min-w-60"
        @update:model-value="onUpdate"
      />
      <Message
        v-show="validator[name]?.message && validator[name]?.persist"
        :severity="validator[name]?.severity"
        variant="simple"
        size="small"
        class="ml-2"
      >
        {{ validator[name]?.message || "" }}
      </Message>
    </Labeler>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { InputText, Message, Skeleton } from "primevue";
import { useContext } from "#imports";
import { AwaitLoading, Labeler } from "#components";

const context = useContext();

const { schema } = defineProps({
  schema: { type: null, required: true }
});

const model = defineModel({ type: null, required: true });

const { label, labelMode, labelVariant, validateLabel, targetProp } = schema;

const path = context.injectPath();
const { validator = {}, name } = context.injectParentContext();

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
