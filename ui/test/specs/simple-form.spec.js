import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/simple-form", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);
    const title = component.locator("h1");

    await expect(title).toBeAttached();
    await expect(title).toHaveText("Simple Form");
  });

  test("inputs", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("input")).toBeAttached();
  });

  test("button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Submit");
  });

  test("button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeDisabled();
  });

  test("button is enabled when inputs are ready", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base);

    await input.fill("text");

    await expect(button).not.toBeDisabled();
  });

  test("action", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base);

    await input.fill("text");
    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("text");
  });

  test("button is disabled until action is completed", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base);

    await input.fill("text");
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

test.describe("Dialog", () => {
  const id = "Dialog";

  test("open button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Simple Form");
  });

  test("header", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.header)).toHaveText("Simple Form");
  });

  test("message", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.content)).toHaveText(/Message/);
  });

  test("input", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.content).getByTestId("input")).toBeAttached();
  });

  test("buttons", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(0)).toHaveText("Cancel");
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1)).toHaveText("Submit");
  });

  test("executes action after close", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submitButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1);
    const text = dialog.locator(primevue.dialog.content).getByTestId("input");

    await button.click();
    await text.fill("text");
    await submitButton.click();

    await expect(dialog).not.toBeAttached();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
  });

  test("does not execute action when closed", async({ page }) => {
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
