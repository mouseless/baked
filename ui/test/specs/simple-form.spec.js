import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs/simple-form", { waitUntil: "hydration" });
  await page.route("*/**/form-sample/state", async route => {
    await route.fulfill("fake-response");
  });
});

test.describe("Base", () => {
  const id = "Base";

  test("Render given inputs", async({ page }) => {
    const component = page.getByTestId(id);
    const number = component.locator(primevue.select.base);
    const select = component.locator(primevue.select.base);
    const text = component.locator(primevue.select.base);

    await expect(number).toBeAttached();
    await expect(select).toBeAttached();
    await expect(text).toBeAttached();
  });

  test("Execute given remote action", async({ page }) => {
    const requestPromise = page.waitForRequest(req => req.url().includes("form-sample/state"));
    const component = page.getByTestId(id);
    const number = component.locator(".b-component--InputNumber .p-inputnumber-input");
    const select = component.locator(primevue.select.base);
    const text = component.locator(".b-component--InputText");
    const options = page.locator(primevue.select.option);
    const button = component.locator(primevue.button.base);

    await number.fill("1");
    await select.click();
    await options.nth(0).click();
    await text.fill("text");
    await button.click();

    const request = await requestPromise;
    expect(request.method()).toBe("POST");
    expect(request.headers()["authorization"]).toContain("token-admin-ui");
    expect(request.url()).toContain("/form-sample/state");
    expect(request.postDataJSON()).toEqual({
      number: 1,
      select: "OPTION_1",
      text: "text"
    });
  });
});