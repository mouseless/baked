<template>
  <div class="flex items-center gap-1 justify-center">
    <span class="text-sm mr-2 max-xs:hidden">{{ lc("Page {page}", { page }) }}</span>
    <Button
      rounded
      variant="text"
      icon="pi pi-chevron-left"
      :disabled="page <= 1"
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
    <Bake
      v-if="takeComponent && isXs"
      v-model="take"
      name="take"
      :descriptor="takeComponent"
    />
  </div>
</template>

<script setup>
import { computed, ref, watch } from "vue";
import { Button } from "primevue";
import { useRoute, useRouter } from "#app";
import { useBreakpoints, useContext, useLocalization } from "#imports";
import { Bake } from "#components";

const route = useRoute();
const router = useRouter();
const { isXs } = useBreakpoints();
const context = useContext();
const { localize: lc } = useLocalization({ group: "ServerPaginator" });

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { take: takeComponent, takeParameterName = "take", skipParameterName = "skip", pageChangeEventName = "page-changed" } = schema;

const events = context.injectEvents();
context.provideLoading(false);
const take = ref(Number(route.query[takeParameterName]) || 10);

const allowNext = computed(() => data?.length >= take.value);
const skip = computed(() => Number(route.query[skipParameterName]) || 0);
const page = computed({
  get: () => skip.value / take.value + 1 || 1,
  set: value => {
    router.push({
      query: {
        ...route.query,
        [skipParameterName]: (value - 1) * take.value,
        [takeParameterName]: take.value
      }
    });
  }
});

watch(take, (newTake, oldTake) => {
  if(oldTake === newTake) { return; }

  page.value = 1;
});

watch([() => route.query[skipParameterName], () => route.query[takeParameterName]], () => {
  events.publish(pageChangeEventName, page.value);
});
</script>
