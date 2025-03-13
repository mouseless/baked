import { defineErrorHandler } from "../types/errorHandling";

export default defineErrorHandler(options => {
  const defaultHandlerOptions = options.defaultHandler;

  return {
    order: Number.MAX_VALUE,
    canHandle: function(route, error) {
      if(!defaultHandlerOptions) {
        return true;
      }

      return getHandleOption(route, error, defaultHandlerOptions) !== null;
    },
    handle: function(route, error)
    {
      if(!defaultHandlerOptions.config) {
        return error;
      }

      const handleOption = getHandleOption(route, error, defaultHandlerOptions.config);

      if(handleOption && handleOption.result === "toast") {
        return getMessage(error);
      }

      return error;
    }
  };
});

function getHandleOption(route, error, config) {
  if(!config) {
    return null;
  }

  for(const option of Object.values(config)) {
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