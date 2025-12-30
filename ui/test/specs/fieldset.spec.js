import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/fieldset", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText(/Title/);
  });

  test("fields", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText(/Data 1/);
    await expect(component.getByTestId("prop-1")).toHaveText("Value 1");
    await expect(component).toHaveText(/Data 2/);
    await expect(component.getByTestId("prop-2")).toHaveText("Value 2");
  });

  test("wide field", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".md\\:col-span-2").getByTestId("prop-3")).toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
