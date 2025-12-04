import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/none", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("format", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
