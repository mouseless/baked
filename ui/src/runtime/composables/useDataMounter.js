import { onMounted, ref } from "vue";
import { useContext, useDataFetcher } from "#imports";

export default function() {
  const context = useContext();
  const dataFetcher = useDataFetcher();
  const contextData = context.injectContextData();

  function mount(schema) {
    const value = ref(null);

    if(!schema) {
      return value;
    }

    value.value = dataFetcher.get({ data: schema, contextData });

    const shouldLoad = dataFetcher.shouldLoad(schema);
    if(!shouldLoad) {
      return value;
    }

    onMounted(async() => {
      value.value = await dataFetcher.fetch({ data: schema, contextData });
    });

    return value;
  }

  return {
    mount
  };
}
