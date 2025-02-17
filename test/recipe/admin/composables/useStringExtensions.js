export default function(){
  function format(b) {
    const a = arguments;
    return b.replace(/(\{\{\d\}\}|\{\d\})/g, function(b) {
      if(b.substring(0, 2) == "{{") return b;
      const c = parseInt(b.match(/\d/)[0]);
      return a[c + 1];
    });
  };

  return {
    format
  };
}