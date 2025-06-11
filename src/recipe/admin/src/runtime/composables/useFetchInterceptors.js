export default function() {
  // Global registry
  if(!globalThis.__fetchInterceptors) {
    globalThis.__fetchInterceptors = new Map();
  }

  let interceptors = null;
  let sorted = false;

  function register(name, interceptor, priority = 100) {
    globalThis.__fetchInterceptors.set(name, { interceptor, priority });
    sorted = false;
  }

  function ensureSorted() {
    if(sorted && interceptors) { return; }

    interceptors = Array.from(globalThis.__fetchInterceptors.entries())
      .sort(([, a], [, b]) => a.priority - b.priority)
      .map(([_, { interceptor }]) => interceptor);
    sorted = true;
  }

  async function execute(context, nuxtApp) {
    ensureSorted();

    for(const interceptor of interceptors) {
      await interceptor(context, nuxtApp);
    }
  }

  return {
    register,
    execute
  };
};