import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/layout", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  test("default layout", async({ page }) => {
    const button = page.getByTestId("default");

    await button.click();

    await expect(page.locator(".b-component--DefaultLayout")).toBeAttached();
  });

  test("modal layout", async({ page }) => {
    const button = page.getByTestId("modal");

    await button.click();

    await expect(page.locator(".b-component--ModalLayout")).toBeAttached();
  });
});
