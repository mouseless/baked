export default function() {
  return {
    async fetch(name) {
      return await import(`~/.baked/${name}.page.json`)
        .catch(_ => {
          throw createError({
            statusCode: 404,
            statusMessage: `'${name}' Page Not Found`,
            fatal: true
          });
        });
    }
  };
}
