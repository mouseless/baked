import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue.js";
import baked from "~/utils/locators/baked.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/error-handling", { waitUntil: "hydration" });
});

test.describe("Default Handler", () => {
  const id = "error-handling";

  test("toast error", async({page}) => {
    const content = page.getByTestId(id);

    await content.getByTestId("toast-error").click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("400");
  });

  test("full page error", async({page}) => {
    const content = page.getByTestId(id);

    await content.getByTestId("full-page-error").click();

    await expect(page.locator(baked.errorPage.base)).toBeVisible();
  });

  test("fetch error toast with data", async({page}) => {
    const content = page.getByTestId(id);

    await content.getByTestId("toast-options-from-fetch-error-data").click();

    await expect(page).toHaveURL("/");
    await expect(page.locator(primevue.toast.summary)).toHaveText("Authentication");
    await expect(page.locator(primevue.toast.detail)).toHaveText("Failed to authenticate with given credentials.");
  });
});
