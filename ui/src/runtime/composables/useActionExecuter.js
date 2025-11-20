import { useComposableResolver, useDataFetcher, useRemoteClient } from "#imports";

export default function() {
  const actions = {
    "Composite": Composite({ actionExecuter: { execute } }),
    "Local": Client(),
    "Remote": Remote()
  };

  function Client() {
    const composableResolver = useComposableResolver();

    async function execute(action, injectedData, events) {
      const composable = (await composableResolver.resolve(action.composable)).default();

      if(composable.execute) {
        return composable.execute(action.args, injectedData, events);
      }

      if(composable.executeAsync) {
        return await composable.executeAsync(action.args, injectedData, events);
      }

      throw new Error("Action composable should have either `execute` or `executeAsync`");
    }

    return {
      execute
    };
  }

  function Composite({ actionExecuter }) {
    async function execute(action, injectData, events) {
      for(const part of action.parts) {
        await actionExecuter.execute({ action: part, injectData, events });
      }
    }

    return {
      execute
    };
  }

  function Remote() {
    const dataFetcher = useDataFetcher();
    const remoteClient = useRemoteClient();

    async function execute(action, injectedData) {
      const headers = action.headers ? await dataFetcher.fetch({ data: action.headers, injectedData }) : { };
      const query = action.query ? await dataFetcher.fetch({ data: action.query, injectedData }) : null;
      const params = action.params ? await dataFetcher.fetch({ data: action.params, injectedData }) : { };
      const body = action.body ? await dataFetcher.fetch({ data: action.body, injectedData })
        : (action.method === "GET" ? null : { });

      await remoteClient.send({
        path: action.path,
        method: action.method,
        headers: headers,
        query: query,
        params: params,
        body: body
      });
    }

    return {
      execute
    };
  }

  async function execute({ action, injectedData, events }) {
    const executer = actions[action?.type];

    await executer.execute(action, injectedData, events);
  }

  return {
    execute
  };
}