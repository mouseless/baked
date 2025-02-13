import { defineContentConfig, defineCollection, z } from "@nuxt/content";

export default defineContentConfig({
  collections: {
    content: defineCollection({
      type: "page",
      source: "**/*.md"
    }),
    notFound: defineCollection({
      type: "page",
      source: "**/not-found.md"
    }),
    sections: defineCollection({
      type: "data",
      source: {
        include: "**/**.md"
      },
      schema: z.object({
        path: z.string(),
        title: z.string(),
        pages: z.array(z.string()),
        position: z.number(),
        sort: z.string()
      })
    }),
    menus: defineCollection({
      type: "data",
      source: {
        include: "**/**/index.md"
      },
      schema: z.object({
        path: z.string(),
        title: z.string(),
        position: z.number(),
        sections: z.array(z.string())
      })
    }),
    menuOrder: defineCollection({
      type: "data",
      source: {
        include: "index.md"
      },
      schema: z.object({
        sections: z.array(z.string())
      })
    })
  }
});
