export default function(name) {
  async function getOrCreate(key, factory) {
    key = `${name}[${key}]`;

    const cached = localStorage.getItem(key);
    if(cached) {
      return JSON.parse(cached);
    }

    const response = await factory();
    localStorage.setItem(key, JSON.stringify(response));

    return response;
  }

  return {
    getOrCreate
  };
}
