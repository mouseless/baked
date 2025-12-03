import { useRuntimeConfig } from "#app";
import { useComposableResolver, useDataFetcher, usePathBuilder, useUnref } from "#imports";

export default function() {
  const actions = {
    "Composite": Composite({ actionExecuter: { execute } }),
    "Emit": Emit(),
    "Local": Local(),
    "Remote": Remote()
  };

  async function execute({ action, contextData, events }) {
    const executer = actions[action?.type];

    await executer.execute({ action, contextData, events });
  }

  return {
    execute
  };
}

function Composite({ actionExecuter }) {
  async function execute({ action, contextData, events }) {
    for(const part of action.parts) {
      await actionExecuter.execute({ action: part, contextData, events });
    }
  }

  return {
    execute
  };
}

function Emit() {
  async function execute({ action, events }) {
    await events.emit(action.eventKey);
  }

  return {
    execute
  };
}

function Local() {
  const composableResolver = useComposableResolver();
  const dataFetcher = useDataFetcher();

  async function execute({ action, contextData }) {
    const composable = (await composableResolver.resolve(action.composable)).default();

    if(composable.run) {
      const options = action.options ? await dataFetcher.fetch({ data: action.options, contextData }) : { };

      return await composable.run(options);
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
  const { public: { apiBaseURL } } = useRuntimeConfig();
  const unref = useUnref();

  async function execute({ action, contextData }) {
    const headers = action.headers ? unref.deepUnref(await dataFetcher.fetch({ data: action.headers, contextData })) : { };
    const query = action.query ? unref.deepUnref(await dataFetcher.fetch({ data: action.query, contextData })) : null;
    const params = action.params ? unref.deepUnref(await dataFetcher.fetch({ data: action.params, contextData })) : { };
    const body = action.method === "GET" ? null
      : (action.body ? unref.deepUnref(await dataFetcher.fetch({ data: action.body, contextData })) : { });

    const result = await $fetch(pathBuilder.build(action.path, params), {
      baseURL: apiBaseURL,
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
