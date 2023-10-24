export default function (index, menus) {
  const sortedSectionsMap = new Map();
  for(let i = 0; i < index.sections.length; i++) {
    sortedSectionsMap.set(menus[i]._path, menus[i]);
  }

  const sectionSorted = menus;
  for(let i = 0; i < index.sections.length; i++) {
    sectionSorted[i] = sortedSectionsMap.get(`/${index.sections[i]}/`);
  }

  return sectionSorted;
}
