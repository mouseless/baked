export default function (index, pages) {
  const sortedPagesMap = new Map();
  for(let i = 0; i < index.pages.length; i++) {
    sortedPagesMap.set(pages[i]._path, pages[i]);
  }

  const sortedPages = pages;
  for(let i = 0; i < index.pages.length; i++) {
    sortedPages[i] = sortedPagesMap.get(`${index._path}/${index.pages[i]}`);
  }

  return sortedPages;
}
