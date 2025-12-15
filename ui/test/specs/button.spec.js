import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/button", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.icon)).toHaveClass(/pi-play-circle/);
  });

  test("label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveText("Button");
  });

  test("loading", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(button).toHaveClass(/p-button-loading/);
  });

  test("Execute action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Execute Action");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
