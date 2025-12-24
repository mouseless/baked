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

  aButton({ action, icon, label, variant, severity, rounded } = {}) {
    label = $(label, "Button");
    action = $(action, this.aLocalAction({ showMessage: `${label} clicked` }));

    return {
      type: "Button",
      schema: { icon, label, variant, severity, rounded },
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

  aConditional({ testId, fallback, conditions, data } = {}) {
    testId = $(testId, "test");
    fallback = $(fallback, this.anExpected(testId));
    conditions = $(conditions, []);

    return {
      type: "Conditional",
      schema: {
        fallback,
        conditions
      },
      data
    };
  },

  aConditionalCondition({ prop, value, testId, component } = {}) {
    prop = $(prop, "testProp");
    value = $(value, "test-value");
    component = $(component, this.anExpected({ testId }));

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

  aContent({ component, narrow, key } = {}) {
    component = $(component, this.anExpected({ value: "Test content is given for testing purposes" }));
    narrow = $(narrow, false);
    key = $(key, "content");

    return { component, narrow, key };
  },

  aContextData({ key, prop, targetProp } = {}) {
    key = $(key, "parent");
    prop = $(prop, key === "parent" ? "data" : undefined);

    return {
      type: "Context",
      key,
      prop,
      targetProp
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

  aDataTable({ actionTemplate, columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, virtualScrollerOptions, data } = {}) {
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
      schema: { actionTemplate, columns, dataKey, exportOptions, footerTemplate, itemsProp, paginator, rows, rowsWhenLoading, scrollHeight, virtualScrollerOptions },
      data: this.anInlineData(data)
    };
  },

  aDataTableColumn({ title, key, alignRight, minWidth, component, exportable, frozen, footer } = {}) {
    title = $(title, "Spec: Test");
    key = $(key, "test");
    alignRight = $(alignRight, false);
    minWidth = $(minWidth, false);
    component = $(component,
      this.anExpected({
        data: footer ?
          this.aContextData({ key: "parent", prop: `data.${key}` }) :
          this.aContextData({ key: "parent", prop: `row.${key}` })
      })
    );
    exportable = $(exportable, false);

    return {
      title,
      key,
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

  aDialog({ action, content, header, open, submit }) {
    header = $(header, "Dialog Header");
    content = $(content, this.aText({ label: "Dialog Header" }));
    open = $(open, { label: "Spec: Show" });

    return {
      type: "Dialog",
      schema: {
        content,
        header,
        open,
        submit
      },
      action
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

  anExpectedInput({ testId, defaultValue, number, action } = {}) {
    testId = $(testId, "test-id");

    return {
      type: "ExpectedInput",
      schema: {
        testId,
        defaultValue,
        number
      },
      action
    };
  },

  aFilter({ placeholder, action } = {}) {
    placeholder = $(placeholder, "Filter");

    return {
      type: "Filter",
      schema: {
        placeholder
      },
      action
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

  aFormPage({ action, title, description, button, inputs } = {}) {
    title = this.aPageTitle({ title, description }).schema;
    button = $(button, this.aButton({ label: "Test Submit" }));
    inputs = $(inputs, []);

    return {
      type: "FormPage",
      schema: {
        title,
        button: button.schema,
        inputs
      },
      action
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

  anInput({ name, required, defaultValue, default_, defaultSelfManaged, queryBound, component } = {}) {
    name = $(name, "test");
    required = $(required, false);
    component = $(component, this.anExpectedInput());
    default_ = $(default_, defaultValue ? this.anInlineData(defaultValue) : undefined);
    defaultSelfManaged = $(defaultSelfManaged, false);
    queryBound = $(queryBound, undefined);

    return { name, required, default: default_, defaultSelfManaged, queryBound, component };
  },

  anInputText({ label } = {}) {
    return {
      type: "InputText",
      schema: {
        label
      }
    };
  },

  anInputNumber({ label } = {}) {
    return {
      type: "InputNumber",
      schema: {
        label
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

  aNavLink({ icon, path, idProp, textProp, data } = {}) {
    path = $(path, "/some-object/{0}");
    idProp = $(idProp, "id");
    textProp = $(textProp, "name");
    data = $(data, { id: "test-id", name: "Test" });

    return {
      type: "NavLink",
      schema: { icon, path, idProp, textProp },
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

  aPublishAction({ event, pageContextKey, data }) {
    if(!pageContextKey) {
      event = $(event, "something-happened");
    }

    data = $(data, this.aContextData({ key: "model" }));

    return {
      type: "Publish",
      event,
      pageContextKey,
      data
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

  aRemoteAction({ path, method, headers, query, params, body, postAction } = {}) {
    path = $(path, "/fake-remote");

    return {
      type: "Remote",
      path,
      method,
      headers,
      query,
      params,
      body,
      postAction
    };
  },

  aRemoteData({ path, query, params, headers, attributes } = {}) {
    path = $(path, "/fake-remote");

    return {
      type: "Remote",
      path,
      query,
      params,
      headers,
      attributes
    };
  },

  aSimplePage({ title, contents } = {}) {
    title = $(title, this.anExpected({ value: "Test Simple Page" }));
    contents = $(contents, [this.aContent()]);

    return {
      type: "SimplePage",
      schema: { title, contents }
    };
  },

  aTab({ id, title, contents, fullScreen, icon, overflow, reactions } = {}) {
    id = $(id, "test-tab");
    title = $(title, "Test Tab");
    contents = $(contents, [this.aContent()]);
    fullScreen = $(fullScreen, false);
    icon = $(icon, this.anIcon());
    overflow = $(overflow, false);
    reactions = $(reactions, undefined);

    return { id, title, contents, fullScreen, icon, overflow, reactions };
  },

  aTabbedPage({ title, description, inputs, tabs } = {}) {
    title = this.aPageTitle({ title, description }).schema;
    inputs = $(inputs, []);
    tabs = $(tabs, [this.aTab()]);

    return {
      type: "TabbedPage",
      schema: { title, inputs, tabs }
    };
  },

  aScreenSize({ name } = {}) {
    name = $(name, "lg");

    return screens.find(screen => screen.name === name) || null;
  },

  aSelect({ label, localizeLabel, optionLabel, optionValue, showClear, stateful, data, inline, action } = {}) {
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
      data,
      action
    };
  },

  aSelectButton({ allowEmpty, localizeLabel, optionLabel, optionValue, stateful, data, inline, action } = {}) {
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
      data,
      action
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

  aSimpleForm({ dialogOptions, inputs, submit, title, action }) {
    inputs = $(inputs, []);
    title = $(title, "Simple Form");
    submit = $(submit, this.aButton({ label: "Spec: Submit" }).schema);

    return {
      type: "SimpleForm",
      schema: {
        dialogOptions,
        inputs,
        submit,
        title
      },
      action
    };
  },

  aSimpleFormDialog({ open, cancel }) {
    open= $(open, this.aButton({ label: "Spec: Open" }).schema);
    cancel= $(cancel, this.aButton({ label: "Spec: Cancel" }).schema);

    return {
      open,
      cancel
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
