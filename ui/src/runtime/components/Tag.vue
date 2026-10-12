<template>
  <AwaitLoading :skeleton="{ height: '1.5rem' }">
    <div
      v-if="data"
      class="flex flex-wrap gap-1"
    >
      <Tag
        v-for="item in array"
        v-bind="$attrs"
        :key="item"
        :severity="getSeverity(item)"
        :value="getValue(item)"
      />
    </div>
    <span v-else>-</span>
  </AwaitLoading>
</template>
<script setup>
import { computed } from "vue";
import { Tag } from "primevue";
import { useFormat, useLocalization, useSeverities } from "#imports";

const { asCamelCase } = useFormat();
const { localize: l } = useLocalization();
const severities = useSeverities();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { localizeText, localizationPrefix } = schema;

const array = computed(() => Array.isArray(data) ? data : [data]);
const severity = severities.compute({ name: asCamelCase(localizationPrefix) });

function getSeverity(value) {
  return severity?.[value] ?? "secondary";
};

function getValue(value) {
  if(!localizeText) { return value; }
  if(!localizationPrefix) { return l(value); }

  return l(`${localizationPrefix}.${value.charAt(0).toUpperCase()}${value.slice(1)}`);
}
</script>