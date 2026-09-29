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
        <InputMask
          v-if="!usePicker"
          :id="path"
          v-model="inputModel"
          v-bind="$attrs"
          slot-char="__/__/____"
          :mask
          placeholder="__/__/____"
          :unmask="true"
          :auto-clear="false"
          class="min-w-60"
          @update:model-value="onUpdateInputModel"
        />
        <DatePicker
          v-else
          :id="path"
          v-model="inputModel"
          v-bind="$attrs"
          placeholder="__/__/____"
          class="min-w-60"
          update-model-type="string"
          :date-format="format"
          show-icon
          @update:model-value="onUpdateInputModel"
        />
      </Labeler>
    </Validation>
  </AwaitLoading>
</template>
<script setup>
import { ref, watch } from "vue";
import { InputMask, DatePicker } from "primevue";
import { useContext, useInputValidator } from "#imports";
import { AwaitLoading, Labeler, Validation } from "#components";

const context = useContext();
const { date: validator } = useInputValidator();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const { label, format = "dd/MM/yyyy", usePicker } = schema;

const path = context.injectPath();

const mask = format?.replace(/[dMy]/g, "9");
const inputModel = ref("");

watch(model, newModel => {
  const value = fromAPIFormat(newModel);
  const valid = validator.validate(value, { silent: true });
  if(!valid) { return; }

  if(usePicker) {
    const { dd, mm, yyyy } = toFormat(value);

    inputModel.value = `${dd}/${mm}/${yyyy}`;

    return;
  }

  inputModel.value = value;
}, { immediate: true });

function onUpdateInputModel(value) {
  const val = String(value || "").replace(/\D/g, "");

  const valid = validator.validate(val);
  model.value = valid ? toAPIFormat(val) : null;
}

function toAPIFormat(value) {
  const { dd, mm, yyyy } = toFormat(value);

  return `${yyyy}-${mm}-${dd}`;
}

function fromAPIFormat(value) {
  if(!value) { return; }

  const [yyyy, MM, dd] = value.split("-");

  return [dd, MM, yyyy].join("");
}

function toFormat(value) {
  const dd = value.slice(0, 2);
  const mm = value.slice(2, 4);
  const yyyy = value.slice(4, 8);

  return { dd, mm, yyyy };
}
</script>
