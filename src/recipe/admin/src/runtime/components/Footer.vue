<template>
  <div class="flex flex-col justify-center items-center">
    <div class="relative w-10 h-10">
      <Bake
        v-if="languageSwitcher"
        name="languageSwitcher"
        :descriptor="languageSwitcher"
        class="absolute top-3 left-3"
      />
    </div>
    <Bake
      v-if="profile"
      name="profile"
      :descriptor="profile"
    />
  </div>
</template>
<script setup>
const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { languageSwitcher, profile } = schema;
</script>