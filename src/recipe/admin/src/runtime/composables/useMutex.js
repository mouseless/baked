import { E_CANCELED } from "async-mutex";

export default function() {
  const mutex = useNuxtApp().$mutex;

  async function run(callback) {
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
