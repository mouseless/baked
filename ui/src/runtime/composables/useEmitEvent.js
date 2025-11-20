export default function() {
  async function executeAsync(eventNames, events) {
    for(const event of eventNames) {
      events.emit(event);
    }
  }

  return {
    executeAsync
  };
}