import { defineNuxtPlugin, useRouter, clearError, showError, useNuxtApp } from "#app";
import type { ToastServiceMethods } from "primevue";
import useToast from "../composables/useToast";
import type { ErrorHandler } from "../types/errorHandling";
import type { MessageOptions } from "../types/errorHandling"

export default defineNuxtPlugin({
  name: "errorHandling",
  enforce: "pre",
  async setup() {
    const errorHandlers = [] as  Array<ErrorHandler>;
    await loadHandlers(errorHandlers);
    errorHandlers.sort((a,b) => a.order - b.order);

    return {
      provide: {
        errorHandlers
      }
    }
  },
  hooks: {
    "vue:error": async (error:any) => {
      const handlers = useNuxtApp().$errorHandlers as Array<ErrorHandler>;
      const router = useRouter();
      const toast = useToast() as ToastServiceMethods;

      let handlerResult = error;
      for (const handler of handlers) {
        if(handler.canHandle(router.currentRoute.value.fullPath, error)) {
          handlerResult = handler.handle(router.currentRoute.value.fullPath, error);
          
          break;
        }
      }

      if((handlerResult as  MessageOptions).summary !== undefined) {
        await clearError(error);
        toast.add({...handlerResult});
      } else {
        showError(error);
      }
    }
  }
});

async function loadHandlers(handlers: Array<ErrorHandler>) {
  const clientHandlerImports = import.meta.glob('@/handlers/*.*');
  const defaultHandlers = import.meta.glob('../handlers/*.js');
  const handlerImports = Object.values<() => Promise<any>>({...clientHandlerImports, ...defaultHandlers});
  
  for (const element of handlerImports) {
    const result = await element();
    const handler = result.default as ErrorHandler;
    if(handler){
      handlers.push(handler);
    }
  }
}
  
