export default function() {
  function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  async function compute({ ms, data } = {}) {
    await delay(ms);

    return data;
  }

  return {
    compute
  };
}
