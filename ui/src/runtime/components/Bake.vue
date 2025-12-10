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
import { h, onMounted, onUnmounted, ref } from "vue";
import { useActionExecuter, useComponentResolver, useContext, useDataFetcher, useFormat } from "#imports";

const actionExecuter = useActionExecuter();
const componentResolver = useComponentResolver();
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
const visible = ref(true);
const is = componentResolver.resolve(descriptor.type, "MissingComponent");
const parentContext = context.injectParentContext();
const data = ref(dataFetcher.get({ data: descriptor.data, contextData: { parent: parentContext } }));
const shouldLoad = dataFetcher.shouldLoad(descriptor.data?.type);
const loading = ref(shouldLoad);
const executing = ref(false);
const classes = [`b-component--${descriptor.type}`, ...asClasses(name)];

context.provideParentContext({ ...parentContext, data });
context.provideExecuting(executing);

if(descriptor.on) {
  const reactions = {
    Reload(success) {
      if(success) {
        load();
      }
    },
    Show(success) {
      visible.value = success;
    },
    Hide(success) {
      visible.value = !success;
    }
  };

  Object.keys(descriptor.on).forEach(key => {
    const [event, constraint] = key.split(":");
    const react = reactions[descriptor.on[event]];

    events.on(event, `${path}:bake`, value => {
      react(
        // if constraint is NOT given
        // it is a success
        constraint === undefined ||

        // if constraint doesn't start with !
        //    and constraint equals to value
        // it is a success
        // e.g.
        //   assume it expects "A"
        //     when value is "A" => "A" === "A" => true
        //     when value is "B" => "A" === "B" => false
        //     when value is "C" => "A" === "C" => false
        //   so it is a success as long as value is "A"
        !constraint.startsWith("!") && constraint === `${value}` ||

        // if constraint starts with !
        //    and constraint doesn't equal to !value
        // it is a success
        // e.g.
        //   assume it expects "!A"
        //     when value is "A" => "!A" !== "!A" => false
        //     when value is "B" => "!A" !== "!B" => true
        //     when value is "C" => "!A" !== "!C" => true
        //   so it is a success as long as value is NOT "A"
        constraint.startsWith("!") && constraint !== `!${value}`
      );
    });
  });
}

if(shouldLoad) {
  context.provideLoading(loading);
}

onMounted(async() => {
  if(!shouldLoad) { return; }

  await load();
});

onUnmounted(() => {
  if(descriptor.on) {
    Object.keys(descriptor.on).forEach(event =>{
      events.off(event, `${path}:bake`);
    });
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
    contextData: { parent: parentContext }
  });
  loading.value = false;
  emit("loaded");
}

async function onModelUpdate(newModel) {
  if(is.props?.modelValue) {
    model.value = newModel;
  }

  if(!descriptor.action) { return; }

  const contextData = { parent: parentContext };
  if(newModel) {
    contextData.model = newModel;
  }

  try {
    executing.value = true;
    await actionExecuter.execute({ action: descriptor.action, contextData, events });
  } finally {
    executing.value = false;
  }
}
</script>
