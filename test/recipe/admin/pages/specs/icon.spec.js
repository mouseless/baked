import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/icon", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("icon class", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".pi-wave-pulse")).toBeVisible();
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
