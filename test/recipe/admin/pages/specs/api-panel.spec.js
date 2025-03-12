import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/api-panel", { waitUntil: "hydration" });
});

const id = "test";

test("write tests", async({page}) => {
  const component = page.getByTestId(id);

  await expect(component.locator(".custom-css-visible")).toBeVisible();
});
