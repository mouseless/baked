<template>
  <div
    :data-testid="title"
    class="flex flex-col"
  >
    <h2 class="text-lg font-semibold mt-2">{{ title }}</h2>
    <Divider />
    <slot />
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Divider } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const { loaded } = defineProps({
  title: { type: String, required: true },
  loading: { type: Boolean, default: false }
});

const loading = ref(!loaded);

context.provideLoading(loading);
</script>