<template>
  <div class="space-y-4">
    <PageTitle
      v-if="loaded"
      :schema="{ title, description }"
    />
    <div class="flex justify-center w-full">
      <div
         class="max-w-screen-xl flex gap-4 align-top w-4/5"
         :class="{
           'flex-col': !vertical,
           'items-center': !vertical,
           'items-start': vertical
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
              'text-lg': !vertical,
              'mt-2': !vertical,
              '-mb-2': !vertical
            }"
          >{{variant.name}}</h2>
          <Divider v-if="!vertical" />
          <div :data-testid="variant.name">
            <Bake
              :name="`variants/${camelize(variant.name)}`"
              :descriptor="prepareDescriptor(variant)"
            />
          </div>
        </div>
        <slot v-if="$slots.default" name="default" />
      </div>
    </div>
  </div>
</template>
<script setup>
import { computed, onMounted, ref } from "vue";
import { usePages } from "#imports";
import { Divider } from "primevue";

const { title, variants, noLoadingVariant } = defineProps({
  title: { type: String, required: true },
  variants: { type: Array, default: () => [] },
  noLoadingVariant: { type: Boolean, default: false },
  vertical: { type: Boolean, default: false }
});

const pages = usePages();
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

onMounted(async() => {
  const specs = await pages.fetch("specs");

  const linksWithTitle = specs.schema.links.filter(l => l.schema.title === title);
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
      args: [variant.delay, variant.descriptor.data?.value]
    };
  }

  return variant.descriptor;
}
</script>
