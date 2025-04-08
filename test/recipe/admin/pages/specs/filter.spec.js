import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/filter", { waitUntil: "hydration" });
});

test.describe("Filter", () => {
  const id = "filter";

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);
    const options = component.locator("#filter");

    await options.nth(0).click();

    await expect(component).toHaveScreenshot();
  });
});
