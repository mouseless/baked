export default function() {
  function format(data, { row, prop }) {
    if(typeof data === "string") { return data; }

    if(prop === "column5") {
      return `${truncateDouble(data, row.formatDigits.value)}`;
    }

    return `${data}`.replace(".", ",");
  }

  function truncateDouble(num, digits) {
    const factor = Math.pow(10, digits);
    return Math.floor(num * factor) / factor;
  }
  return {
    format
  };
}