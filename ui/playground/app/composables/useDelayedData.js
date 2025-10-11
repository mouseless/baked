export default function() {
  function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  async function computeAsync(ms, data) {
    await delay(ms);

    return data;
  }

  return {
    computeAsync
  };
}
