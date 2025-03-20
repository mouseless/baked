<template>
  <div :class="{ hidden: !show }">
    <slot v-if="loaded || show" />
  </div>
</template>
<script setup>
import { computed, ref, watch } from "vue";

const { when } = defineProps({
  when: {
    type: String,
    required: true
  }
});

const model = defineModel({
  type: String,
  required: true
});

const show = computed(() => model.value === when);
const loaded = ref(show.value);

watch(show, () => {
  if(show.value) {
    loaded.value = true;
  }
});
</script>
