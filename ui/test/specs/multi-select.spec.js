import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/multi-select", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    await component.click();

    await expect(options.nth(0)).toHaveText("OPTION_1");
    await expect(options.nth(1)).toHaveText("OPTION_2");
  });

  test("no clear button", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.multiselect.clearIcon)).not.toBeAttached();
  });

  test("select multiple options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await options.nth(0).click();
    await options.nth(1).click();

    await expect(model).toHaveText("[ \"OPTION_1\", \"OPTION_2\" ]");
  });

  test("deselect an option", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await options.nth(0).click();
    await options.nth(0).click();

    await expect(model).toBeEmpty();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Base w/ Localization", () => {
  const id = "Base w/ Localization";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    await component.click();

    await expect(options.nth(0)).toHaveText("Option 1");
    await expect(options.nth(1)).toHaveText("Option 2");
  });
});

test.describe("Option Label and Value", () => {
  const id = "Option Label and Value";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    await component.click();

    await expect(options.nth(0)).toHaveText("LABEL_1");
    await expect(options.nth(1)).toHaveText("LABEL_2");
  });

  test("select option", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await options.nth(0).click();

    await expect(model).toHaveText("[ \"VALUE_1\" ]");
  });
});

test.describe("Option Label and Value with Localization", () => {
  const id = "Option Label and Value with Localization";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    await component.click();

    await expect(options.nth(0)).toHaveText("Label 1");
    await expect(options.nth(1)).toHaveText("Label 2");
  });
});

test.describe("Show Clear", () => {
  const id = "Show Clear";

  test("clear button", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.multiselect.clearIcon)).toBeAttached();
  });

  test("clears the selection", async({ page }) => {
    const component = page.getByTestId(id);
    const model = page.getByTestId(`${id}:model`);

    await component.locator(primevue.multiselect.clearIcon).click();

    await expect(model).toBeEmpty();
  });
});

test.describe("Max Selected Labels", () => {
  const id = "Max Selected Labels";

  test("shows selected labels when within the limit", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);

    await component.click();
    await options.nth(0).click();
    await options.nth(1).click();

    await expect(component.locator(primevue.multiselect.label)).toHaveText("OPTION_1, OPTION_2");
  });

  test("shows an overflow message when the limit is exceeded", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);

    await component.click();
    await options.nth(0).click();
    await options.nth(1).click();
    await options.nth(2).click();
    await options.nth(3).click();

    await expect(component.locator(primevue.multiselect.label)).toHaveText("4 items selected");
  });
});

test.describe("Show Toggle All", () => {
  const id = "Show Toggle All";

  test("selects all options", async({ page }) => {
    const component = page.getByTestId(id);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await page.locator(primevue.multiselect.selectAll).click();

    await expect(model).toHaveText("[ \"OPTION_1\", \"OPTION_2\" ]");
  });

  test("deselects all options", async({ page }) => {
    const component = page.getByTestId(id);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await page.locator(primevue.multiselect.selectAll).click();
    await page.locator(primevue.multiselect.selectAll).click();

    await expect(model).toBeEmpty();
  });
});

test.describe("Stateful and Not Inline", () => {
  const id = "Stateful and Not Inline";

  test("initial model is selected when state is empty", async({ page }) => {
    const model = page.getByTestId(`${id}:model`);

    await expect(model).toHaveText("[ \"OPTION 1\" ]");
  });

  test("retains selected state", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    const model = page.getByTestId(`${id}:model`);
    await expect(component.locator(primevue.multiselect.base)).toBeAttached();
    await component.click();
    await options.nth(1).click();

    await page.locator("a[href='/specs']").nth(0).click();
    await page.locator("a[href='/specs/multi-select']").nth(0).click();

    await expect(model).toHaveText("[ \"OPTION 1\", \"OPTION 2\" ]");
  });
});

test.describe("Stateful and Inline", () => {
  const id = "Stateful and Inline";

  test("initial model is selected when state is empty", async({ page }) => {
    const model = page.getByTestId(`${id}:model`);

    await expect(model).toHaveText("[ \"OPTION 1\" ]");
  });

  test("retains selected state", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    const model = page.getByTestId(`${id}:model`);
    await expect(component.locator(primevue.multiselect.base)).toBeAttached();
    await component.click();
    await options.nth(1).click();

    await page.locator("a[href='/specs']").nth(0).click();
    await page.locator("a[href='/specs/multi-select']").nth(0).click();

    await expect(model).toHaveText("[ \"OPTION 1\", \"OPTION 2\" ]");
  });
});

test.describe("Set Selected", () => {
  const id = "Set Selected";

  test("initial selection is displayed", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.multiselect.label)).toHaveText("ValueA, ValueB");
  });
});

test.describe("Page Context", () => {
  const id = "Page Context";

  test("when no option is selected, page context is empty", async({ page }) => {
    const pageContext = page.getByTestId(`${id}:page-context`);

    await expect(pageContext).toBeEmpty();
  });

  test("selected options are set to the page context with the given key", async({ page }) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.multiselect.option);
    const pageContext = page.getByTestId(`${id}:page-context`);
    await expect(component.locator(primevue.multiselect.base)).toBeAttached();

    await component.click();
    await options.nth(0).click();
    await options.nth(1).click();

    await expect(pageContext).toHaveText("[ \"ValueA\", \"ValueB\" ]");
  });
});

test.describe("Validation", () => {
  const id = "Validation";

  test("component shows the message component under the component", async({ page }) => {
    const component = page.getByTestId(id);
    const message = component.locator(baked.message.base);

    await expect(message).toHaveText("this is an error message");
  });
});
