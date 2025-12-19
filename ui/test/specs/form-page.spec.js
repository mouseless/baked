import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/form-page", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h1")).toHaveText("Title");
  });

  test("description", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).toHaveText("Description");
  });

  test("inputs", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");

    await expect(text).toBeAttached();
  });

  test("button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(1);

    await expect(button).toHaveText("Submit");
  });

  test("button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(1);

    await expect(button).toBeDisabled();
  });

  test("button is enabled when inputs are ready", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base).nth(1);

    await text.fill("text");

    await expect(button).not.toBeDisabled();
  });

  test("action", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base).nth(1);

    await text.fill("text");
    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("text");
  });

  test("button is disabled until action is completed", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(".b-component--InputText");
    const button = component.locator(primevue.button.base).nth(1);

    await text.fill("text");
    await button.click();

    await expect(button).toBeDisabled();
    const spinner = button.locator(".p-button-loading-icon, .pi-spinner");
    await expect(spinner).toBeVisible();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(button).not.toBeDisabled();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Multiple Inputs", () => {
  const id = "Multiple Inputs";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
