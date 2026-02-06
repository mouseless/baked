<template>
  <div class="space-y-4 mb-40">
    <PageTitle
      v-if="loaded"
      :schema="{ title, description }"
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
            class="space-x-4"
          >
            <div
              :data-testid="variant.name"
              :class="variantClass"
            >
              <!-- renders given variants -->
              <Bake
                v-if="variant.model"
                v-model="variant.model.value"
                :name="`variants/${camelize(variant.name)}`"
                :descriptor="prepareDescriptor(variant)"
              />
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
import { computed, onMounted, reactive, ref } from "vue";
import { Divider } from "primevue";
import { useContext, useEvents, usePages } from "#imports";

const context = useContext();
const events = useEvents();
const pages = usePages();

const { title, variants, noLoadingVariant } = defineProps({
  title: { type: String, required: true },
  variants: { type: Array, default: () => [] },
  noLoadingVariant: { type: Boolean, default: false },
  vertical: { type: Boolean, default: false },
  testId: { type: String, default: "test" },
  fullPage: { type: Boolean, default: false },
  useModel: { type: Boolean, default: false },
  variantClass: { type: String, default: "inline-block" }
});

const pageContext = reactive({});
const description = ref();
const loaded = ref(false);
const allVariants = computed(() => {
  if(noLoadingVariant) { return variants; }
  if(variants.length === 0) { return variants; }

  const result = [
    ...variants,
    {
      name: "Loading",
      delay: 60 * 1000,
      descriptor: { ...variants[0].descriptor }
    }
  ];

  return result;
});

context.provideEvents(events.create());
context.providePageContext(pageContext);

onMounted(async() => {
  const specs = await pages.fetch("specs");

  const linksWithTitle = specs.schema.sections.flatMap(section =>
    section.links.filter(link => link.title === title).map(link => link.component)
  );
  if(linksWithTitle.length > 0) {
    description.value = linksWithTitle[0].schema.description;
  }

  loaded.value = true;
});

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
