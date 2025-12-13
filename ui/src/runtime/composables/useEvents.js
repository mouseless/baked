export default function() {
  const listeners = {};

  function create() {
    function on(name, id, callback) {
      listeners[name] ||= {};

      listeners[name][id] = callback;
    }

    function off(name, id) {
      delete listeners[name][id];
    }

    async function emit(name, value) {
      if(!listeners[name]) { return; }

      for(const id in listeners[name]) {
        listeners[name][id](value);
      }
    }

    return {
      on,
      off,
      emit
    };
  }

  return {
    create
  };
}
