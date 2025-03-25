import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/select", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("options", async({page}) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.select.option);
    await component.click();

    console.log(options);
    await expect(options.nth(0)).toHaveText("OPTION 1");
    await expect(options.nth(1)).toHaveText("OPTION 2");
  });

  test("no clear button", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.select.clearIcon)).not.toBeAttached();
  });

  test("select option", async({page}) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.select.option);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await options.nth(0).click();

    await expect(model).toHaveText("OPTION 1");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Option Label and Value", () => {
  const id = "Option Label and Value";

  test("options", async({page}) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.select.option);
    await component.click();

    await expect(options.nth(0)).toHaveText("LABEL 1");
    await expect(options.nth(1)).toHaveText("LABEL 2");
  });

  test("select option", async({page}) => {
    const component = page.getByTestId(id);
    const options = page.locator(primevue.select.option);
    const model = page.getByTestId(`${id}:model`);

    await component.click();
    await options.nth(0).click();

    await expect(model).toHaveText("VALUE_1");
  });
});

test.describe("Show Clear", () => {
  const id = "Show Clear";

  test("clear button", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.select.clearIcon)).toBeAttached();
  });
});

test.describe("Long Label", () => {
  const id = "Long Label";

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
