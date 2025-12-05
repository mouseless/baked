export default function() {
  const listeners = {};

  function on(name, id, callback) {
    listeners[name] ||= {};

    listeners[name][id] = callback;
  }

  function off(name, id) {

    delete listeners[name][id];
  }

  async function emit(name) {
    if(!listeners[name]) { return; }

    for(const id in listeners[name]) {
      listeners[name][id]();
    }
  }

  return {
    on,
    off,
    emit
  };
}