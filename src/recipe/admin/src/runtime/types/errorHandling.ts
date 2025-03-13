import type { ToastMessageOptions } from "primevue";
import { useRuntimeConfig } from "#app";

export interface ErrorHandlingOptions{
  defaultHandler: DefaulHandlerOptions
}

export interface DefaulHandlerOptions {
  config: Array<HandlerOption>
}

export interface HandlerOption{
  routePattern?: string,
  statusCode?: number,
  result?: string
}

export interface MessageOptions extends ToastMessageOptions { }

export interface ErrorHandler {
  order: number,
  canHandle(route: String, error: Error): boolean,
  handle(route: String,  error: Error): Error | MessageOptions
}

export function defineErrorHandler(handler: ErrorHandler | ((options: ErrorHandlingOptions) => ErrorHandler)) : ErrorHandler {
  if(typeof handler === "function"){
    const options = useRuntimeConfig().public.errorHandling as ErrorHandlingOptions
    return handler(options);
  }

  return handler;
}