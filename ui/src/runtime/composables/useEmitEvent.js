export default function() {
  async function executeAsync(eventName, events) {
    events.emit(eventName);
  }

  return {
    executeAsync
  };
}