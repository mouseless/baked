import { defineNuxtPlugin, useRouter, clearError, showError } from "#app";
import type { ToastServiceMethods } from "primevue";
import useToast from "../composables/useToast";
import type { ErrorHandler } from "../types/errorHandling";
import type { MessageOptions } from "../types/errorHandling"

const handlers = [] as  Array<ErrorHandler>

export default defineNuxtPlugin({
  name: "errorHandling",
  enforce: "pre",
  
  setup(){
    const clienthandlerImports = import.meta.glob('@/handlers/*.*');
    const deafultHandlers = import.meta.glob('../handlers/*.*');
    const handlerImports = {...clienthandlerImports, ...deafultHandlers};

    handlers.length = 0;
    Object.values<() => Promise<any>>(handlerImports).forEach(value => {
      value().then(result =>{
        const handler = result.default as ErrorHandler;
        if(handler){
          handlers.push(handler);
        }
      })
    });

    handlers.sort((a,b) => a.order - b.order);
  },
  hooks: {
    "vue:error": async (error:any) => {
      const router = useRouter();
      const toast = useToast() as ToastServiceMethods;

      let handlerResult = error;
      for (let i = 0; i < handlers.length; i++) {
        const handler = handlers[i];
        if(handler.canHandle(router.currentRoute.value.fullPath, error)){
          handlerResult = handler.handle(router.currentRoute.value.fullPath, error);
          
          break;
        }
      }

      if((handlerResult as  MessageOptions).summary !== undefined){
        await clearError(error);
        toast.add({...handlerResult});
      }else{
        showError(error);
      }
    }
  }
});
  
