import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/dialog", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Dialog Button");
  });

  test("show dialog", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog).toBeAttached();
  });

  test("header and content", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.header)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.header)).toHaveText("Dialog Header");

    await expect(dialog.locator(primevue.dialog.content)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.content).locator(baked.string.text)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.content).locator(baked.string.text)).toHaveText("Dialog Content");
  });
});

test.describe("Action", () => {
  const id = "Action";

  test("submit button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.footer)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base)).toHaveText("Submit");
  });

  test("execute action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const dialogAction = dialog.locator(primevue.dialog.footer).locator(primevue.button.base);

    await button.click();
    await dialogAction.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Execute Action");
    await expect(dialog).not.toBeAttached();
  });

  test("does not execute action when closed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const dialogClose = dialog.locator(primevue.dialog.close);

    await button.click();
    await dialogClose.click();

    await expect(page.locator(primevue.toast.base)).not.toBeVisible();
  });
});
