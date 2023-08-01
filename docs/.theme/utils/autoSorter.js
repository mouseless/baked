export default function (a, b, array) {
  const result = array.sort?.order === "desc"
    ? desc(a, b, array.sort?.by)
    : asc(a, b, array.sort?.by);

  return result;
}
function asc(a, b, by = "title") {
  if(a[by] < b[by]) { return -1; }
  if(a[by] > b[by]) { return 1; }
  return 0;
}

function desc(a, b, by = "title") {
  if(b[by] < a[by]) { return -1; }
  if(b[by] > a[by]) { return 1; }
  return 0;
}