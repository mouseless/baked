import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";
import giveMe from "~/utils/giveMe";

test.beforeEach(async({ page }) => {
  await page.route("*/**/authentication-samples/login", async route => {
    const json = giveMe.aToken();
    await route.fulfill({ json });
  });
  await page.route("*/**/cache-samples/application", async route => {
    await route.fulfill({ json: "" });
  });
  await page.route("*/**/cache-samples/scoped", async route => {
    await route.fulfill({ json: "" });
  });
});

test.describe("application cache", () => {
  test("it is called only the first time", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/application", async route => {
      callCount++;
      await route.fulfill({ json: "" });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it is not cleared after login", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: "" });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it is not cleared after logout", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: "" });
    });

    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await logout({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });
});

test.describe("user cache", () => {
  test("it is called only the first time", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: "" });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it is cleared after login", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: "" });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#2
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(2);
  });

  test("it is cleared after logout", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: "" });
    });

    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await logout({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#2

    expect(callCount).toBe(1);
  });
});

async function login({goto, page}) {
  await goto("/login");
  const form = page.locator("form");
  await form.getByPlaceholder("Username").fill("test");
  await form.locator(primevue.button.base).click();
}

async function logout({goto, page}) {
  await goto("/specs/auth", { waitUntil: "hydration" });
  await page.getByTestId("logout").click();
}
