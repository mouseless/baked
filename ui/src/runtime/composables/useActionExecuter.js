import { useRuntimeConfig } from "#app";
import { useComposableResolver, useDataFetcher, usePathBuilder } from "#imports";

export default function() {
  const actions = {
    "Composite": Composite({ actionExecuter: { execute } }),
    "Emit": Emit(),
    "Local": Local(),
    "Remote": Remote()
  };

  async function execute({ action, injectedData, events }) {
    const executer = actions[action?.type];

    await executer.execute({ action, injectedData, events });
  }

  return {
    execute
  };
}

function Composite({ actionExecuter }) {
  async function execute({ action, injectedData, events }) {
    for(const part of action.parts) {
      await actionExecuter.execute({ action: part, injectedData, events });
    }
  }

  return {
    execute
  };
}

function Emit() {
  async function execute({ action, events }) {
    events.emit(action.eventKey);
  }

  return {
    execute
  };
}

function Local() {
  const composableResolver = useComposableResolver();

  async function execute({ action }) {
    const composable = (await composableResolver.resolve(action.composable)).default();

    if(composable.run) {
      return await composable.run(...(action.args || []));
    }

    throw new Error("Action composable should have async `run`");
  }

  return {
    execute
  };
}

function Remote() {
  const dataFetcher = useDataFetcher();
  const pathBuilder = usePathBuilder();
  const { public: { baseURL } } = useRuntimeConfig();

  async function execute({ action, injectedData }) {
    // TODO make sure `ref` values are unreffed
    const headers = action.headers ? await dataFetcher.fetch({ data: action.headers, injectedData }) : { };
    const query = action.query ? await dataFetcher.fetch({ data: action.query, injectedData }) : null;
    const params = action.params ? await dataFetcher.fetch({ data: action.params, injectedData }) : { };
    const body = action.body ? await dataFetcher.fetch({ data: action.body, injectedData })
      : (action.method === "GET" ? null : { });

    const result = await $fetch(pathBuilder.build(action.path, params),
      {
        baseURL: baseURL,
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
