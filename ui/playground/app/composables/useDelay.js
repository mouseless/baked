export default function() {
  async function execute(time) {
    const promise = new Promise(resolve => setTimeout(resolve, time));

    await promise;
  }

  return {
    execute
  };
}