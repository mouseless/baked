import { onMounted, ref } from "vue";
import { useContext, useDataFetcher } from "#imports";

export default function() {
  const context = useContext();
  const dataFetcher = useDataFetcher();
  const contextData = context.injectContextData();
  const mounts = [];
  let onAfterMountCallback = null;
  let onBeforeMountCallback = null;

  onMounted(async() => {
    if(onBeforeMountCallback) {
      await onBeforeMountCallback();
    }

    for(const { value, schema } of mounts) {
      value.value = await dataFetcher.fetch({ data: schema, contextData });
    }

    if(onAfterMountCallback) {
      await onAfterMountCallback();
    }
  });

  function mount(schema) {
    const value = ref(null);

    if(!schema) {
      return value;
    }

    value.value = dataFetcher.get({ data: schema, contextData });

    const shouldLoad = dataFetcher.shouldLoad(schema);
    if(shouldLoad) {
      mounts.push({ value, schema });
    }

    return value;
  }

  function onAfterMount(callback) {
    onAfterMountCallback = callback;
  }

  function onBeforeMount(callback) {
    onBeforeMountCallback = callback;
  }

  return {
    mount,
    onAfterMount,
    onBeforeMount
  };
}
