import { E_CANCELED } from "async-mutex";

export default function() {
  async function run(callback) {
    const mutex = useNuxtApp().$mutex;

    try {
      await mutex.acquire();
      await callback();
    } catch (e) {
      if(e !== E_CANCELED) {
        mutex.cancel();
      }

      throw e;
    } finally {
      if(mutex.isLocked()) {
        mutex.release();
      }
    }
  }

  return {
    run
  };
}
