export default function (collection, pathAtIndex) {
  const map = new Map();
  for(let i = 0; i < collection.length; i++) {
    map.set(collection[i]._path, collection[i]);
  }

  const result = [];
  for(let i = 0; i < collection.length; i++) {
    result.push(map.get(pathAtIndex(i)));
  }

  return result;
}
