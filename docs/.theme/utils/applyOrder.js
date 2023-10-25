export default function(collection, pathAtIndex) {
  const map = new Map();
  for(let i = 0; i < collection.length; i++) {
    map.set(collection[i]._path, collection[i]);
  }

  for(let i = 0; i < collection.length; i++) {
    collection[i] = map.get(pathAtIndex(i));
  }
}
