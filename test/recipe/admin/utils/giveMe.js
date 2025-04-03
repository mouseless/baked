//expires at 2999-03-28
const accessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjMyNDc5NjE0MTk0fQ.F4K4GkNqtuUNy6cgyOEtrLtaidgvVQmsw1Ouixyw5a0";
//expires at 2000-03-28
const expiredAccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjk1NDI0MTM5NH0.ZKPMybdzg1aO1g_xyV1QXUx9NR_vynu9s9z4Zll7WNA";
//expires at 9999-03-28
const refreshToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjI1MzM3ODIzNDk5NH0.IO-jutz7t-FbvgrQ87n0y_tSWUsSfiNPpfr3sAzvWhg";

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

  aDataPanel({ title, collapsed, parameters, content } = {}) {
    title = $(title, { type: "Inline", value: "Test Title" });
    collapsed = $(collapsed, false);
    parameters = $(parameters, []);
    content = $(content, this.anExpected());

    return {
      type: "DataPanel",
      schema: { title, collapsed, parameters, content }
    };
  },

  aDataTable({ columns, dataKey, paginator, rows, rowsWhenLoading, data } = {}) {
    columns = $(columns, [
      this.aDataTableColumn({ prop: "test" })
    ]);
    dataKey = $(dataKey, null);
    paginator = $(paginator, false);
    rows = $(rows, null);
    rowsWhenLoading = $(rowsWhenLoading, null);
    data = $(data, [
      { test: "value 1" },
      { test: "value 2" },
      { test: "value 3" }
    ]);

    return {
      type: "DataTable",
      schema: { columns, dataKey, paginator, rows, rowsWhenLoading },
      data: { type: "Inline", value: data }
    };
  },

  aDataTableColumn({ title, prop, minWidth, component } = {}) {
    title = $(title, "Test");
    prop = $(prop, "test");
    minWidth = $(minWidth, false);
    component = $(component, this.anExpected());

    return {
      title,
      prop,
      minWidth,
      component
    };
  },

  anErrorPage({ errorInfos, footerInfo, safeLinks, safeLinksMessage, data } = {}){
    errorInfos = $(errorInfos, [this.anErrorPageInfo()]);
    footerInfo = $(footerInfo, "Test footer info");
    safeLinks = $(safeLinks, [this.anExpected()]);
    safeLinksMessage = $(safeLinksMessage, "Test links message");
    data = $(data, new Error("Test Error", { status: 500 }));

    errorInfos = errorInfos.reduce((result, ei) => ({
      ...result,
      [ei.statusCode]: { title: ei.title, message: ei.message}
    }), {});

    return {
      type: "ErrorPage",
      schema: { errorInfos, footerInfo, safeLinks, safeLinksMessage},
      data: { type: "Inline", value: data }
    };
  },

  anErrorPageInfo({ statusCode, title, message } = {}){
    statusCode = $(statusCode, "500");
    title = $(title, "Test Title");
    message = $(message, "Test message");

    return {
      statusCode,
      title,
      message
    };
  },

  anExpected({ testId, value, data } = {}) {
    testId = $(testId, "test-id");
    value = $(value, "");
    data = $(data, { type: "Inline", value });

    return {
      type: "Expected",
      schema: testId,
      data
    };
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
      data: { type: "Inline", value: data }
    };
  },

  aHeaderItem({ route, icon, title, parentRoute } = {}) {
    route = $(route, "/item");
    icon = $(icon, route === "/" ? "pi pi-home" : "pi pi-heart");

    return { route, icon, title, parentRoute };
  },

  anIcon({ iconClass } = {}) {
    iconClass = $(iconClass, "pi-heart");

    return {
      type: "Icon",
      schema: { iconClass }
    };
  },

  theInjectedData() {
    return {
      type: "Injected"
    };
  },

  anInput({ testId } = {}) {
    testId = $(testId, "test-input");

    return {
      type: "Input",
      schema: testId
    };
  },

  aNavLink({ path, idProp, textProp, data } = {}) {
    path = $(path, "/some-object/{0}");
    idProp = $(idProp, "id");
    textProp = $(textProp, "name");
    data = $(data, { id: "test-id", name: "Test" });

    return {
      type: "NavLink",
      schema: { path, idProp, textProp },
      data: { type: "Inline", value: data }
    };
  },

  aMoney({ data } = {}) {
    data = $(data, 100_000);

    return {
      type: "Money",
      data: { type: "Inline", value: data }
    };
  },

  aMenuPage({ header, links } = {}) {
    header = $(header, this.anExpected());
    links = $(links, []);

    return {
      type: "MenuPage",
      schema: { header, links }
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

  aParameter({ name, component, required, defaultValue } = {}) {
    name = $(name, "test");
    required = $(required, false);
    component = $(component, this.anInput());

    return { name, required, default: defaultValue, component };
  },

  theQueryData() {
    return {
      type: "Computed",
      composable: "useQuery"
    };
  },

  aRate({ data } = {}) {
    data = $(data, 0.5);

    return {
      type: "Rate",
      data: { type: "Inline", value: data }
    };
  },

  aReportPage({ title, description, queryParameters, tabs } = {}) {
    title = this.aPageTitle({ title, description }).schema;
    queryParameters = $(queryParameters, []);
    tabs = $(tabs, [this.aReportPageTab()]);

    return {
      type: "ReportPage",
      schema: { title, queryParameters, tabs }
    };
  },

  aReportPageTab({ id, title, icon, contents } = {}) {
    id = $(id, "test-tab");
    title = $(title, "Test Tab");
    icon = $(icon, this.anIcon());
    contents = $(contents, [this.aReportPageTabContent()]);

    return { id, title, icon, contents };
  },

  aReportPageTabContent({ component, fullScreen, narrow } = {}) {
    component = $(component, this.anExpected({ value: "Test content is given for testing purposes" }));
    fullScreen = $(fullScreen, false);
    narrow = $(narrow, false);

    return { component, fullScreen, narrow };
  },

  aSelect({ label, optionLabel, optionValue, showClear, stateful, data } = {}) {
    label = $(label, "Test");
    showClear = $(showClear, false);
    stateful = $(stateful, false);
    data = $(data, ["Test Option 1", "Test Option 2"]);

    return {
      type: "Select",
      schema: { label, optionLabel, optionValue, showClear, stateful },
      data: { type: "Inline", value: data }
    };
  },

  aSelectButton({ allowEmpty, optionLabel, optionValue, stateful, data } = {}) {
    data = $(data, ["Test Option 1", "Test Option 2"]);
    allowEmpty = $(allowEmpty, false);
    stateful = $(stateful, false);

    return {
      type: "SelectButton",
      schema: { allowEmpty, optionLabel, optionValue, stateful },
      data: { type: "Inline", value: data }
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
      data: { type: "Inline", value: data }
    };
  },

  aSideMenuItem({ route, icon, title, disabled } = {}) {
    route = $(route, "/item");
    icon = $(icon, "pi pi-home");
    disabled = $(disabled, false);

    return { route, icon, title, disabled };
  },

  aToken({ accessExpired } = {}) {
    accessExpired = $(accessExpired, false);

    return {
      access: accessExpired ? expiredAccessToken : accessToken,
      refresh: refreshToken
    };
  }
};
