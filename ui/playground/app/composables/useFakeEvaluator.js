export default function({ expected } = {}) {
  function evaluate(data) {
    return data === expected;
  }

  return {
    evaluate
  };
}
