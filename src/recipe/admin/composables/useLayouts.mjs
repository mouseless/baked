export default function() {
  return {
    async fetch(name) {
      return await import(`~/.baked/${name}.layout.json`)
        .catch(_ => {
          throw createError({
            statusCode: 404,
            statusMessage: `'${name}' Layout Not Found`,
            fatal: true
          });
        });
    }
  };
}
