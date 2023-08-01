export default function (index, menus) {
  const map = new Map();
  for(let i = 0; i < index.sections.length; i++) {
    map.set(menus[i]._path, menus[i]);
  }

  const sectionSorted = menus;
  for(let i = 0; i < index.sections.length; i++) {
    sectionSorted[i] = map.get(`/${index.sections[i]}`);
  }

  return sectionSorted;
}