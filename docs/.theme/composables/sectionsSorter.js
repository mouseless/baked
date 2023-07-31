export default function (a, b, array) {
  const splitedA = a._path.split("/");
  const splitedB = b._path.split("/");

  return array.indexOf(splitedA[splitedA.length - 1]) - array.indexOf(splitedB[splitedB.length - 1]);
}
