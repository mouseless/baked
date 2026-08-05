export default function() {
  function build(path, params, options) {
    return buildDetailed(path, params, options).path;
  }

  function buildDetailed(path, params, { forRoute } = {}) {
    const usedKeys = [];

    Object.entries(params).forEach(([key, value]) => {
      // AI-GEN
      // match key ex: either {id} or {id:guid}
      // or
      // match key ex: [id]
      const regex = forRoute
        ? new RegExp(`\\[${key}(?::[^\\]]*)?\\]`, "g")
        : new RegExp(`\\{${key}(?::[^}]*)?\\}`, "g");

      const replaced = path.replace(regex, value);
      if(replaced !== path) {
        usedKeys.push(key);
      }

      path = replaced;
    });

    return {
      path,
      usedKeys
    };
  }

  return {
    build,
    buildDetailed
  };
}