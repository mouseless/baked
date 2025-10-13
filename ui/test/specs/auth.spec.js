import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import mockMe from "../utils/mockMe";
import giveMe from "../utils/giveMe";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs", { waitUntil: "hydration" });
  await page.route("*/**/authentication-samples/login", async route => {
    const json = giveMe.aToken();
    await route.fulfill({ json });
  });
  await page.route("*/**/authentication-samples/refresh", async route => {
    const json = giveMe.aToken();
    await route.fulfill({ json });
  });
  await page.route("*/**/time-provider-samples/now", async route => {
    const json = Date.now();
    await route.fulfill({ json });
  });
});

test("redirect to login", async({ goto, page }) => {
  await goto("/specs/auth");

  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("show toast for 400 errors at login page", async({ goto, page }) => {
  await page.route("*/**/authentication-samples/login", async route =>
    await route.fulfill({
      status: 400,
      response: { title: "title", detail: "detail" }
    })
  );
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");

  await form.locator(primevue.button.base).click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
});

test("view authorized page after login", async({ goto, page }) => {
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");

  await login(form);

  await expect(page).toHaveURL("/specs/auth");
});

test("logout redirects to login", async({ goto, page }) => {
  const token = giveMe.aToken();
  await mockMe.theSession(page, token);
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("logout").click();

  await expect(page).toHaveURL("/login");
});

test("redirects to login page when backend returns 401 error", async({ goto, page }) => {
  const token = giveMe.aToken();
  await mockMe.theSession(page, token);
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("exception").click();

  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("refresh token before navigation when access is expired", async({ goto, page }) => {
  const requestPromise = page.waitForRequest(req => req.url().includes("refresh"));
  const token = giveMe.aToken({ accessExpired: true });
  await mockMe.theSession(page, token);

  await goto("/specs/auth");

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toContain(`Bearer ${token.refresh}`);
  await expect(page).toHaveURL("/specs/auth");
});

test("add access token to fetch requests", async({ goto, page }) => {
  const requestPromise = page.waitForRequest(req => req.url().includes("time-provider-samples/now"));
  const token = giveMe.aToken();
  await mockMe.theSession(page, token);
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("request").click();

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toContain(`Bearer ${token.access}`);
});

test("refresh token before fetch when access is expired", async({ goto, page }) => {
  const requestPromise = page.waitForRequest(req => req.url().includes("refresh"));
  const token = giveMe.aToken();
  await mockMe.theSession(page, token);
  await goto("/specs/auth", { waitUntil: "hydration" });

  const expiredToken = giveMe.aToken({ accessExpired: true });
  await mockMe.theSession(page, expiredToken);
  await page.getByTestId("request").click();

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toContain(`Bearer ${expiredToken.refresh}`);
});

async function login(form) {
  await form.getByPlaceholder("Username").fill("Username");
  await form.locator(primevue.button.base).click();
}
