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

const { state, error } = defineProps({
  title: { type: String, required: true },
  state: { type: Boolean, default: true },
  error: { type: Object, default: null }
});

const loading = ref(state);

if(error) {
  context.provideError(ref(error));
} else {
  context.provideLoading(loading);
}
</script>