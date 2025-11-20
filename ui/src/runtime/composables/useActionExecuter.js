import { useComposableResolver, useDataFetcher, useRemoteClient } from "#imports";

export default function() {

  const actions = {
    "Composite": Composite({ actionExecuter: { execute } }),
    "Local": Client(),
    "Remote": Remote()
  };

  function Client() {
    const composableResolver = useComposableResolver();

    async function execute(action) {
      const composable = (await composableResolver.resolve(action.composable)).default();

      if(composable.execute) {
        return composable.execute(action.args);
      }

      if(composable.executeAsync) {
        return await composable.executeAsync(action.args);
      }

      throw new Error("Action composable should have either `execute` or `executeAsync`");
    }

    return {
      execute
    };
  }

  function Composite({ actionExecuter }) {
    async function execute(action) {
      for(const part of action.parts) {
        await actionExecuter.execute(part);
      }
    }

    return {
      execute
    };
  }

  function Remote() {
    const dataFetcher = useDataFetcher();
    const remoteClient = useRemoteClient();

    async function execute(action) {
      const headers = action.headers ? await dataFetcher.fetch({ data: action.headers }) : { };
      const query = action.query ? await dataFetcher.fetch({ data: action.query }) : null;
      const params = action.params ? await dataFetcher.fetch({ data: action.params }) : { };
      const body = action.body ? await dataFetcher.fetch({ data: action.body })
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

  async function execute(action) {
    const executer = actions[action?.type];

    await executer.execute(action);
  }

  return {
    execute
  };
}