import { onMounted, ref } from "vue";
import { useContext, useDataFetcher } from "#imports";

export default function() {
  const context = useContext();
  const dataFetcher = useDataFetcher();
  const contextData = context.injectContextData();
  const error = context.injectError({ handle: false });
  const mounts = [];
  let onAfterMountCallback = null;
  let onBeforeMountCallback = null;

  onMounted(async() => {
    if(onBeforeMountCallback) {
      await onBeforeMountCallback();
    }

    await Promise.all(
      mounts.map(async({ value, schema, inlineError }) => {
        try {
          value.value = await dataFetcher.fetch({ data: schema, contextData });
        } catch (err) {
          if(inlineError && err.statusCode === 400) {
            error.value = err;

            return;
          }

          throw err;
        }
      })
    );

    if(onAfterMountCallback) {
      await onAfterMountCallback();
    }
  });

  function mount(schema, { inlineError } = {}) {
    const value = ref(null);

    if(!schema) {
      return value;
    }

    value.value = dataFetcher.get({ data: schema, contextData });

    const shouldLoad = dataFetcher.shouldLoad(schema);
    if(shouldLoad) {
      mounts.push({ value, schema, inlineError });
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