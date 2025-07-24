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

  return {
    getOrCreate
  };
}
