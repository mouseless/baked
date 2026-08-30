<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <div
      v-if="data"
      class="flex gap-2"
    >
      <Button
        :icon
        :label
        :to
        as="router-link"
        link
        class="m-0 p-0"
        @mouseenter="openPopover"
        @mouseleave="closePopover"
      />
      <span
        v-if="summary"
        class="md:hidden"
        data-icon
        @click="openPopover"
      >
        <i class="pi pi-eye" />
      </span>
      <PersistentPopover
        v-if="summary"
        ref="popover"
        class="
          mx-4 max-w-96
          max-md:w-[calc(100%-2rem)]
        "
      >
        <Bake
          v-if="summaryShown"
          name="summary"
          :descriptor="summary"
          @loaded="showPopover"
          @mouseenter="cancelHidePopover"
          @mouseleave="closePopover"
        />
      </PersistentPopover>
    </div>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed, ref } from "vue";
import defu from "defu";
import { Button } from "primevue";
import { useRuntimeConfig } from "#app";
import { useDataMounter, useFormat, usePathBuilder } from "#imports";
import { AwaitLoading, Bake, PersistentPopover } from "#components";

const { mount: mountData } = useDataMounter({ defaultInlineError: true });
const { truncate } = useFormat();
const pathBuilder = usePathBuilder();
const runtimeConfig = useRuntimeConfig();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { icon, labelProp, path, query: queryData, params: paramsData, summary, maxLength } = schema;
const timeouts = defu(
  runtimeConfig.public.components?.NavLink?.timeouts,
  { show: { initial: 500, consequent: 250 }, hide: 250 }
);

const popover = ref();
let loadPopoverTask = null;
let hidePopoverTask = null;
let showPopover = null;

const query = mountData(queryData);
const params = mountData(paramsData);

const label = computed(() => truncate(labelProp ? data?.[labelProp] : data, maxLength));
const summaryShown = ref(false);
const to = computed(() => ({
  path: params.value ? pathBuilder.build(path, params.value, { forRoute: true }) : path,
  query: query.value
}));

function openPopover(event) {
  if(!summary) { return false; }

  clearTimeout(loadPopoverTask);
  clearTimeout(hidePopoverTask);

  // store target value when async function
  // https://github.com/primefaces/primevue/issues/2352
  const target = event.target || event;
  const onIcon = event.target.dataset["icon"] !== undefined;

  loadPopoverTask = setTimeout(() => {
    if(summaryShown.value) {
      popover.value?.show(event, target);
    } else {
      summaryShown.value = true;
      showPopover = () => popover.value?.show(event, target);
    }
  }, onIcon
    ? 0
    : !summaryShown.value
      ? timeouts.show.initial
      : timeouts.show.consequent
  );
}

function closePopover(event) {
  clearTimeout(loadPopoverTask);
  hidePopoverTask = setTimeout(() => popover.value?.hide(event.target), timeouts.hide);
}

function cancelHidePopover() {
  clearTimeout(hidePopoverTask);
}
</script>