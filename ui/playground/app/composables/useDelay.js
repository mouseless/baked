export default function() {
  async function run({ time } = {}) {
    const promise = new Promise(resolve => setTimeout(resolve, time));

    await promise;
  }

  return {
    run
  };
}