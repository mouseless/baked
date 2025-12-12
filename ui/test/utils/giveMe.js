//expires at 2999-03-28
const accessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjMyNDc5NjE0MTk0fQ.F4K4GkNqtuUNy6cgyOEtrLtaidgvVQmsw1Ouixyw5a0";
//expires at 2000-03-28
const expiredAccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjk1NDI0MTM5NH0.ZKPMybdzg1aO1g_xyV1QXUx9NR_vynu9s9z4Zll7WNA";
//expires at 9999-03-28
const refreshToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjI1MzM3ODIzNDk5NH0.IO-jutz7t-FbvgrQ87n0y_tSWUsSfiNPpfr3sAzvWhg";
//expires never
const adminUiToken = "token-admin-ui";

// value or default function, named $ for quick access
function $(value, defaultValue) {
  return value === undefined ? defaultValue : value;
}
// screens breakpoints of tailwind
const screens = [
  { name: "2xs", width: 340, height: 800 },
  { name: "xs", width: 480, height: 800 },
  { name: "sm", width: 640, height: 800 },
  { name: "md", width: 768, height: 800 },
  { name: "lg", width: 1024, height: 800 },
  { name: "xl", width: 1280, height: 800 },
  { name: "2xl", width: 1536, height: 800 },
  { name: "3xl", width: 1920, height: 800 }
];

