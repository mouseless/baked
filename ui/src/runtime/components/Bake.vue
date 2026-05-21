<template>
  <template v-if="!error">
    <component
      :is="component"
      v-if="visible"
      :key="loading"
      :class="classes"
      v-bind="{
        ...$attrs,
        ...baseAttrs,
        ...dataAttrs(),
        ...modelAttrs()
      }"
    >
      <template
        v-for="(_, slotName) in $slots"
        #[slotName]="slotProps"
      >
        <slot
          :name="slotName"
          v-bind="slotProps ?? {}"
        />
      </template>
    </component>
  </template>
  <div
    v-else
    class="
      flex flex-col gap-4 p-4
      rounded-md border border-red-500/10
      text-red-500 bg-red-500/10
      overflow-auto
    "
  >
    <div class="flex gap-3">
      <i class="pi pi-exclamation-circle max-w-min self-center" />
      <h2 class="font-bold">{{ error.summary }}</h2>
    </div>
    <span>{{ error.detail }}</span>
  </div>
</template>
<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from "vue";
import { useActionExecuter, useComponentResolver, useContext, useDataFetcher, useFormat, useReactionHandler } from "#imports";

const actionExecuter = useActionExecuter();
const componentResolver = useComponentResolver();
const context = useContext();
const dataFetcher = useDataFetcher();
const { asClasses } = useFormat();
const reactionHandler = useReactionHandler();

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});
const model = defineModel({ type: null });
const emit = defineEmits(["loaded"]);

const parentPath = context.injectPath();
const parentLoading = context.injectLoading();
const path = parentPath && parentPath !== "" ? `${parentPath}/${name}` : name;
const events = context.injectEvents();
const contextData = context.injectContextData();
const component = componentResolver.resolve(descriptor.type, "MissingComponent");
const data = ref(dataFetcher.get({ data: descriptor.data, contextData }));
const shouldLoad = !parentLoading.value && dataFetcher.shouldLoad(descriptor.data);
const loading = ref(shouldLoad);
const visible = ref(true);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];
const baseAttrs = { };
const error = ref();

context.providePath(path);
context.provideDataDescriptor(descriptor.data);
context.provideParentContext({ ...contextData.parent, data });

if(shouldLoad) {
  context.provideLoading(loading);
}

let executing = null;
if(descriptor.action) {
  executing = ref(false);
  context.provideExecuting(executing);
}

if(descriptor.schema) {
  baseAttrs.schema = descriptor.schema;
}

if(component.emits?.includes("submit")) {
  baseAttrs.onSubmit = executeAction;
}

let dataAttrs = () => ({});
if(descriptor.data) {
  dataAttrs = () => ({ data: data.value });
}

let modelAttrs = () => ({});
if(component.props?.modelValue) {
  modelAttrs = () => ({ modelValue: model.value, "onUpdate:modelValue": value => model.value = value });
  watch(model, executeAction);
}

let reactions = null;
if(descriptor.reactions) {
  reactions = reactionHandler.create(`${path}:bake`, {
    reload(success) {
      if(!success) { return; }

      load();
    },
    show(success) {
      visible.value = success;
    }
  });
  reactions.bind(descriptor.reactions);
}

onMounted(async() => {
  if(shouldLoad) {
    await load();
  }
});

onBeforeUnmount(() => {
  if(descriptor.reactions) {
    reactions.unbind();
  }
});

async function load() {
  loading.value = true;
  try {
    data.value = await dataFetcher.fetch({
      data: descriptor.data,
      contextData
    });
  } catch (err) {
    if(err?.status !== 400) { throw err; }

    error.value = getMessage(err);
  }
  loading.value = false;
  emit("loaded");
}

function getMessage(error) {
  if(error.name === "FetchError") {
    return {
      summary: error.data?.title ?? error.statusCode ?? "ERROR",
      detail: error.data?.detail ?? error.message ?? error.cause ?? "An error occured..."
    };
  }

  return {
    summary: error.statusCode ?? error.status ?? "ERROR",
    detail: error.message ?? error.cause ?? "An error occured..."
  };
}

async function executeAction(newModel) {
  if(!descriptor.action) { return; }

  try {
    executing.value = true;
    await actionExecuter.execute({
      action: descriptor.action,
      contextData: { ...contextData, model: newModel },
      events
    });
  } finally {
    executing.value = false;
  }
}
</script>