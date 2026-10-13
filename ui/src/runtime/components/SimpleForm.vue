<template>
  <div
    class="flex flex-col gap-4"
    :class="[
      { 'gap-8': !horizontal },
      { 'horizontal': horizontal }
    ]"
  >
    <h2
      v-if="title"
      class="font-bold text-xl truncate"
    >
      {{ l(title) }}
    </h2>
    <div
      class="flex gap-4"
      :class="{ 'flex-col': !horizontal }"
    >
      <Inputs
        v-if="inputs"
        :inputs
        form-mode
        input-class="w-full"
        @ready="onReady"
        @changed="onChanged"
      />
      <Teleport
        defer
        :to="submitTarget"
        :disabled="!submitTarget"
      >
        <Button
          ref="submitRef"
          v-tooltip.top="{
            disabled: !showValidationSummary,
            value: messages,
            pt: { text: 'text-sm' }
          }"
          :schema="submit"
          :ready
          class="min-w-min"
          :class="[
            { 'mt-4': !horizontal && inputs.length },
            { 'text-nowrap self-start': horizontal }
          ]"
          @submit="$emit('submit', model)"
        />
        <ErrorPopover ref="errorPopoverRef">
          <InlineError :error />
        </ErrorPopover>
      </Teleport>
    </div>
  </div>
</template>
<script setup>
import { computed, ref, watch } from "vue";
import { useBakeError, useLocalization, useValidation } from "#imports";
import { Button, ErrorPopover, InlineError, Inputs } from "#components";

const { handle: handleError } = useBakeError();
const { localize: l } = useLocalization();
const { validate } = useValidation();

const { schema } = defineProps({
  schema: { type: null, required: true },
  submitTarget: { type: String, default: null }
});
defineEmits(["submit"]);

const { inputs, horizontal, submit, title, validations = [], showValidationSummary } = schema;

const model = ref({});
const readyData = ref({});
const submitRef = ref();
const errorPopoverRef = ref();
const { error } = handleError();

const ready = computed(() => Object.values(readyData.value).every(v => v) && isValid.value);

const { isValid, messages } = validate({
  inputs,
  model,
  composables: validations
});

watch(error, newError => {
  if(newError) {
    errorPopoverRef.value.show(submitRef);
  } else {
    errorPopoverRef.value.hide();
  }
});

function onReady(value) {
  readyData.value = value;
}

function onChanged({ values }) {
  model.value = values;
}
</script>