export default {
  anApiResponse() {
    return { sample: "response" };
  },

  aButton({ action, icon, label } = {}) {
    label = $(label, "Button Title");

    return {
      type: "Button",
      schema: { icon, label },
      action
    };
  },

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

  aCompositeAction(parts) {
    parts = $(parts, []);

    return {
      type: "Composite",
      parts
    };
  },

  aCompositeData(parts) {
    parts = $(parts, [this.anInlineData()]);

    return {
      type: "Composite",
      parts
    };
  },

  aComputedData({ composable, options } = {}) {
    composable = $(composable, "useFakeComputed");
    options = $(options, this.anInlineData({ data: "fake" }));

    return {
      type: "Computed",
      composable,
      options
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

  aConstraint({ is, isNot, composable, options }) {
    if(composable) {
      return {
        type: "Composable",
        composable,
        options
      };
    }

    if(isNot) {
      return {
        type: "IsNot",
        isNot
      };
    }

    is = $(is, "expected");

    return {
      type: "Is",
      is
    };
  },

  aContainer({ content, contents, data } = {}) {
    content = $(content, this.anExpected());
    contents = $(contents, [content]);
    data = $(data, this.anInlineData("Test value"));

    return {
      type: "Container",
      schema: { contents },
      data
    };
  },

  aContextData({ key, prop } = {}) {
    key = $(key, "parent");
    prop = $(prop, "data");

    return {
      type: "Context",
      key,
      prop
    };
  },

  aDataPanel({ title, collapsed, localizeTitle, inputs, content } = {}) {
    title = $(title, this.anInlineData("Spec: Test Title"));
    collapsed = $(collapsed, false);
    inputs = $(inputs, []);
    content = $(content, this.anExpected());
    localizeTitle = $(localizeTitle, title.type === "Inline");

    return {
      type: "DataPanel",
      schema: { title, collapsed, localizeTitle, inputs, content }
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
      data: this.anInlineData(data)
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

  anErrorPage({ errorInfos, footerInfo, safeLinks, safeLinksMessage, data } = {}) {
    errorInfos = $(errorInfos, [this.anErrorPageInfo()]);
    footerInfo = $(footerInfo, "Test footer info");
    safeLinks = $(safeLinks, [this.anExpected()]);
    safeLinksMessage = $(safeLinksMessage, "Test links message");
    data = $(data, new Error("Test Error", { status: 500 }));

    errorInfos = errorInfos.reduce((result, ei) => ({
      ...result,
      [ei.statusCode]: { title: ei.title, message: ei.message }
    }), {});

    return {
      type: "ErrorPage",
      schema: { errorInfos, footerInfo, safeLinks, safeLinksMessage },
      data: this.anInlineData(data)
    };
  },

  anErrorPageInfo({ statusCode, title, message } = {}) {
    statusCode = $(statusCode, "500");
    title = $(title, "Test Title");
    message = $(message, "Test message");

    return {
      statusCode,
      title,
      message
    };
  },

  anEmitAction({ event, pageContextKey }) {
    if(!pageContextKey) {
      event = $(event, "something-happened");
    }

    return {
      type: "Emit",
      event,
      pageContextKey
    };
  },

  anExpected({ testId, showDataParams, value, data, reactions } = {}) {
    testId = $(testId, "test-id");
    showDataParams = $(showDataParams, false);
    value = $(value, "");
    data = $(data, this.anInlineData(value));

    return {
      type: "Expected",
      schema: {
        testId,
        showDataParams
      },
      data,
      reactions
    };
  },

  aFilter({ placeholder } = {}) {
    placeholder = $(placeholder, "Filter");

    return {
      type: "Filter",
      schema: {
        placeholder
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
      data: this.anInlineData(data)
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

  anInlineData(value) {
    value = $(value, null);

    return {
      type: "Inline",
      value
    };
  },

  anInput({ name, component, required, defaultValue, default_, defaultSelfManaged } = {}) {
    name = $(name, "test");
    required = $(required, false);
    component = $(component, this.anInputText());
    default_ = $(default_, defaultValue ? this.anInlineData(defaultValue) : undefined);
    defaultSelfManaged = $(defaultSelfManaged, false);

    return { name, required, default: default_, defaultSelfManaged, component };
  },

  anInputText({ testId, defaultValue } = {}) {
    testId = $(testId, "test-input");
    defaultValue = $(defaultValue, null);

    return {
      type: "InputText",
      schema: {
        testId,
        defaultValue
      }
    };
  },

  anInputNumber({ testId, defaultValue } = {}) {
    testId = $(testId, "test-input");
    defaultValue = $(defaultValue, null);

    return {
      type: "InputNumber",
      schema: {
        testId,
        defaultValue
      }
    };
  },

  aLanguageSwitcher() {
    return {
      type: "LanguageSwitcher",
      schema: {}
    };
  },

  aLocalAction({ composable, options, showMessage, delay } = {}) {
    composable = $(composable, delay ? "useDelay" : "useShowMessage");
    options = $(options,
      this.anInlineData(delay
        ? { time: delay }
        : { message: $(showMessage, "Test") }
      )
    );

    return {
      type: "Local",
      composable,
      options
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
      data: this.anInlineData(data)
    };
  },

  aMessage({ message, icon, severity, localizeMessage, data } = {}) {
    message = $(message, "Spec: This is a message");
    localizeMessage = $(localizeMessage, true);
    data = $(data, this.anInlineData(message));

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
      data: this.anInlineData(data)
    };
  },

  aMenuPage({ header, sections, filterEvent } = {}) {
    header = $(header, this.anExpected());
    sections = $(sections, this.aMenuPageSection());

    return {
      type: "MenuPage",
      schema: { header, sections, filterEvent }
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

  aMissingComponent({ data, component, path, source } = {}) {
    data = $(data, null);
    component = $(component, null);
    path = $(path, []);
    source = $(source, null);

    return {
      type: "MissingComponent",
      schema: {
        component,
        path,
        source
      },
      data: this.anInlineData(data)
    };
  },

  aNumber({ data } = {}) {
    data = $(data, 100_000);

    return {
      type: "Number",
      data: this.anInlineData(data)
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

  theParentContext() {
    return {
      type: "Context",
      key: "parent"
    };
  },

  theQueryData() {
    return this.aComputedData({
      composable: "useNuxtRoute",
      options: this.anInlineData({ property: "query" })
    });
  },

  aRate({ data } = {}) {
    data = $(data, 0.5);

    return {
      type: "Rate",
      data: this.anInlineData(data)
    };
  },

  aRemoteAction({ path, query, params, headers, body, postAction } = {}) {
    path = $(path, "/fake-remote");

    return {
      type: "Remote",
      path,
      query,
      params,
      headers,
      body,
      postAction
    };
  },

  aRemoteData({ path, query, params, headers } = {}) {
    path = $(path, "/fake-remote");

    return {
      type: "Remote",
      path,
      query,
      params,
      headers
    };
  },

  aReportPage({ title, description, inputs, tabs } = {}) {
    title = this.aPageTitle({ title, description }).schema;
    inputs = $(inputs, []);
    tabs = $(tabs, [this.aReportPageTab()]);

    return {
      type: "ReportPage",
      schema: { title, inputs, tabs }
    };
  },

  aReportPageTab({ id, title, contents, fullScreen, icon, overflow, showWhen } = {}) {
    id = $(id, "test-tab");
    title = $(title, "Test Tab");
    contents = $(contents, [this.aReportPageTabContent()]);
    fullScreen = $(fullScreen, false);
    icon = $(icon, this.anIcon());
    overflow = $(overflow, false);
    showWhen = $(showWhen, undefined);

    return { id, title, contents, fullScreen, icon, overflow, showWhen };
  },

  aReportPageTabContent({ component, narrow, showWhen } = {}) {
    component = $(component, this.anExpected({ value: "Test content is given for testing purposes" }));
    narrow = $(narrow, false);
    showWhen = $(showWhen, undefined);

    return { component, narrow, showWhen };
  },

  aScreenSize({ name } = {}) {
    name = $(name, "lg");

    return screens.find(screen => screen.name === name) || null;
  },

  aSelect({ label, localizeLabel, optionLabel, optionValue, showClear, stateful, data, inline } = {}) {
    label = $(label, "Spec: Test");
    localizeLabel = $(localizeLabel, false);
    showClear = $(showClear, false);
    stateful = $(stateful, false);
    data = $(data, ["Test Option 1", "Test Option 2"]);
    inline = $(inline, true);

    data = inline
      ? this.anInlineData(data)
      : this.aComputedData({
        composable: "useDelayedData",
        options: this.anInlineData({ ms: 1, data })
      });

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
      ? this.anInlineData(data)
      : this.aComputedData({
        composable: "useDelayedData",
        options: this.anInlineData({ ms: 1, data })
      });

    return {
      type: "SelectButton",
      schema: { allowEmpty, localizeLabel, optionLabel, optionValue, stateful },
      data
    };
  },

  aSideMenu({ logo, largeLogo, menu, data, footer } = {}) {
    logo = $(logo, "logo.svg");
    largeLogo = $(largeLogo, "logo-full.svg");
    menu = $(menu, []);
    data = $(data, { path: "/test" });
    footer = $(footer, this.anExpected());

    return {
      type: "SideMenu",
      schema: { logo, largeLogo, menu, footer },
      data: this.anInlineData(data)
    };
  },

  aSideMenuItem({ route, icon, title, disabled } = {}) {
    route = $(route, "/item");
    icon = $(icon, "pi pi-home");
    disabled = $(disabled, false);

    return { route, icon, title, disabled };
  },

  aSimpleForm({ buttonIcon, buttonLabel, inputs, action }) {
    buttonIcon = $(buttonIcon, "pi pi-save");
    buttonLabel = $(buttonLabel, "Button Label");
    inputs = $(inputs, []);

    return {
      type: "SimpleForm",
      schema: {
        buttonIcon,
        buttonLabel,
        inputs
      },
      action
    };
  },

  aText({ value, data, maxLength } = {}) {
    value = $(value, "Test string");
    data = $(data, this.anInlineData(value));

    return {
      type: "Text",
      schema: { maxLength },
      data
    };
  },

  aToken({ accessExpired, admin } = {}) {
    accessExpired = $(accessExpired, false);
    admin = $(admin, false);

    return {
      access:
        accessExpired ? expiredAccessToken :
          admin ? adminUiToken :
            accessToken,
      refresh: refreshToken
    };
  },

  aTrigger({ on, when, parts, constraint } = {}) {
    if(when) {
      return {
        type: "When",
        when,
        constraint
      };
    }

    if(parts) {
      return {
        type: "Composite",
        parts
      };
    }

    on = $(on, "something-happened");

    return {
      type: "On",
      on,
      constraint
    };
  }
};
