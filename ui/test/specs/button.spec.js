import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs/button", { waitUntil: "hydration" });
  await page.route("*/**/rich-transient-with-datas/1/method", async route => {
    await route.fulfill("fake-response");
  });
});

test.describe("Base", () => {
  const id = "Base";

  test("icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.icon)).toHaveClass(/pi-play-circle/);
  });

  test("label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveText("Base");
  });

  test("loading", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(button).toHaveClass(/p-button-loading/);

    const spinner = button.locator(".p-button-loading-icon, .pi-spinner");
    await expect(spinner).toBeVisible();
    await expect(button).toBeDisabled();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Actions", () => {
  test("Remote", async({ page }) => {
    const id = "Remote";
    const requestPromise = page.waitForRequest(req => req.url().includes("rich-transient-with-datas"));
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    const request = await requestPromise;
    expect(request.headers()["authorization"]).toContain("token-admin-ui");
    expect(request.url()).toContain("/rich-transient-with-datas/1/method");
    expect(request.url()).toContain("?query=value");
  });
});