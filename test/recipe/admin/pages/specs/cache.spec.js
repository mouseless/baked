import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";
import giveMe from "~/utils/giveMe";

test.beforeEach(async({ page }) => {
  await page.route("*/**/authentication-samples/login", async route => {
    await route.fulfill({ json: giveMe.aToken() });
  });
  await page.route("*/**/cache-samples/application", async route => {
    await route.fulfill({ json: giveMe.anApiResponse() });
  });
  await page.route("*/**/cache-samples/scoped", async route => {
    await route.fulfill({ json: giveMe.anApiResponse() });
  });
});

test.describe("application cache", () => {
  test("it is called only the first time", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/application", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it is not cleared after login", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/application", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it is not cleared after logout", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/application", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await logout({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it invalidates after configured time", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/application", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await page.clock.setFixedTime(new Date("2025-07-24T21:10:00"));
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await page.clock.setFixedTime(new Date("2025-07-24T22:10:00"));
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#2

    expect(callCount).toBe(2);
  });
});

test.describe("user cache", () => {
  test("it is called only the first time", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await goto("/specs/cache", { waitUntil: "hydration" }); // cache hit!

    expect(callCount).toBe(1);
  });

  test("it is cleared after login", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
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
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await login({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await logout({goto, page});
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#2

    expect(callCount).toBe(2);
  });

  test("it invalidates after configured time", async({goto, page}) => {
    let callCount = 0;
    await page.route("*/**/cache-samples/scoped", async route => {
      callCount++;
      await route.fulfill({ json: giveMe.anApiResponse() });
    });

    await page.clock.setFixedTime(new Date("2025-07-24T21:10:00"));
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#1
    await page.clock.setFixedTime(new Date("2025-07-24T22:10:00"));
    await goto("/specs/cache", { waitUntil: "hydration" }); // hit#2

    expect(callCount).toBe(2);
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
