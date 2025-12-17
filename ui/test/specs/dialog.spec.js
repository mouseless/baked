import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/dialog", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("A button with given label is rendered", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Dialog Button");
  });

  test("Button click shows dialog", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog).toBeAttached();
  });

  test("Dialog contains given header and content", async({ page }) => {
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

  test("Renders button when action label is defined", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.footer)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base)).toHaveText("Dialog Action");
  });

  test("Executes given action and hides dialog", async({ page }) => {
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
});
