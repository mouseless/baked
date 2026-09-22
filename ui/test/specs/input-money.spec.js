import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-money", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("placeholder", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("shows currency addon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.inputGroupAddon.base).locator("i")).toHaveClass(/pi-dollar/);
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
});

test.describe("Custom Icon", () => {
  const id = "Custom Icon";

  test("shows the given icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.inputGroupAddon.base).locator("i")).toHaveClass(/pi-turkish-lira/);
  });
});
