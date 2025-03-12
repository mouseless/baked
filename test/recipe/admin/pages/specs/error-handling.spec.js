import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/error-handling", { waitUntil: "hydration" });
});

const id = "error-handling";

test("full page error", async({page}) => {
  const content = page.getByTestId(id);

  await content.getByTestId("full-page-error").click();

  await expect(page.locator(".p-error")).toBeVisible();
});

test("toast error", async({page}) => {
  const content = page.getByTestId(id);

  await content.getByTestId("toast-error").click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
  await expect(page.locator(primevue.toast.summary)).toHaveText("Beklenmeyen Hata");
});

test("custom handler full page error", async({page}) => {
  const content = page.getByTestId(id);

  await content.getByTestId("custom-handler-full-page").click();

  await expect(page.locator(".p-error")).toBeVisible();
});

test("custom handler toast error", async({page}) => {
  const content = page.getByTestId(id);

  await content.getByTestId("custom-handler-toast").click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
  await expect(page.locator(primevue.toast.summary)).toHaveText("Custom Handler");
});
