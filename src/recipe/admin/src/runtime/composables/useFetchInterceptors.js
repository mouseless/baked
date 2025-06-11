export default function() {
  // Global registry
  if(!globalThis.__fetchInterceptors) {
    globalThis.__fetchInterceptors = new Map();
  }

  const interceptors = globalThis.__fetchInterceptors;
  let sortedInterceptors = null;
  let isSorted = false;

  const register = (name, interceptor, priority = 100) => {
    interceptors.set(name, { interceptor, priority });
    isSorted = false;
  };

  const getAll = () => {
    if(isSorted && sortedInterceptors) {
      return sortedInterceptors;
    }

    sortedInterceptors = Array.from(interceptors.entries())
      .sort(([, a], [, b]) => a.priority - b.priority)
      .map(([name, { interceptor }]) => ({ name, interceptor }));

    isSorted = true;

    return sortedInterceptors;
  };

  const execute = async(context, nuxtApp) => {
    const interceptors = getAll();

    for(const { interceptor } of interceptors) {
      await interceptor(context, nuxtApp);
    }
  };

  return {
    register,
    getAll,
    execute
  };
};