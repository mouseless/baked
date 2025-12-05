<template>
  <AwaitLoading :skeleton="{ height:'2rem', class:'inline-block bg-red-500/20' }">
    <Button
      v-if="inlinesData"
      v-bind="$attrs"
      :label
      size="small"
      severity="danger"
      class="px-1 py-0 hover:underline"
      @click="visible = true"
    />
    <Button
      v-else
      v-bind="$attrs"
      :label
      size="small"
      severity="danger"
      icon="pi pi-plus"
      @click="visible = true"
    />
  </AwaitLoading>
  <Dialog
    v-model:visible="visible"
    modal
    header="Missing Component"
    class="min-w-[500px] max-w-[750px] mx-4"
  >
    <div class="flex flex-col gap-4 w-full">
      <code>{{ path.join('/') }}</code>
      <span>A component descriptor is required but missing here.</span>
      <span v-if="code">
        You can use below convention in your theme feature
        (<code>...ThemeFeature.cs</code>) or in one of your UI override
        features (<code>...UiOverrideFeature.cs</code>).
      </span>
      <code
        v-if="code"
        class="flex flex-col"
      >
        <Button
          :icon="copied ? 'pi pi-check' : 'pi pi-copy'"
          variant="text"
          severity="secondary"
          size="small"
          class="self-end p-1 hover:bg-transparent hover:text-white"
          @click="copyToClipboard"
        />
        <!-- eslint-disable vue/no-v-html -->
        <pre
          class="
            -mt-[2.5em]
            block rounded-lg overflow-auto bg-zinc-950 p-4
            text-xs text-sky-300
          "
          v-html="highlightedCode"
        />
        <!-- eslint-enable vue/no-v-html -->
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
const copied = ref(false);

const code = computed(() => {
  if(!source) { return null; }
  if(!source.type) { return null; }

  return source.type.startsWith("Type") ? renderTypeSample(source.path) :
    source.type.startsWith("Property") ? renderPropertySample(source.path) :
      source.type.startsWith("Method") ? renderMethodSample(source.path) :
        source.type.startsWith("Parameter") ? renderParameterSample(source.path) :
          null;
});

const highlightedCode = computed(() => {
  if(!code.value) { return null; }

  return highlightCSharp(code.value);
});

// AI-GEN provide below code samples and asked for the minimal csharp syntax
// higlighter.
function highlightCSharp(src) {
  if(!src) return "";

  let s = src
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/=/g, "&equals;");

  const kw = ["nameof"];
  const kwRe = new RegExp(`\\b(${kw.join("|")})\\b`, "g");
  s = s.replace(kwRe, "<span class='c--code-keyword'>$1</span>");

  s = s.replace(/"([^"]*)"/g, "<span class='c--code-string'>\"$1\"</span>");
  s = s.replace(/&lt;([^&]+)&gt;/g, "<span class='c--code-type'>&lt;$1&gt;</span>");
  s = s.replace(/(\w+)\s*\(/g, "<span class='c--code-method'>$1</span>(");
  s = s.replace(/(\.|\(|\)|&amp;|&lt;|&gt;|&equals;)/g, "<span class='c--code-symbol'>$1</span>");

  return s;
}

function renderTypeSample([ type ]) {
  return String.raw`builder.Conventions.AddTypeComponent(
    when: c => c.Type.Is<${type}>(),
    where: cc => cc.Path.EndsWith(${path.map(p => `"${p}"`).join(", ")}),
    component: () => B.Text()
);`;
}

function renderPropertySample([ type, property ]) {
  return String.raw`builder.Conventions.AddPropertyComponent(
    when: c => c.Type.Is<${type}>() && c.Property.Name == nameof(${type}.${property}),
    where: cc => cc.Path.EndsWith(${path.map(p => `"${p}"`).join(", ")}),
    component: () => B.Text()
);`;
}

function renderMethodSample([ type, method ]) {
  return String.raw`builder.Conventions.AddMethodComponent(
    when: c => c.Type.Is<${type}>() && c.Method.Name == nameof(${type}.${method}),
    where: cc => cc.Path.EndsWith(${path.map(p => `"${p}"`).join(", ")}),
    component: () => B.Text()
);`;
}

function renderParameterSample([ type, method, parameter ]) {
  return String.raw`builder.Conventions.AddParameterComponent(
    when: c => c.Type.Is<${type}>() && c.Method.Name == nameof(${type}.${method}) && c.Parameter.Name == "${parameter}",
    where: cc => cc.Path.EndsWith(${path.map(p => `"${p}"`).join(", ")}),
    component: () => B.InputText()
);`;
}

async function copyToClipboard() {
  try {
    await navigator.clipboard.writeText(code.value);
    copied.value = true;
    setTimeout(() => copied.value = false, 1000);
  } catch {
    console.log("clipboard copy failed");
  }

}
</script>
<style>
code:not(:has(pre)) {
  @apply rounded p-1 text-xs bg-zinc-50 text-orange-700 dark:bg-zinc-950 dark:text-orange-300;
}

pre {
  .c--code-string { @apply text-orange-300; }
  .c--code-keyword { @apply text-purple-400; }
  .c--code-symbol { @apply text-gray-100; }
  .c--code-type { @apply text-blue-400; }
  .c--code-method { @apply text-yellow-100; }
}
</style>
