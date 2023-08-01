export default function (index, pages) {
  const map = new Map();
  for(let i = 0; i < index.pages.length; i++) {
    map.set(pages[i]._path, pages[i]);
  }

  const sortedPages = pages;
  for(let i = 0; i < index.pages.length; i++) {
    sortedPages[i] = map.get(`${index._path}/${index.pages[i]}`);
  }

  return sortedPages;
}