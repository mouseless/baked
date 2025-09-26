import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/", { waitUntil: "hydration" });
});

test.describe("Layout", () => {
  test("has path marker class", async({page}) => {
    await expect(page.locator(".b--root")).toBeAttached();
  });

  test("has component type marker class", async({page}) => {
    await expect(page.locator(".b-component--DefaultLayout")).toBeAttached();
  });
});

test.describe("Page", () => {
  test("has 'page' marker class", async({page}) => {
    await expect(page.locator(".b--page")).toBeAttached();
  });

  test("has path marker class", async({page}) => {
    await expect(page.locator(".b--index")).toBeAttached();
  });

  test("has component type marker class", async({page}) => {
    await expect(page.locator(".b-component--MenuPage")).toBeAttached();
  });

  test("has route marker class", async({page}) => {
    await expect(page.locator(".b-route--index")).toBeAttached();
  });
});
