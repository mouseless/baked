<template>
  <Fieldset>
    <template #legend>
      <AwaitLoading :skeleton="{ width: '10em', height: '1.75em' }">
        <span
          v-if="data"
          class="font-bold text-xl pb-4"
        >{{ data[titleProp] }}</span>
      </AwaitLoading>
    </template>
    <div class="grid grid-cols-2 gap-4 max-md:grid-cols-1">
      <div
        v-for="field in fields"
        :key="field.key"
        class="flex flex-col"
        :class="{ 'md:col-span-2': field.wide }"
      >
        <span class="text-sm font-bold">{{ l(field.label ) }}</span>
        <Bake
          :name="`fields/${field.key}`"
          :descriptor="field.component"
        />
      </div>
    </div>
  </Fieldset>
</template>
<script setup>
import { Fieldset } from "primevue";
import { AwaitLoading, Bake } from "#components";
import { useLocalization } from "#imports";

const { localize: l } = useLocalization();

const { schema, data } = defineProps({
  schema: { type: Object, required: true },
  data: { type: null, required: true }
});

const { titleProp, fields } = schema;
</script>
