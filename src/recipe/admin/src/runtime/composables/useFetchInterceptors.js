export default function() {
  // Global interceptor registry
  if(!globalThis.__fetchInterceptors) {
    globalThis.__fetchInterceptors = new Map();
  }

  const interceptors = globalThis.__fetchInterceptors;

  const register = (name, interceptor, priority = 100) => {
    interceptors.set(name, { interceptor, priority });
  };

  const unregister = name => {
    interceptors.delete(name);
  };

  const getAll = () => {
    return Array.from(interceptors.entries())
      .sort(([, a], [, b]) => a.priority - b.priority)
      .map(([name, { interceptor }]) => ({ name, interceptor }));
  };

  const execute = async(context, nuxtApp) => {
    const sortedInterceptors = getAll();

    for(const { name, interceptor } of sortedInterceptors) {
      await interceptor(context, nuxtApp);
    }
  };

  return {
    register,
    unregister,
    getAll,
    execute
  };
};