<template>
  <template v-if="!error || errorHandled || errorCausedByAction">
    <component
      :is="component"
      v-if="visible"
      ref="componentRef"
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
    <ErrorPopover
      v-if="descriptor.action && !errorHandled"
      ref="errorPopoverRef"
    >
      <InlineError :error />
    </ErrorPopover>
  </template>
  <InlineError
    v-else
    :class="$attrs.class"
    :style="$attrs.style"
    :error
  />
</template>
<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from "vue";
import { useActionExecuter, useBakeError, useComponentResolver, useContext, useDataFetcher, useFormat, useReactionHandler } from "#imports";
import { ErrorPopover, InlineError } from "#components";

const actionExecuter = useActionExecuter();
const { normalize: normalizeError } = useBakeError();
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
const events = context.injectEvents();
const contextData = context.injectContextData();

const component = componentResolver.resolve(descriptor.type, "MissingComponent");
const componentRef = ref();
const path = parentPath && parentPath !== "" ? `${parentPath}/${name}` : name;
const data = ref(dataFetcher.get({ data: descriptor.data, contextData }));
const shouldLoad = !parentLoading.value && dataFetcher.shouldLoad(descriptor.data);
const loading = ref(shouldLoad);
const visible = ref(true);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];
const baseAttrs = { };
const rawError = ref();
const errorHandled = context.provideError(rawError);
const error = normalizeError(rawError);

context.providePath(path);
context.provideDataDescriptor(descriptor.data);
context.provideParentContext({ ...contextData.parent, data, grand: contextData.parent });

if(shouldLoad) {
  context.provideLoading(loading);
}

let errorPopoverRef = null;
let errorCausedByAction = null;
let executing = null;
if(descriptor.action) {
  errorPopoverRef = ref();
  errorCausedByAction = ref(false);
  executing = ref(false);
  context.provideExecuting(executing);

  watch([executing, error], ([newExecuting, newError], [oldExecuting, oldError]) => {
    if(newExecuting) {
      rawError.value = null;
      errorCausedByAction.value = false;
    } else if(oldExecuting && newError && !oldError) {
      errorCausedByAction.value = true;
    }
  });

  watch(error, newError => {
    if(newError) {
      errorPopoverRef.value?.show(componentRef);
    } else {
      errorPopoverRef.value?.hide();
    }
  });
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

    rawError.value = err;
  }
  loading.value = false;
  emit("loaded");
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
  } catch (err) {
    if(err?.status !== 400) { throw err; }

    rawError.value = err;
  } finally {
    executing.value = false;
  }
}
</script>