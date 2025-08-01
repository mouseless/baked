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
    title = $(title, "Spec: Test");
    description = $(description, "Spec: Test description is given for testing purposes");
    disabled = $(disabled, false);
    disabledReason = $(disabledReason, disabled ? "REASON" : "");

    return {
      type: "CardLink",
      schema: { route, icon, title, description, disabled, disabledReason }
    };
  },

  aConditional({ testId, fallback, conditions } = {}) {
    testId = $(testId, "test");
    fallback = $(fallback, this.anExpected(testId));
    conditions = $(conditions, []);

    return { fallback, conditions };
  },

  aConditionalCondition({ prop, value, testId } = {}) {
    prop = $(prop, "testProp");
    value = $(value, "test-value");
    const component = this.anExpected({ testId });

    return { prop, value, component };
  },

  aContainer({ content, contents, data } = {}) {
    content = $(content, this.anExpected());
    contents = $(contents, [content]);
    data = $(data, { type: "Inline", value: "Test value" });

    return {
      type: "Container",
      schema: { contents },
      data
    };
  },

  aDataPanel({ title, collapsed, localizeTitle, parameters, content } = {}) {
    title = $(title, { type: "Inline", value: "Spec: Test Title" });
    collapsed = $(collapsed, false);
    parameters = $(parameters, []);
    content = $(content, this.anExpected());
    localizeTitle = $(localizeTitle, title.type === "Inline");

    return {
      type: "DataPanel",
      schema: { title, collapsed, localizeTitle, parameters, content }
    };
  },

  aDataTable({ columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, virtualScrollerOptions, data } = {}) {
    columns = $(columns, [
      this.aDataTableColumn({ prop: "test" })
    ]);
    dataKey = $(dataKey, null);
    itemsProp = $(itemsProp, footerTemplate ? "items" : undefined);
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
      schema: { columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, virtualScrollerOptions },
      data: { type: "Inline", value: data }
    };
  },

  aDataTableColumn({ title, prop, alignRight, minWidth, component, exportable, frozen } = {}) {
    title = $(title, "Spec: Test");
    prop = $(prop, "test");
    alignRight = $(alignRight, false);
    minWidth = $(minWidth, false);
    component = $(component, this.aConditional());
    exportable = $(exportable, false);

    return {
      title,
      prop,
      alignRight,
      minWidth,
      component,
      exportable,
      frozen
    };
  },

  aDataTableExport({ csvSeparator, fileName, formatter, buttonIcon, buttonLabel }) {
    csvSeparator = $(csvSeparator, ";");
    fileName = $(fileName, `${Date.now()}`);
    buttonIcon = $(buttonIcon, "pi pi-external-link");
    buttonLabel = $(buttonLabel, "");

    return {
      csvSeparator,
      fileName,
      formatter,
      buttonIcon,
      buttonLabel
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

  aFilter({ placeholder, pageContextKey } = {}) {
    placeholder = $(placeholder, "Filter");
    pageContextKey = $(pageContextKey, "filter");

    return {
      type: "Filter",
      schema: {
        placeholder,
        pageContextKey
      }
    };
  },

  aFilterable({ title, component } = {}) {
    title = $(title, "filter title");
    component = $(component, this.anExpected());

    return {
      title,
      component
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

  aLanguageSwitcher() {
    return {
      type: "LanguageSwitcher",
      schema: {}
    };
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
      type: "Injected",
      key: "Custom"
    };
  },

  anInput({ testId, defaultValue } = {}) {
    testId = $(testId, "test-input");
    defaultValue = $(defaultValue, null);

    return {
      type: "Input",
      schema: {
        testId,
        defaultValue
      }
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

  aMessage({ message, icon, severity, localizeMessage, data } = {}) {
    message = $(message, "Spec: This is a message");
    localizeMessage = $(localizeMessage, true);
    data = $(data, { type: "Inline", value: message });

    return {
      type: "Message",
      schema: {
        icon,
        severity,
        localizeMessage
      },
      data
    };
  },

  aMoney({ data } = {}) {
    data = $(data, 100_000);

    return {
      type: "Money",
      data: { type: "Inline", value: data }
    };
  },

  aMenuPage({ header, sections, filterPageContextKey } = {}) {
    header = $(header, this.anExpected());
    sections = $(sections, this.aMenuPageSection());

    return {
      type: "MenuPage",
      schema: { header, sections, filterPageContextKey }
    };
  },

  aMenuPageSection({ title, links } = {}) {
    title = $(title, null);
    links = $(links, [this.aFilterable()]);

    return {
      title,
      links
    };
  },

  aNumber({ data } = {}) {
    data = $(data, 100_000);

    return {
      type: "Number",
      data: { type: "Inline", value: data }
    };
  },

  aPageTitle({ title, description, actions } = {}) {
    title = $(title, "Spec: Test Title");
    description = $(description, "Spec: Test description is given for testing purposes");
    actions = $(actions, []);

    return {
      type: "PageTitle",
      schema: { title, description, actions }
    };
  },

  aParameter({ name, component, required, defaultValue, default_, defaultSelfManaged } = {}) {
    name = $(name, "test");
    required = $(required, false);
    component = $(component, this.anInput());
    default_ = $(default_, defaultValue ? { type: "Inline", value: defaultValue } : undefined);
    defaultSelfManaged = $(defaultSelfManaged, false);

    return { name, required, default: default_, defaultSelfManaged, component };
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

  aReportPageTab({ id, title, contents, fullScreen, icon, overflow } = {}) {
    id = $(id, "test-tab");
    title = $(title, "Test Tab");
    contents = $(contents, [this.aReportPageTabContent()]);
    fullScreen = $(fullScreen, false);
    icon = $(icon, this.anIcon());
    overflow = $(overflow, false);

    return { id, title, contents, fullScreen, icon, overflow };
  },

  aReportPageTabContent({ component, narrow } = {}) {
    component = $(component, this.anExpected({ value: "Test content is given for testing purposes" }));
    narrow = $(narrow, false);

    return { component, narrow };
  },

  aSelect({ label, localizeLabel, optionLabel, optionValue, showClear, stateful, data, inline } = {}) {
    label = $(label, "Spec: Test");
    localizeLabel = $(localizeLabel, false);
    showClear = $(showClear, false);
    stateful = $(stateful, false);
    data = $(data, ["Test Option 1", "Test Option 2"]);
    inline = $(inline, true);

    data = inline
      ? { type: "Inline", value: data }
      : { type: "Computed", composable: "useDelayedData", args: [1, data] };

    return {
      type: "Select",
      schema: { label, localizeLabel, optionLabel, optionValue, showClear, stateful },
      data
    };
  },

  aSelectButton({ allowEmpty, localizeLabel, optionLabel, optionValue, stateful, data, inline } = {}) {
    data = $(data, ["Test Option 1", "Test Option 2"]);
    inline = $(inline, true);
    allowEmpty = $(allowEmpty, false);
    stateful = $(stateful, false);
    localizeLabel = $(localizeLabel, false);
    data = inline
      ? { type: "Inline", value: data }
      : { type: "Computed", composable: "useDelayedData", args: [1, data] };

    return {
      type: "SelectButton",
      schema: { allowEmpty, localizeLabel, optionLabel, optionValue, stateful },
      data
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

  aString({ value, data, maxLength } = {}) {
    value = $(value, "Test string");
    data = $(data, { type: "Inline", value });

    return {
      type: "String",
      schema: { maxLength },
      data
    };
  },

  aToken({ accessExpired } = {}) {
    accessExpired = $(accessExpired, false);

    return {
      access: accessExpired ? expiredAccessToken : accessToken,
      refresh: refreshToken
    };
  },

  anApiResponse() {
    return { sample: "response" };
  }
};
