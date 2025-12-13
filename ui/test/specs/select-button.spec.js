import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/select-button", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);

    await expect(options.nth(0)).toHaveText("OPTION_1");
    await expect(options.nth(1)).toHaveText("OPTION_2");
  });

  test("select option", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const model = page.getByTestId(`${id}:model`);

    await options.nth(0).click();

    await expect(model).toHaveText("OPTION_1");
  });

  test("prevent empty after select", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const model = page.getByTestId(`${id}:model`);

    await options.nth(0).click(); // select
    await options.nth(0).click(); // try deselect

    await expect(model).toHaveText("OPTION_1");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);

    await options.nth(0).click();

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Base w/ Localization", () => {
  const id = "Base w/ Localization";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);

    await expect(options.nth(0)).toHaveText("Option 1");
    await expect(options.nth(1)).toHaveText("Option 2");
  });
});

test.describe("Option Label and Value", () => {
  const id = "Option Label and Value";

  test("options", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);

    await expect(options.nth(0)).toHaveText("Label 1");
    await expect(options.nth(1)).toHaveText("Label 2");
  });

  test("select option", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const model = page.getByTestId(`${id}:model`);

    await options.nth(0).click();

    await expect(model).toHaveText("VALUE_1");
  });
});

test.describe("Allow Empty", () => {
  const id = "Allow Empty";

  test("clears selection", async({ page }) => {
    const component = page.getByTestId(id);
    const option = component.locator(primevue.selectbutton.option);
    const model = page.getByTestId(`${id}:model`);

    await option.click(); // select
    await option.click(); // deselect

    await expect(model).not.toHaveText("OPTION");
  });
});

test.describe("Stateful and Not Inline", () => {
  const id = "Stateful and Not Inline";

  test("initial model is selected when state is empty", async({ page }) => {
    const model = page.getByTestId(`${id}:model`);

    await expect(model).toHaveText("OPTION 1");
  });

  test("retains selected state", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const model = page.getByTestId(`${id}:model`);
    await options.nth(1).click();

    await page.locator("a[href='/specs']").nth(0).click();
    await page.locator("a[href='/specs/select-button']").nth(0).click();

    await expect(model).toHaveText("OPTION 2");
  });
});

test.describe("Stateful and Inline", () => {
  const id = "Stateful and Inline";

  test("initial model is selected when state is empty", async({ page }) => {
    const model = page.getByTestId(`${id}:model`);

    await expect(model).toHaveText("OPTION 1");
  });

  test("retains selected state", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const model = page.getByTestId(`${id}:model`);
    await options.nth(1).click();

    await page.locator("a[href='/specs']").nth(0).click();
    await page.locator("a[href='/specs/select-button']").nth(0).click();

    await expect(model).toHaveText("OPTION 2");
  });
});

test.describe("Page Context", () => {
  const id = "Page Context";

  test("when no option is selected, page context is empty", async({ page }) => {
    const pageContext = page.getByTestId(`${id}:page-context`);

    await expect(pageContext).toBeEmpty();
  });

  test("selected option is set to the page context with the given key", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const pageContext = page.getByTestId(`${id}:page-context`);

    await component.click();
    await options.nth(0).click();

    await expect(pageContext).toHaveText("OPTION 1");
  });

  test("when selection changes page context is updated", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const pageContext = page.getByTestId(`${id}:page-context`);

    await component.click();
    await options.nth(1).click();

    await expect(pageContext).toHaveText("OPTION 2");
  });
});

test.describe("Page Context - Option Label and Value", () => {
  const id = "Page Context - Option Label and Value";

  test("when no option is selected, page context is empty", async({ page }) => {
    const pageContext = page.getByTestId(`${id}:page-context`);

    await expect(pageContext).toBeEmpty();
  });

  test("selected option is set to the page context with the given key", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const pageContext = page.getByTestId(`${id}:page-context`);

    await component.click();
    await options.nth(0).click();

    await expect(pageContext).toHaveText("OPTION 1");
  });

  test("when selection changes page context is updated", async({ page }) => {
    const component = page.getByTestId(id);
    const options = component.locator(primevue.selectbutton.option);
    const pageContext = page.getByTestId(`${id}:page-context`);

    await component.click();
    await options.nth(1).click();

    await expect(pageContext).toHaveText("OPTION 2");
  });
});

test.describe("Set Selected", () => {
  const id = "Set Selected";

  test("initial model is selected", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.selectbutton.selected)).toHaveText("VALUE_B");
  });
});
