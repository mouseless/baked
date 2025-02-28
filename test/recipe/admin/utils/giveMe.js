// value or default function, named $ for quick access
function $(value, defaultValue) {
  return value === undefined ? defaultValue : value;
}

export default {
  aDetail({ title, header, props, data }) {
    title = $(title, "Test Title");
    header = $(header, this.anExpected({testId: "header", value: "Test Header"}));
    props = $(props, []);
    data = $(data, { });

    return {
      type: "Detail",
      schema: { title, header, props },
      data
    };
  },

  aDetailProp({ keyAndTestId, title, component }) {
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
