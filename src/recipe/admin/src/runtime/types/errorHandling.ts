import type { ToastMessageOptions } from "primevue";

export interface MessageOptions extends ToastMessageOptions{ }

export interface ErrorHandler{
  canHandle(route: String, error: Error): boolean,
  handle(route: String,  error: Error): Error | MessageOptions
}

export function defineErrorHandler(handler: ErrorHandler) : ErrorHandler{
  return handler;
}

