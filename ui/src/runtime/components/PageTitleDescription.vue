<template>
  <div
    class="
      relative h-8 flex items-center gap-2
      text-sm text-gray-600 dark:text-gray-400
    "
  >
    <AwaitLoading :skeleton="{ width: '10rem' }">
      <div
        v-for="field in infoFields"
        :key="field.key"
        class="flex gap-1 items-center text-nowrap max-xs:hidden"
      >
        <span class="max-md:hidden">{{ l(field.label) }}:</span>
        <Bake
          :name="`fields-${field.key}`"
          :descriptor="field.component"
          class="lg:font-bold"
        />
      </div>
    </AwaitLoading>
    <template v-if="description">
      <div
        class="flex gap-2 items-center"
        :class="{
          'max-md:hidden': !infoFields.length,
          'max-xl:hidden ml-2': infoFields.length
        }"
      >
        <i
          v-if="infoFields.length"
          class="text-sm pi pi-info-circle"
        />
        <div
          data-testid="description"
          class="grid text-nowrap overflow-hidden"
        >
          <span class="truncate">
            {{ l(description) }}
          </span>
        </div>
      </div>
      <Button
        v-tooltip.focus.bottom="{ value: l(description) }"
        :class="{
          'md:hidden': !infoFields.length,
          'xl:hidden': infoFields.length
        }"
        icon="pi pi-info-circle"
        variant="text"
        size="small"
        rounded
      />
    </template>
  </div>
</template>
<script setup>
import { useLocalization } from "#imports";
import { Button } from "primevue";

const { localize: l } = useLocalization();

const { description, infoFields } = defineProps({
  description: { type: String, default: null },
  infoFields: { type: Array, default: () => [] }
});
</script>