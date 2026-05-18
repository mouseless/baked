import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";
import baked from "../utils/locators/baked.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/error-handling", { waitUntil: "hydration" });
});

const id = "error-handling";

test("toast error", async({ page }) => {
  const content = page.getByTestId(id);

  await content.getByTestId("alert").click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
  await expect(page.locator(primevue.toast.summary)).toHaveText("400");
});

test("full page error", async({ page }) => {
  const content = page.getByTestId(id);

  await content.getByTestId("page").click();

  await expect(page.getByTestId(baked.errorPage.baseTestId)).toBeVisible();
  await expect(page.getByTestId(baked.errorPage.baseTestId).locator(baked.errorPage.statusCode)).toHaveText("404");
});

test("full page fetch unhandled", async({ page }) => {
  const content = page.getByTestId(id);

  await content.getByTestId("pageErrorFetch").click();

  await expect(page.getByTestId(baked.errorPage.baseTestId)).toBeVisible();
  await expect(page.getByTestId(baked.errorPage.baseTestId).locator(baked.errorPage.statusCode)).toHaveText("500");
});

test("full page type error", async({ page }) => {
  const content = page.getByTestId(id);

  await content.getByTestId("type").click();

  await expect(page.getByTestId(baked.errorPage.baseTestId)).toBeVisible();
  await expect(page.getByTestId(baked.errorPage.baseTestId).locator(baked.errorPage.statusCode)).toHaveText("APP");
});

test("redirect and toast with fetch error data", async({ page }) => {
  const content = page.getByTestId(id);

  await content.getByTestId("redirect").click();

  await expect(page).toHaveURL("/login?redirect=/specs/error-handling");
  await expect(page.locator(primevue.toast.summary)).toHaveText("Authentication");
  await expect(page.locator(primevue.toast.detail)).toHaveText("Failed to authenticate with given credentials.");
});
