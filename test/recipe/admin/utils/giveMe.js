// value or default function, named $ for quick access
function $(value, defaultValue) {
  return value === undefined ? defaultValue : value;
}

export default {
  aDetail({ title, header, menu, props, data })
  {
    title = $(title, "Test Title");
    header = $(header, this.anExpected({testId: "header", value: "Test Header"}));
    menu = $(menu, this.aMenu());
    props = $(props, []);
    data = $(data, { });

    return {
      type: "Detail",
      schema: { title, header, menu, props },
      data
    };
  },

  aDetailProp({ keyAndTestId, title, component })
  {
    keyAndTestId = $(keyAndTestId, "testKey");
    title = $(title, "Test Prop");
    component = $(component, this.anExpected({ testId: keyAndTestId }));

    return { key: keyAndTestId, title, component };
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
