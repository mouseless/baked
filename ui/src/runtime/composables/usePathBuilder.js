export default function() {
  function build(path, params, { forRoute } = {}) {
    Object.entries(params).forEach(([key, value]) => {
      // AI-GEN
      // match key ex: either {id} or {id:guid}
      // or
      // match key ex: [id]
      const regex = forRoute ? new RegExp(`\\[${key}(?::[^\\]]*)?\\]`, "g") : new RegExp(`\\{${key}(?::[^}]*)?\\}`, "g");
      path = path.replace(regex, value);
    });

    return path;
  }

  return {
    build
  };
}
