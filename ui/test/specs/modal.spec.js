import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/modal", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("A button with given label is rendered", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Modal Button");
  });

  test("Button click toggles modal dialog", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog).toBeAttached();
  });

  test("Given content, header and footer is displayed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.content)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.content).locator(baked.string.text)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.content).locator(baked.string.text)).toHaveText("Modal Content");

    await expect(dialog.locator(primevue.dialog.footer)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(baked.string.text)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(baked.string.text)).toHaveText("Modal Footer");

    await expect(dialog.locator(primevue.dialog.header)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.header).locator(baked.string.text)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.header).locator(baked.string.text)).toHaveText("Modal Header");
  });
});
