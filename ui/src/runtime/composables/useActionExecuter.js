import { useRuntimeConfig } from "#app";
import { useComposableResolver, useDataFetcher, usePathBuilder, useUnref } from "#imports";

export default function() {
  const actions = {
    "Composite": Composite({ actionExecuter: { execute } }),
    "Local": Local(),
    "Publish": Publish(),
    "Remote": Remote({ actionExecuter: { execute } })
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

function Local() {
  const composableResolver = useComposableResolver();
  const dataFetcher = useDataFetcher();

  async function execute({ action, contextData }) {
    const composable = composableResolver.resolve(action.composable).default();

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

function Publish() {
  const dataFetcher = useDataFetcher();

  async function execute({ action, contextData, events }) {
    const data = action.data ? await dataFetcher.fetch({ data: action.data, contextData }) : undefined;

    if(action.event) {
      await events.publish(action.event, data);
    }

    if(action.pageContextKey) {
      contextData.page[action.pageContextKey] = data;
    }
  }

  return {
    execute
  };
}

function Remote({ actionExecuter }) {
  const dataFetcher = useDataFetcher();
  const pathBuilder = usePathBuilder();
  const { public: { apiBaseURL } } = useRuntimeConfig();
  const unref = useUnref();

  async function execute({ action, contextData, events }) {
    const headers = action.headers ? unref.deepUnref(await dataFetcher.fetch({ data: action.headers, contextData })) : { };
    const query = action.query ? unref.deepUnref(await dataFetcher.fetch({ data: action.query, contextData })) : null;
    const params = action.params ? unref.deepUnref(await dataFetcher.fetch({ data: action.params, contextData })) : { };
    const body = action.method === "GET"
      ? null
      : (action.body ? unref.deepUnref(await dataFetcher.fetch({ data: action.body, contextData })) : { });

    const response = await $fetch(pathBuilder.build(action.path, params), {
      baseURL: apiBaseURL,
      method: action.method,
      headers: headers,
      query: query,
      body: body
    });

    await actionExecuter.execute({
      action: action.postAction,
      contextData: { ...contextData, response },
      events
    });
  }

  return {
    execute
  };
}
