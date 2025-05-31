import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/language-switcher", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "component-block";

  test("language overlay", async({page}) => {
    const block = page.getByTestId(id);
    const button = block.locator(".p-button");

    button.click();

    const menu = page.locator("#overlay_menu");
    await expect(menu).toBeVisible();
  });

  test("change language", async({page}) => {
    const block = page.getByTestId(id);
    const button = block.locator(".p-button");

    button.click();
    const menu = page.locator("#overlay_menu");

    await expect(menu.locator("#overlay_menu_1")).toHaveId();
    const languageTr = menu.locator("#overlay_menu_1");

    languageTr.locator("a").click();

    await expect(menu).toBeVisible();
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
