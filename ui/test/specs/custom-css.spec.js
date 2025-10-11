import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/custom-css", { waitUntil: "hydration" });
});

const id = "test";

test("custom css hides", async({ page }) => {
  const component = page.getByTestId(id);

  await expect(component.locator(".custom-css-visible")).toBeVisible();
});

test("custom css shows", async({ page }) => {
  const component = page.getByTestId(id);

  await expect(component.locator(".custom-css-hidden")).not.toBeVisible();
});
