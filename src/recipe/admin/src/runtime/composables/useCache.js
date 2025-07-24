export default function(name) {
  async function getOrCreate(key, create) {
    key = `${name}[${key}]`;

    const cached = localStorage.getItem(key);
    if(cached) {
      return JSON.parse(cached);
    }

    const result = await create();
    localStorage.setItem(key, JSON.stringify(result));

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
    getOrCreate,
    clear
  };
}
