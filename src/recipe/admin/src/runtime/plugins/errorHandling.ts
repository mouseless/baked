import { defineNuxtPlugin, useRouter, clearError, showError } from "#app";
import type { ToastServiceMethods } from "primevue";
import useToast from "../composables/useToast";
import type { ErrorHandler, MessageOptions } from "../types/errorHandling";

const handlers = [] as  Array<ErrorHandler>

export default defineNuxtPlugin({
  name: "errorHandling",
  enforce: "pre",
  async setup(){
    handlers.length = 0;
    
    const clienthandlerImports = import.meta.glob('@/handlers/*.*');
    const deafultHandlers = import.meta.glob('../handlers/*.*');
    const handlerImports = {...clienthandlerImports, ...deafultHandlers};
    Object.values<() => Promise<any>>(handlerImports).forEach(value => {
      value().then(result =>{
        const handler = result.default as ErrorHandler;
        if(handler){
          handlers.push(handler);
        }
      })
    });
  },
  hooks: {
    "vue:error": async (error:any) => {
      const router = useRouter();
      const toast = useToast() as ToastServiceMethods;

      for (let i = 0; i < handlers.length; i++) {
        const handler = handlers[i];
        if(handler.canHandle(router.currentRoute.value.fullPath, error)){
          const result = handler.handle(router.currentRoute.value.fullPath, error) as MessageOptions;
          if(result){
            await clearError(error);
            toast.add(result);
          }   
          return;
        }
      }
      showError(error);
    }
  }
});
  
