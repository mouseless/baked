<template>
  <div class="space-y-4 mb-40">
    <PageTitle
      :schema="{
        description: `${$route.path}:Description`,
        localizeTitle: true,
        actions: []
      }"
      :data="`${$route.path}:Title`"
    />
    <div class="flex justify-center w-full">
      <div
        class="flex gap-4 align-top w-4/5 max-md:w-full"
        :class="{
          'flex-col items-center': !vertical,
          'flex-wrap items-start': vertical,
          'max-w-screen-xl': !fullPage,
          'w-full': fullPage
        }"
      >
        <div
          v-for="variant in allVariants"
          :key="variant.name"
          :class="{
            'w-full': !vertical,
            'text-center': vertical
          }"
        >
          <h2
            :id="variant.name"
            class="font-semibold"
            :class="{
              'text-lg mt-2 -mb-2': !vertical,
              'mb-2': vertical
            }"
          >
            {{ variant.name }}
          </h2>
          <Divider v-if="!vertical" />
          <div
            v-if="!useModel && !variant.model"
            :data-testid="variant.name"
            :class="{ 'inline-block': vertical }"
          >
            <Bake
              :name="`variants/${camelize(variant.name)}`"
              :descriptor="prepareDescriptor(variant)"
            />
          </div>
          <div
            v-else
            class="space-x-4 flex"
          >
            <div
              :data-testid="variant.name"
              :class="`variantClass ${variant.class || ''}`"
            >
              <!-- renders given variants -->
              <ProvideValidation
                v-if="variant.model"
                :validation="variant.validation"
              >
                <Bake
                  v-model="variant.model.value"
                  :name="`variants/${camelize(variant.name)}`"
                  :descriptor="prepareDescriptor(variant)"
                />
              </ProvideValidation>
              <!-- draws remaining variant, e.g., loading variant -->
              <Bake
                v-else
                :name="`variants/${camelize(variant.name)}`"
                :descriptor="prepareDescriptor(variant)"
              />
            </div>
            <div
              v-if="variant.model"
              class="inline-block border-2 border-gray-500 rounded p-2"
            >
              ➡️  <span :data-testid="`${variant.name}:model`">{{ variant.model }}</span> ⬅️
            </div>
            <div
              v-if="variant.pageContextKey"
              :data-testid="`${variant.name}:page-context`"
              class="inline-block border-2 border-gray-500 rounded p-2"
            >
              {{ pageContext[variant.pageContextKey] }}
            </div>
          </div>
        </div>
        <div
          v-if="$slots.default"
          :data-testid="testId"
          class="w-full flex flex-col gap-4"
        >
          <slot name="default" />
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import { computed, reactive, ref } from "vue";
import { Divider } from "primevue";
import { useContext, useEvents } from "#imports";

const context = useContext();
const events = useEvents();

const { variants, noLoadingVariant, useModel, noValidationVariant } = defineProps({
  variants: { type: Array, default: () => [] },
  noLoadingVariant: { type: Boolean, default: false },
  vertical: { type: Boolean, default: false },
  testId: { type: String, default: "test" },
  fullPage: { type: Boolean, default: false },
  useModel: { type: Boolean, default: false },
  noValidationVariant: { type: Boolean },
  variantClass: { type: String, default: "inline-block" }
});

const useValidationVariant = noValidationVariant !== undefined ? !noValidationVariant : useModel;
const useLoadingVariant = !noLoadingVariant;

const pageContext = reactive({});
const allVariants = computed(() => {
  if(variants.length === 0) { return variants; }

  const result = [ ...variants ];

  if(useValidationVariant) {
    result.push({
      name: "Validation",
      model: ref(),
      validation: {
        name: "testInput",
        validations: {
          testInput: {
            valid: false,
            persist: true,
            message: "test fail message",
            severity: "error"
          }
        }
      },
      descriptor: { ...variants[0].descriptor }
    });
  }

  if(useLoadingVariant) {
    result.push({
      name: "Loading",
      delay: 60 * 1000,
      descriptor: { ...variants[0].descriptor }
    });
  }

  return result;
});

context.provideEvents(events);
context.providePageContext(pageContext);

function camelize(str) {
  return str
    .replace(/\s+(.)/g, (_, char) => char.toUpperCase())
    .replace(/^[A-Z]/, char => char.toLowerCase());
}

function prepareDescriptor(variant) {
  if(variant.delay) {
    variant.descriptor.data = {
      type: "Computed",
      composable: "useDelayedData",
      options: { type: "Inline", value: { ms: variant.delay, data: variant.descriptor.data?.value } },
      isAsync: true
    };
  }

  return variant.descriptor;
}
</script>