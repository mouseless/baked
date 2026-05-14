<template>
  <div class="flex flex-col gap-8">
    <PageTitle :schema="title">
      <template
        v-if="sections.length > 0"
        #actions
      >
        <Button
          v-tooltip="{
            disabled: !validationOnTooltip,
            value: validationMessages,
            showDelay: 300,
            pt: { text: 'text-sm' }
          }"
          :schema="submit"
          :ready
          @submit="onSubmit"
        />
      </template>
    </PageTitle>
    <div class="flex justify-center">
      <Contents
        v-focustrap
        class="gap-6"
      >
        <div
          v-for="section in sections"
          :key="section.key"
          class="w-full col-span-2 grid gap-4"
        >
          <div
            v-if="sections.length > 1"
            class="
              pb-2 border-b-2
              border-zinc-100 dark:border-zinc-900
            "
          >
            <span
              class="
                text-md font-semibold
                text-zinc-800 dark:text-zinc-400
              "
            >
              {{ l(section.label) }}
            </span>
          </div>
          <template
            v-for="(inputGroups, i) in splitByWide(section.inputGroups)"
            :key="`${section.key}_${i}`"
          >
            <div
              v-if="inputGroups.length > 0"
              class="
                grid grid-cols-2 grid-flow-col
                gap-4 items-start
                max-md:flex max-md:flex-col
              "
              :style="{ 'grid-template-rows': `repeat(${Math.ceil(inputGroups.length / 2)}, auto)` }"
            >
              <div
                v-for="inputGroup in inputGroups"
                :key="inputGroup.key"
                class="w-full flex gap-4 max-md:flex-col self-end"
                :class="{
                  'col-span-2': inputGroup.wide,
                  'reset-min-w': inputGroup.inputs.length > 1
                }"
              >
                <Inputs
                  :inputs="inputGroup.inputs"
                  input-class="w-full"
                  :validator="validator"
                  @ready="(value) => onReady(`${section.key}_${inputGroup.key}`, value)"
                  @changed="onChanged"
                />
              </div>
            </div>
          </template>
        </div>
      </Contents>
    </div>
  </div>
</template>
<script setup>
import { computed, ref } from "vue";
import { useLocalization, useComposableResolver } from "#imports";
import { Button, Contents, Inputs, PageTitle } from "#components";

const { localize: l } = useLocalization();
const composableResolver = useComposableResolver();

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const { title, submit, sections, validateComposable = [], validationOnTooltip = true } = schema;

const validators = validateComposable.map(vc => composableResolver.resolve(vc).default);

const formData = ref({});
const readyData = ref({});

const validationMessages = computed(() => {
  if(!validationOnTooltip) { return null; }

  return Object.values(validator.value)
    .filter(v => v.message)
    .map((v, i) => `${i > 0 ? "\n" : ""} - ${v.message}`)
    .join("")
    .toString();
});
const ready = computed(() => {
  return Object.values(readyData.value).every(v => v) && Object.values(validator.value).every(v => v.valid);
});
const validator = computed(() =>
  validators.reduce((_default, useValidate) => {
    return { ..._default, ...useValidate({ sections, formData }) };
  }, {})
);

function splitByWide(inputGroups) {
  const result = [];
  let cur = [];
  for(const inputGroup of inputGroups) {
    if(!inputGroup.wide) {
      cur.push(inputGroup);

      continue;
    }

    result.push(cur);
    result.push([inputGroup]);
    cur = [];
  }

  result.push(cur);

  return result.filter(r => r.length);
}

function onReady(key, value) {
  Object.assign(readyData.value, { [key]: value });
}

function onChanged({ values }) {
  Object.assign(formData.value, values);
}

function onSubmit() {
  if(!ready.value) { return; }

  emit("submit", formData.value);
}
</script>
<style>
.b-component--FormPage {
  .reset-min-w * {
    @apply min-w-0;
  }
}
</style>
