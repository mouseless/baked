// value or default function, named $ for quick access
function $(value, defaultValue) {
  return value === undefined ? defaultValue : value;
}

export default {
  aDetailPage({ title, header, props, data }) {
    title = $(title, "Test Title");
    header = $(header, this.anExpected({testId: "header", value: "Test Header"}));
    props = $(props, []);
    data = $(data, { });

    return {
      type: "DetailPage",
      schema: { title, header, props },
      data
    };
  },

  aDetailPageProp({ keyAndTestId, title, component }) {
    keyAndTestId = $(keyAndTestId, "testKey");
    title = $(title, "Test Prop");
    component = $(component, this.anExpected({ testId: keyAndTestId }));

    return { key: keyAndTestId, title, component };
  },

  aSideMenu({ logo, menu, data }) {
    logo = $(logo, "logo.svg");
    menu = $(menu, []);
    data = $(data, { path: "/test" });

    return {
      type: "SideMenu",
      schema: { logo, menu },
      data
    };
  },

  aSideMenuItem({ route, icon, title, soon }) {
    route = $(route, "/item");
    icon = $(icon, "pi pi-home");
    soon = $(soon, false);

    return {
      route,
      icon,
      title,
      soon
    };
  },

  aHeader({ sitemapItems, data }) {
    sitemapItems = $(sitemapItems, [this.aHeaderSitemapItem({ route: "/test" })]);
    data = $(data, { path: "/test" });

    return {
      type: "Header",
      schema: {
        sitemap: sitemapItems
          .reduce((result, item) => ({
            ...result,
            [item.route]: item
          }), {})
      },
      data
    };
  },

  aHeaderSitemapItem({ route, icon, title, parentRoute }) {
    route = $(route, "/item");
    icon = $(icon, "pi pi-home");

    return {
      route,
      icon,
      title,
      parentRoute
    };
  },

  anExpected({ testId, value }) {
    testId = $(testId, "test-id");
    value = $(value, "test value");

    return {
      type: "Expected",
      schema: testId,
      data: value
    };
  }
};
