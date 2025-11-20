import { useRuntimeConfig } from "#app";
import { useComposableResolver, useDataFetcher, usePathBuilder } from "#imports";

export default function() {
  const actions = {
    "Composite": Composite({ actionExecuter: { execute } }),
    "Local": Client(),
    "Remote": Remote()
  };

  function Client() {
    const composableResolver = useComposableResolver();

    async function execute({ action, events }) {
      const composable = (await composableResolver.resolve(action.composable)).default();

      if(composable.execute) {
        return composable.execute(...(action.args || []), events);
      }

      if(composable.executeAsync) {
        return await composable.executeAsync(...(action.args || []), events);
      }

      throw new Error("Action composable should have either `execute` or `executeAsync`");
    }

    return {
      execute
    };
  }

  function Composite({ actionExecuter }) {
    async function execute({ action, injectData, events }) {
      for(const part of action.parts) {
        await actionExecuter.execute({ action: part, injectData, events });
      }
    }

    return {
      execute
    };
  }

  function Remote() {
    const { public: { composables } } = useRuntimeConfig();
    const dataFetcher = useDataFetcher();
    const pathBuilder = usePathBuilder();

    async function execute({ action, injectedData }) {
      const headers = action.headers ? await dataFetcher.fetch({ data: action.headers, injectedData }) : { };
      const query = action.query ? await dataFetcher.fetch({ data: action.query, injectedData }) : null;
      const params = action.params ? await dataFetcher.fetch({ data: action.params, injectedData }) : { };
      const body = action.body ? await dataFetcher.fetch({ data: action.body, injectedData })
        : (action.method === "GET" ? null : { });

      const result = await $fetch(pathBuilder.build(action.path, params),
        {
          baseURL: composables.useDataFetcher.baseURL,
          method: action.method,
          headers: headers,
          query: query,
          body: body
        });

      return result;
    }

    return {
      execute
    };
  }

  async function execute({ action, injectedData, events }) {
    const executer = actions[action?.type];

    await executer.execute({ action, injectedData, events });
  }

  return {
    execute
  };
}