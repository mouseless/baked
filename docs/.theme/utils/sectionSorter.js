export default function (a, b, sections) {
  const splitedA = a._path.split("/");
  const splitedB = b._path.split("/");

  return sections?.indexOf(splitedA[splitedA.length - 1]) - sections?.indexOf(splitedB[splitedB.length - 1]);
}
