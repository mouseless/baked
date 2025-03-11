import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/error-handling", { waitUntil: "hydration" });
});

const id = "error-handling";

test("full page error", async({page}) => {
  const content = page.getByTestId(id);

  await content.getByTestId("full-page-error").click();

  // this expect is for demonstration
  // should be changed to expect error page/component
  await expect(page.locator("h1")).toHaveText("404");
});

test("toast error", async({page}) => {
  const content = page.getByTestId(id);

  await content.getByTestId("toast-error").click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
});
