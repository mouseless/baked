import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/simple-form", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("Renders form with given inputs", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base);

    await expect(text).toBeAttached();
    await expect(button).toBeAttached();
    await expect(button).toHaveText("Submit");
  });

  test("Button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeDisabled();
  });

  test("Button is enabled when inputs are ready", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base);

    await text.fill("text");

    await expect(button).not.toBeDisabled();
  });

  test("Execute given remote action", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base);

    await text.fill("text");
    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("text");
  });

  test("Button is disabled until action is completed", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base);

    await text.fill("text");
    await button.click();

    await expect(button).toBeDisabled();
    const spinner = button.locator(".p-button-loading-icon, .pi-spinner");
    await expect(spinner).toBeVisible();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(button).not.toBeDisabled();
  });
});