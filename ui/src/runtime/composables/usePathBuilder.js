export default function() {
  function build(path, params) {
    Object.entries(params).forEach(([key, value]) => {
      // AI-GEN
      // match key ex: either {id} or {id:guid}
      const regex = new RegExp(`\\{${key}(?::[^}]*)?\\}`, "g");
      path = path.replace(regex, value);
    });
    return path;
  }

  return {
    build
  };
}