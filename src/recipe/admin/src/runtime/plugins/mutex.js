import { defineNuxtPlugin } from "#app";
import { Mutex } from "async-mutex";

export default defineNuxtPlugin({
  name: "mutex",
  setup() {
    const mutex = new Mutex();

    return {
      provide: {
        mutex
      }
    };
  }
});
