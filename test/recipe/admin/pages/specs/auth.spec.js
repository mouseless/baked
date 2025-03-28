import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

//expires at 2999-03-28
const accessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjMyNDc5NjE0MTk0fQ.F4K4GkNqtuUNy6cgyOEtrLtaidgvVQmsw1Ouixyw5a0";
//expires at 2000-03-28
const expiredAccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjk1NDI0MTM5NH0.ZKPMybdzg1aO1g_xyV1QXUx9NR_vynu9s9z4Zll7WNA";
//expires at 9999-03-28
const refreshToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjI1MzM3ODIzNDk5NH0.IO-jutz7t-FbvgrQ87n0y_tSWUsSfiNPpfr3sAzvWhg";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs", { waitUntil: "hydration" });
  await page.route("*/**/authentication-samples/login", async route => {
    const json = {
      access: accessToken,
      refresh: refreshToken
    };
    await route.fulfill({ json });
  });
  await page.route("*/**/authentication-samples/refresh", async route => {
    const json = {
      access: accessToken,
      refresh: refreshToken
    };
    await route.fulfill({ json });
  });
  await page.route("*/**/time-provider-samples/now", async route => {
    const json = Date.now();
    await route.fulfill({ json });
  });
});

test("redirect to login", async({goto, page}) => {
  await goto("/specs/auth", { waitUntil: "hydration" });

  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("show toast for 400 errors at login page", async({goto, page}) => {
  await page.route("*/**/authentication-samples/login", async route =>
    await route.fulfill({
      status: 400,
      response: { title: "title", detail: "detail" }
    })
  );
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");

  await form.locator(".p-button").click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
});


test("view authorized page after login", async({goto, page}) => {
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");

  await login(form);

  await expect(page).toHaveURL("/specs/auth");
});

test("logout redirects to login", async({goto, page}) => {
  await setSession(page, { access: accessToken, refresh: refreshToken });
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("logout").click();

  await expect(page).toHaveURL("/login");
});

test("redirects to login page when backend returns 401 error", async({goto, page}) => {
  await setSession(page, { access: accessToken, refresh: refreshToken });
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("exception").click();

  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("refresh token before navigation when access is expired", async({goto, page}) => {
  await page.route("*/**/authentication-samples/login", async route => {
    const json = {
      access: expiredAccessToken,
      refresh: refreshToken
    };
    await route.fulfill({ json });
  });
  const requestPromise = page.waitForRequest(req => req.url().includes("refresh"));
  await setSession(page, { access: expiredAccessToken, refresh: refreshToken });

  await goto("/specs/auth", { waitUntil: "hydration" });

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toContain(`Bearer ${refreshToken}`);
  await expect(page).toHaveURL("/specs/auth");
});

test("add access token to fetch requests", async({goto, page}) => {
  const requestPromise = page.waitForRequest(req => req.url().includes("time-provider-samples/now"));
  await setSession(page, { access: accessToken, refresh: refreshToken });
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("request").click();

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toContain(`Bearer ${accessToken}`);
});

test("refresh token before fetch when access is expired", async({goto, page}) => {
  const requestPromise = page.waitForRequest(req => req.url().includes("refresh"));
  await setSession(page, { access: accessToken, refresh: refreshToken });
  await goto("/specs/auth", { waitUntil: "hydration" });

  setSession(page, { access: expiredAccessToken, refresh: refreshToken }).then(async() => {
    await page.getByTestId("request").click();
  });

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toContain(`Bearer ${refreshToken}`);
});

async function login(form) {
  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  await form.locator(".p-button").click();
}

async function setSession(page, { access, refresh }){
  await page.evaluate(() => localStorage.clear("token"));
  await page.evaluate(token => localStorage.setItem("token", token), JSON.stringify({ access, refresh }));
}
