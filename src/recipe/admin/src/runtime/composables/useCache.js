export default function(name, { expirationInMinutes = 60 } = {}) {
  function buildKey({ path, query }) {
    let result = path;
    if(query) {
      const search = new URLSearchParams(query);
      search.sort();
      result += `?${search}`;
    }

    return result;
  }

  function isCacheValid(entry) {
    return Date.now() - entry.createdAt < expirationInMinutes * 60 * 1000;
  }

  async function getOrCreate({ key, create }) {
    if(typeof create !== "function") {
      throw new Error("create must be a function");
    }

    key = `${name}[${key}]`;

    const cached = localStorage.getItem(key);
    if(cached) {
      const entry = JSON.parse(cached);
      if(isCacheValid(entry)) {
        return entry.data;
      }
    }

    const result = await create();
    localStorage.setItem(key, JSON.stringify({
      createdAt: Date.now(),
      data: result
    }));

    return result;
  }

  function clear() {
    const keysToClear = [];
    for(let i = 0; i<localStorage.length; i++) {
      const key = localStorage.key(i);
      if(key.startsWith(name)) {
        keysToClear.push(key);
      }
    }

    for(const key of keysToClear) {
      localStorage.removeItem(key);
    }
  }

  return {
    buildKey,
    getOrCreate,
    clear
  };
}
