<template>
  <AwaitLoading :skeleton="{ height:'2rem', class:'inline-block' }">
    <Button
      v-bind="$attrs"
      :label
      :icon="inlinesData ? undefined : 'pi pi-plus'"
      size="small"
      severity="danger"
      @click="visible = true"
    />
  </AwaitLoading>
  <Dialog
    v-model:visible="visible"
    modal
    header="Missing Component"
  >
    <div class="flex flex-col gap-4">
      <span>
        A component descriptor was expected at path
        <code class="text-xs rounded bg-zinc-800 p-1">{{ path.join('/') }}</code>
      </span>
      <code v-if="code">
        <pre class="block rounded-lg text-xs bg-zinc-950">{{ code }}</pre>
      </code>
    </div>
  </Dialog>
</template>
<script setup>
import { computed, ref } from "vue";
import { Button, Dialog } from "primevue";
import { AwaitLoading } from "#components";

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { path, source } = schema;

const inlinesData = data !== undefined && typeof data !== "object";
const label = inlinesData ? data : "Configure";
const visible = ref(false);

const code = computed(() => {
  if(!source) { return null; }

  return renderTypeSample(source.path);
});

function renderTypeSample([ type ]) {
  console.log(type);

  return String.raw`
  builder.Conventions.AddTypeComponent(
      component: () => B.Text(),
      when: c => c.Type.Is<${type}>()
  );
  `;
}
</script>
