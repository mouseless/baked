import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/search", { waitUntil: "hydration" });
});

test.describe("Search", () => {
  const id = "search";

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);
    const options = component.locator("#search-box");

    await options.nth(0).click();

    await expect(component).toHaveScreenshot();
  });
});
