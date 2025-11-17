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

      if(composable.executeAsync) {
        return await composable.executeAsync(action.args);
      }

      throw new Error("Action composable should `executeAsync`");
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

      const params = await dataFetcher.fetch({ data: action.params });

      await remoteClient.send({
        path: action.path,
        params: params,
        method: action.method,
        headers: {
          "Authorization": "Authorization"
        }
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