import { defineNuxtPlugin, useRouter, clearError, showError, useNuxtApp, useRuntimeConfig } from "#app";
import useToast from "../composables/useToast";

export default defineNuxtPlugin({
  name: "errorHandling",
  enforce: "pre",
  async setup() {
    const config = useRuntimeConfig().public.errorHandling;

    const errorHandlers = config.handlers;
    errorHandlers.sort((a,b) => a.order - b.order);

    return {
      provide: {
        errorHandlers
      }
    };
  },
  hooks: {
    "vue:error": async error => {
      const handlers = useNuxtApp().$errorHandlers;
      const router = useRouter();
      const toast = useToast();

      const handler = getHandler(handlers, router.currentRoute.value.fullPath, error);

      if(!handler) {
        showError(error);

        return;
      }

      if(handler.behavior === "Alert") {
        await clearError(error);
        toast.add({ ...getMessage(error) });
      } else if(handler.behavior === "Redirect") {
        await clearError(error);
        toast.add({ ...getMessage(error) });
        await router.replace(handler.behaviorArgument);
      } else {
        showError(error);
      }
    }
  }
});

// handlers
function getHandler(handlers, route, error) {
  if(!handlers) {
    return null;
  }

  for(const option of Object.values(handlers)) {
    if(
      (!option.statusCode || error.statusCode === option.statusCode) &&
      (!option.routePattern || route.match(`/${option.routePattern}/`).length > 0)
    ) {
      return option;
    }
  }

  return null;
}

function getMessage(error) {
  if(error.name === "FetchError") {
    return {
      severity: "error",
      summary: error.data?.title ?? error.statusCode,
      detail: error.data?.detail ?? error.message ?? error.cause,
      life: 3000
    };
  }

  return {
    severity: "error",
    summary: error.statusCode ?? error.status ?? "Unexpected Error",
    detail: error.message ?? error.cause ?? "Please contact system administrator",
    life: 3000
  };
}