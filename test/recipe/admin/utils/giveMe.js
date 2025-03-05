// value or default function, named $ for quick access
function $(value, defaultValue) {
  return value === undefined ? defaultValue : value;
}

export default {
  aCardLink({ route, icon, title, description, disabled, disabledReason } = {}) {
    route = $(route, "/test-route");
    icon = $(icon, "pi pi-heart");
    title = $(title, "Test");
    description = $(description, "Test description is given for testing purposes");
    disabled = $(disabled, false);
    disabledReason = $(disabledReason, disabled ? "REASON" : "");

    return {
      type: "CardLink",
      schema: { route, icon, title, description, disabled, disabledReason }
    };
  },

  aDetailPage({ header, props, data } = {}) {
    header = $(header, this.anExpected());
    props = $(props, []);
    data = $(data, { });

    return {
      type: "DetailPage",
      schema: { header, props },
      data
    };
  },

  aDetailPageProp({ keyAndTestId, title, component } = {}) {
    keyAndTestId = $(keyAndTestId, "testKey");
    title = $(title, "Test Prop");
    component = $(component, this.anExpected({ testId: keyAndTestId }));

    return { key: keyAndTestId, title, component };
  },

  aHeader({ sitemapItems, data } = {}) {
    sitemapItems = $(sitemapItems, [this.aHeaderItem({ route: "/test" })]);
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

  aHeaderItem({ route, icon, title, parentRoute } = {}) {
    route = $(route, "/item");
    icon = $(icon, route === "/" ? "pi pi-home" : "pi pi-heart");

    return { route, icon, title, parentRoute };
  },

  aMenuPage({ title, description, links } = {}) {
    title = $(title, "Test Title");
    description = $(description, "Test description is given for testing purposes");
    links = $(links, []);

    return {
      type: "MenuPage",
      schema: { title, description, links }
    };
  },

  aPageTitle({ title, description, actions } = {}) {
    title = $(title, "Test Title");
    description = $(description, "Test description is given for testing purposes");
    actions = $(actions, []);

    return {
      type: "PageTitle",
      schema: { title, description, actions }
    };
  },

  aSideMenu({ logo, menu, data, footer } = {}) {
    logo = $(logo, "logo.svg");
    menu = $(menu, []);
    data = $(data, { path: "/test" });
    footer = $(footer, this.anExpected());

    return {
      type: "SideMenu",
      schema: { logo, menu, footer },
      data
    };
  },

  aSideMenuItem({ route, icon, title, soon } = {}) {
    route = $(route, "/item");
    icon = $(icon, "pi pi-home");
    soon = $(soon, false);

    return { route, icon, title, soon };
  },

  anExpected({ testId, value } = {}) {
    testId = $(testId, "test-id");
    value = $(value, "");

    return {
      type: "Expected",
      schema: testId,
      data: value
    };
  }
};
