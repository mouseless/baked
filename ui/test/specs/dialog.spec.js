import { expect, test } from "@nuxt/test-utils/playwright";
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
    await expect(dialog.locator(primevue.dialog.content).getByTestId("content")).toBeAttached();
    await expect(dialog.locator(primevue.dialog.content).getByTestId("content")).toHaveText("Dialog Content");
  });
});