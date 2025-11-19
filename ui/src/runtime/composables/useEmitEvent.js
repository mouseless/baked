import { useNuxtApp } from "#app";

export default function() {
  const nuxtApp = useNuxtApp();
  const events = nuxtApp.$events;

  async function executeAsync(eventNames) {
    for(const event of eventNames) {
      await events.emit(event);
    }
  }

  return {
    executeAsync
  };
}