import { useFormat } from "#imports";

export default function() {
  const { asNumber } = useFormat();

  function format(data, { row, prop }) {
    if(typeof data === "string") { return data; }

    if(prop === "column5") {
      return `${asNumber(data, { formatOptions: { maximumFractionDigits: row.formatDigits } })}`;
    }

    return `${data}`.replace(".", ",");
  }

  return {
    format
  };
}