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
    const title = component.locator("h1");

    await expect(title).toBeAttached();
    await expect(title).toHaveText("Simple Form");
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

test.describe("Dialog", () => {
  const id = "Dialog";

  test("A toggle button with given label is rendered", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Toggle Dialog");
  });

  test("Button click shows dialog with inputs as content with submit and cancel button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.header)).toHaveText("Simple Form");
    await expect(dialog.locator(primevue.dialog.content).locator(".b-component--InputText")).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(0)).toHaveText("Cancel");
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1)).toHaveText("Submit");
  });

  test("Executes action after close", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submitButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1);
    const text = dialog.locator(primevue.dialog.content).locator(".b-component--InputText");

    await button.click();
    await text.fill("text");
    await submitButton.click();

    await expect(dialog).not.toBeAttached();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
  });

  test("Does not execute action when closed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const dialogClose = dialog.locator(primevue.dialog.close);
    const cancelButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(0);

    await button.click();
    await dialogClose.click();

    await expect(page.locator(primevue.toast.base)).not.toBeVisible();

    await button.click();
    await cancelButton.click();

    await expect(page.locator(primevue.toast.base)).not.toBeVisible();
  });
});