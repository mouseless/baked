<template>
  <div
    v-if="!loading"
    class="b-error p-8"
  >
    <div class="pt-8 space-y-4">
      <Tag
        severity="danger"
        :value="statusCode"
        class="text-4xl"
      />
      <h1 class="b-title text-6xl">
        {{ errorInfo.title }}
      </h1>
      <div class="b-message text-2xl">
        {{ errorInfo.message }}
      </div>
      <div class="text-2xl">
        Try the links from the menu below to view the page you want to access.
      </div>
    </div>
    <Divider
      type="dashed"
      class="my-8"
    />
    <div class="b-links grid gap-4 grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-6">
      <Bake
        v-for="link in safeLinks"
        :key="link.schema.route"
        :descriptor="link"
      />
    </div>
    <Divider
      type="dashed"
      class="my-8"
    />
    <Message severity="warn">
      <i class="pi pi-exclamation-circle mr-2" />
      If you cannot reach the page you want, please contact the system administrator.
    </Message>
  </div>
</template>
<script setup>
import { computed } from "vue";
import { Tag, Divider, Message } from "primevue";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null },
  loading: { type: Boolean, default: false }
});

const { safeLinks, errorInfos } = schema;

const statusCode = computed(() => data?.data?.status ?? data?.statusCode ?? 500);
const errorInfo = computed(() => errorInfos[`${statusCode.value}`] ?? errorInfos["500"]);
</script>
