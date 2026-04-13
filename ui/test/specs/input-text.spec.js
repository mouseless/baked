import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-text", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("placeholder", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("model", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("test value");

    await expect(model).toHaveText("test value");
  });

  test("label", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);

    await input.fill("x");

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
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

test.describe("Target Prop", () => {
  const id = "Target Prop";

  test("Model value is wrapped in an object and passed as a property of the target", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("1");

    await expect(model).toHaveText(/\{\s*"id": "1"\s*\}/);
  });
});
