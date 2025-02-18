export default function(){
  function format(formatString, args) {
    return formatString.replace(/(\{\{\d\}\}|\{\d\})/g, part => {
      if(part.substring(0, 2) === "{{") { return part; } // escape

      const index = parseInt(part.match(/\d/)[0]);

      return args[index];
    });
  };

  return {
    format
  };
}