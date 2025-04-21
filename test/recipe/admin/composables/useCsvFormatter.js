export default function() {

  function format(data) {
    return `${data}`.replace(".",",");
  }

  return {
    format
  };
}