import { defineNuxtPlugin, useRouter, clearError, showError } from "#app";
import type { ToastServiceMethods } from "primevue";
import useToast from "../composables/useToast";
import type { ErrorHandler } from "../types/errorHandling";
import type { MessageOptions } from "../types/errorHandling"

const handlers = [] as  Array<ErrorHandler>

export default defineNuxtPlugin({
  name: "errorHandling",
  enforce: "pre",
  async setup(){
    await loadHandlers(handlers);
    handlers.sort((a,b) => a.order - b.order)
  },
  hooks: {
    "vue:error": async (error:any) => {
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
  handlers.length = 0;
  const clientHandlerImports = import.meta.glob('@/handlers/*.*');
  const defaultHandlers = import.meta.glob('../handlers/*.*');
  const handlerImports = Object.values<() => Promise<any>>({...clientHandlerImports, ...defaultHandlers});

  for (const element of handlerImports) {
    const handler = (await element()).default as ErrorHandler ?? null;
    if(handler){
      handlers.push(handler);
    }
  }
}
  
