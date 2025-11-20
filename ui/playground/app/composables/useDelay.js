export default function() {
  async function executeAsync({ time }) {
    const promise = new Promise(resolve => setTimeout(resolve, time));

    await promise;
  }

  return {
    executeAsync
  };
}