<template>
  <component
    :is="render()"
    v-if="visible"
    :key="loading"
    :class="classes"
  >
    <slot v-if="$slots.default" />
  </component>
</template>
<script setup>
import { h, onMounted, onUnmounted, ref, watch } from "vue";
import { useActionExecuter, useComponentResolver, useConstraintEvaluator, useContext, useDataFetcher, useFormat } from "#imports";

const actionExecuter = useActionExecuter();
const componentResolver = useComponentResolver();
const constraintEvaluator = useConstraintEvaluator();
const context = useContext();
const dataFetcher = useDataFetcher();
const { asClasses } = useFormat();

const { name, descriptor } = defineProps({
  name: { type: String, required: true },
  descriptor: { type: null, required: true }
});
const model = defineModel({ type: null });
const emit = defineEmits(["loaded"]);

context.providePath(name);
context.provideDataDescriptor(descriptor.data);

const path = context.injectPath();
const events = context.injectEvents();
const contextData = context.injectContextData();
const is = componentResolver.resolve(descriptor.type, "MissingComponent");
const data = ref(dataFetcher.get({ data: descriptor.data, contextData }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const loading = ref(shouldLoad);
const executing = ref(false);
const visible = ref(true);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];
const eventsToUnsubscribe = [];

context.provideParentContext({ ...contextData.parent, data });
context.provideExecuting(executing);

if(shouldLoad) {
  context.provideLoading(loading);
}

if(descriptor.reactions) {
  const reactions = {
    reload(success) {
      if(success) {
        load();
      }
    },
    show(success) {
      visible.value = success;
    }
  };

  for(const reaction in descriptor.reactions) {
    const trigger = descriptor.reactions[reaction];
    const react = reactions[reaction];

    eventsToUnsubscribe.push(
      ...hook(trigger, react)
    );
  }
}

onMounted(async() => {
  if(!shouldLoad) { return; }

  await load();
});

onUnmounted(() => {
  for(const event of eventsToUnsubscribe) {
    events.off(event, `${path}:bake`);
  }
});

function render() {
  const props = { };
  if(descriptor.schema) { props.schema = descriptor.schema; }
  if(descriptor.data) { props.data = data.value; }
  if(is.emits?.includes("submit")) { props.onSubmit = onModelUpdate; }
  if(is.props?.modelValue) {
    props.modelValue = model.value;
    props["onUpdate:modelValue"] = onModelUpdate;
  }

  return h(is, props);
}

async function load() {
  loading.value = true;
  data.value = await dataFetcher.fetch({
    data: descriptor.data,
    contextData
  });
  loading.value = false;
  emit("loaded");
}

async function onModelUpdate(newModel) {
  if(is.props?.modelValue) {
    model.value = newModel;
  }

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

function hook(trigger, react) {
  if(trigger.type === "On") {
    events.on(trigger.on, `${path}:bake`, async value => {
      react(await constraintEvaluator.evaluate({ constraint: trigger.constraint, value, contextData }));
    });

    return [trigger.on];
  } else if(trigger.type === "When") {
    watch(() => contextData.page[trigger.when], async(value, oldValue) => {
      if(value === oldValue || value === undefined) { return; }

      react(await constraintEvaluator.evaluate({ constraint: trigger.constraint, value, contextData }));
    }, { immediate: true });

    return [];
  } else if(trigger.type === "Composite") {
    const result = [];
    for(const part of trigger.parts) {
      result.push(
        ...hook(part, react)
      );
    }

    return result;
  }
}
</script>
