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
    const text = page.getByTestId("text");

    await expect(text).toHaveText("Test Text");

    button.click();
    const language = page.locator("#overlay_menu > ul > li:nth-child(2) > div > a");
    language.click();

    await expect(text).toHaveText("Test Metni");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
