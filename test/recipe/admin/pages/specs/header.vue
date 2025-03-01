<template>
  <ComponentSpec :variants="variants" />
</template>
<script setup>
const variants = [
  {
    name: "Multi Level",
    descriptor: giveMe.aHeader({
      sitemapItems: [
        giveMe.aHeaderSitemapItem({ route: "/" }),
        giveMe.aHeaderSitemapItem({
          route: "/root",
          icon: "pi pi-heart",
          title: "Root Page"
        }),
        giveMe.aHeaderSitemapItem({
          route: "/root/mid",
          icon: "pi pi-wave-pulse",
          title: "Mid Page",
          parentRoute: "/root"
        }),
        giveMe.aHeaderSitemapItem({
          route: "/root/mid/leaf",
          icon: "pi pi-sun",
          title: "Leaf Page",
          parentRoute: "/root/mid"
        })
      ],
      data: { path: "/root/mid/leaf" }
    })
  },
  {
    name: "Hidden at Home",
    descriptor: giveMe.aHeader({
      sitemapItems: [ giveMe.aHeaderSitemapItem({ route: "/" }) ],
      data: { path: "/" }
    })
  },
  {
    name: "Hidden at Unknown",
    descriptor: giveMe.aHeader({
      sitemapItems: [
        giveMe.aHeaderSitemapItem({ route: "/" }),
        giveMe.aHeaderSitemapItem({
          route: "/known",
          icon: "pi pi-heart",
          title: "Known Page"
        })
      ],
      data: { path: "/unknown" }
    })
  },
  {
    name: "No Title",
    descriptor: giveMe.aHeader({
      sitemapItems: [
        giveMe.aHeaderSitemapItem({ route: "/" }),
        giveMe.aHeaderSitemapItem({
          route: "/no-title",
          icon: "pi pi-wave-pulse",
          title: null
        })
      ],
      data: { path: "/no-title" }
    })
  },
  {
    name: "No Icon",
    descriptor: giveMe.aHeader({
      sitemapItems: [
        giveMe.aHeaderSitemapItem({ route: "/" }),
        giveMe.aHeaderSitemapItem({
          route: "/no-icon",
          icon: null,
          title: "No Icon"
        })
      ],
      data: { path: "/no-icon" }
    })
  }
];
</script>
