import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/language-switcher", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("component", async() => {

  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
