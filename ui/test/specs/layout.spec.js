import { expect, test } from "@nuxt/test-utils/playwright";

test.describe("Base", () => {
  test("default layout", async({ goto, page }) => {
    await goto("/specs/layout", { waitUntil: "hydration" });

    await expect(page.locator(".b-component--DefaultLayout")).toBeAttached();
  });

  test("modal layout", async({ goto, page }) => {
    await goto("/specs/layout", { waitUntil: "hydration" });
    const button = page.getByTestId("login");

    await button.click();

    await expect(page.locator(".b-component--ModalLayout")).toBeAttached();
  });
});
