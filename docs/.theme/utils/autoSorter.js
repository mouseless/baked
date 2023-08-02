export default function (a, b, by = "title", order = "asc") {
  const result = order === "desc"
    ? desc(a, b, by)
    : asc(a, b, by);

  return result;
}
function asc(a, b, by) {
  if(a[by] < b[by]) { return -1; }
  if(a[by] > b[by]) { return 1; }
  return 0;
}

function desc(a, b, by) {
  if(b[by] < a[by]) { return -1; }
  if(b[by] > a[by]) { return 1; }
  return 0;
}
