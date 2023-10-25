export default function (reference, referenceKeySelector, collection, collectionKeySelector) {
  const map = new Map();
  for(let i = 0; i < reference.length; i++) {
    map.set(collectionKeySelector(i), collection[i]);
  }

  const sorted = collection;
  for(let i = 0; i < reference.length; i++) {
    sorted[i] = map.get(referenceKeySelector(i));
  }

  return sorted;
}
