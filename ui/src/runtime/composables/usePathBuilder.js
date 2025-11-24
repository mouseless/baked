export default function() {
  function build(path, params) {
    Object.entries(params).forEach(([key, value]) => {
      // AI-GEN
      // match either {key} or {anything:key}
      const regex = new RegExp(`\\{(?:[\\w-]+:)?${key}\\}`, "g");
      path = path.replace(regex, value);
    });
    return path;
  }

  return {
    build
  };
}