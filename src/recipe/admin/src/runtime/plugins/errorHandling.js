import { defineNuxtPlugin, useRouter, clearError, showError, useNuxtApp, useRuntimeConfig } from "#app";
import useToast from "../composables/useToast";
import useComposableResolver from "../composables/useComposableResolver";

export default defineNuxtPlugin({
  name: "errorHandling",
  enforce: "pre ",
  async setup() {
    const { public: { errorHandling } } = useRuntimeConfig();

    const errorHandlers = errorHandling.handlers;
    errorHandlers.sort((a,b) => a.order - b.order);

    return {
      provide: {
        errorHandlers,
        defaultAlert: { title: errorHandling.defaultAlertTitle, message: errorHandling.defaultAlertMessage }
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

      const arg = handler.behaviorArgument.type == "Computed"
        ? (await useComposableResolver().resolve(handler.behaviorArgument.value)).default()
        : handler.behaviorArgument.value;

      if(handler.behavior === "Alert") {
        await clearError(error);
        toast.add({ ...getMessage(error) });
      } else if(handler.behavior === "Redirect") {
        await clearError(error);
        toast.add({ ...getMessage(error) });
        router.replace(arg);
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
  const defaultAlert = useNuxtApp().$defaultAlert;

  if(error.name === "FetchError") {
    return {
      severity: "error",
      summary: error.data?.title ?? error.statusCode ?? defaultAlert.title,
      detail: error.data?.detail ?? error.message ?? error.cause ?? defaultAlert.message,
      life: 3000
    };
  }

  return {
    severity: "error",
    summary: error.statusCode ?? error.status ?? defaultAlert.title,
    detail: error.message ?? error.cause ?? defaultAlert.message,
    life: 3000
  };
}