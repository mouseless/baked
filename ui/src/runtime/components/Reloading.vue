<template>
  <Transition
    enter-active-class="transition duration-200 ease-out"
    enter-from-class="opacity-0 scale-75"
    leave-active-class="transition duration-150 ease-in"
    leave-to-class="opacity-0 scale-75"
  >
    <div
      v-if="reloading"
      class="fixed top-2 right-2 z-10 flex"
    >
      <span
        class="
          size-6 animate-spin rounded-full
          border-2
          border-slate-300 dark:border-zinc-700
          border-t-(--p-primary-color)!
        "
      />
    </div>
  </Transition>
</template>
<script setup>
defineProps({
  reloading: Boolean
});
</script>