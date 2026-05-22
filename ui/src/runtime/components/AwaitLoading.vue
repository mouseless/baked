<template>
  <template v-if="loading">
    <slot name="loading">
      <Skeleton v-bind="skeleton" />
    </slot>
  </template>
  <template v-else-if="!noError && error">
    <slot
      name="error"
      :error
    >
      <div
        class="
          rounded-md pl-1
          bg-red-500/10 text-red-500
        "
        :style="skeleton"
      >
        <span
          v-tooltip.bottom="{
            value: `${error.summary} - ${error.detail}`,
            autoHide: false,
            pt: {
              text: { class: 'bg-red-500' },
              arrow: { class: 'border-b-red-500' }
            }
          }"
          class="text-sm"
        >{{ error.summary }}</span>
      </div>
    </slot>
  </template>
  <slot v-else-if="!error" />
</template>
<script setup>
import { computed } from "vue";
import { Skeleton } from "primevue";
import { useContext } from "#imports";

const { skeleton = {}, noError } = defineProps({
  skeleton: { type: Object, default: () => ({}) },
  noError: { type: Boolean, default: false }
});

const context = useContext();
const loading = context.injectLoading();
const errorObject = noError ? null : context.injectError();

const error = computed(() => {
  if(!errorObject.value) { return null; }

  const error = errorObject.value;
  if(error.name === "FetchError") {
    return {
      summary: error.data?.title ?? error.statusCode ?? "ERROR",
      detail: error.data?.detail ?? error.message ?? error.cause ?? "An error occured..."
    };
  }

  return {
    summary: error.statusCode ?? error.status ?? "ERROR",
    detail: error.message ?? error.cause ?? "An error occured..."
  };
});
</script>