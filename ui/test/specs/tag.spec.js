import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/tag", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("display full localized text", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.tag.base)).toHaveText("Success");
  });

  test("uses severity", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.tag.base)).toHaveClass(/.p-tag-success/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Array", () => {
  const id = "Array";

  test("creates tag per item when data is array", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.tag.base).nth(0)).toHaveText("item 1");
    await expect(component.locator(primevue.tag.base).nth(1)).toHaveText("item 2");
  });
});