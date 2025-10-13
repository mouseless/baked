import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs/language-switcher", { waitUntil: "hydration" });
  await page.route("*/**/time-provider-samples/now", async route => {
    const json = Date.now();
    await route.fulfill({ json });
  });
});

test.describe("Base", () => {
  const id = "component-block";

  test("language overlay", async({ page }) => {
    const block = page.getByTestId(id);
    const button = block.locator(".p-button");

    await button.click();

    const menu = page.locator("#overlay_menu");
    await expect(menu).toBeVisible();
  });

  test("change language", async({ page }) => {
    const block = page.getByTestId(id);
    const button = block.locator(".p-button");
    const text = page.getByTestId("text");

    await expect(text).toHaveText("Test Text");

    await button.click();
    const language = page.locator("#overlay_menu > ul > li:nth-child(2) > div > a");
    await language.click();

    await expect(text).toHaveText("Test Metni");
  });

  test("send language header with request", async({ page }) => {
    const requestPromise = page.waitForRequest(req => req.url().includes("time-provider-samples"));
    const requestButton = page.getByTestId("requestWithLanguageHeader");
    const block = page.getByTestId(id);
    const button = block.locator(".p-button");

    await button.click();
    const language = page.locator("#overlay_menu > ul > li:nth-child(2) > div > a");
    await language.click();
    await requestButton.click();

    const request = await requestPromise;
    expect(request.headers()["accept-language"]).toContain("tr");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
