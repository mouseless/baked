import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs/button?val=2", { waitUntil: "hydration" });
  await page.route("*/**/rich-transient-with-datas/12/method", async route => {
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

    await expect(component.locator(primevue.button.base)).toHaveText("Button");
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

  test("Execute given composite action", async({ page }) => {
    const requestPromise = page.waitForRequest(req => req.url().includes("rich-transient-with-datas"));
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Execute Action");

    const request = await requestPromise;
    expect(request.method()).toBe("POST");
    expect(request.headers()["authorization"]).toContain("token-admin-ui");
    expect(request.url()).toContain("/rich-transient-with-datas/12/method");
    expect(request.url()).toContain("?val=2");
    expect(request.postDataJSON()).toEqual({ text: "text" });
  });

  test("Execute given post action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base).last()).toBeVisible();
    await expect(page.locator(primevue.toast.summary).last()).toHaveText("Execute Post Action");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});