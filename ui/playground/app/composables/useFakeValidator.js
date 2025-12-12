export default function({ expected } = {}) {
  function validate(data) {
    return data === expected;
  }

  return {
    validate
  };
}
