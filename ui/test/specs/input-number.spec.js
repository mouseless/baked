import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-number", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("placeholder", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("model", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputNumber.base);
    const model = page.getByTestId(`${id}:model`);

    await input.click();
    await page.keyboard.press("Digit1");
    await page.keyboard.press("Digit0");

    await expect(model).toHaveText("10");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Long Label", () => {
  const id = "Long Label";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("No Grouping", () => {
  const id = "No Grouping";

  test("does not group input value", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputNumber.base);

    await input.click();
    await page.keyboard.press("Digit1");
    await page.keyboard.press("Digit0");
    await page.keyboard.press("Digit0");
    await page.keyboard.press("Digit0");

    await expect(input.locator("input")).toHaveValue("1000");
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