<template>
  <div
    :data-testid="title"
    class="flex flex-col"
  >
    <h2 class="text-lg font-semibold mt-2">{{ title }}</h2>
    <Divider />
    <slot :handled />
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Divider } from "primevue";
import { useContext } from "#imports";

const context = useContext();

const { error } = defineProps({
  title: { type: String, required: true },
  error: { type: Object, required: true }
});

const handled = context.provideError(ref(error));
</script>