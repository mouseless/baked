<template>
  <AwaitLoading>
    <div class="flex items-center justify-center gap-1">
      <span class="whitespace-nowrap text-xs max-xs:hidden">{{ lc("Page {page}", { page }) }}</span>
      <Button
        rounded
        variant="text"
        icon="pi pi-chevron-left"
        :disabled="!allowPrevious"
        severity="secondary"
        size="small"
        @click="page--"
      />
      <Button
        rounded
        variant="text"
        icon="pi pi-chevron-right"
        severity="secondary"
        size="small"
        :disabled="!allowNext"
        @click="page++"
      />
    </div>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Button } from "primevue";
import { useContext, useLocalization } from "#imports";
import { AwaitLoading } from "#components";

const context = useContext();
const { localize: lc } = useLocalization({ group: "Paginator" });

const { data } = defineProps({
  data: { type: null, required: true }
});
const model = defineModel({ type: null, required: true });

const path = context.injectPath();
const takeStateKey = path + ".take";
const contextData = context.injectContextData();

const allowPrevious = computed(() => !Number.isNaN(page.value) && page.value > 1);
const allowNext = computed(() => !Number.isNaN(page.value) && data.length >= data.take);
const page = computed({
  get: () => Number(model.value) / Number(data.take) + 1,
  set: value => {
    model.value = (value - 1) * Number(data.take);
  }
});

if(data && contextData.page[takeStateKey] !== data.take) {
  contextData.page[takeStateKey] = data.take;

  page.value = 1;
}
</script>
