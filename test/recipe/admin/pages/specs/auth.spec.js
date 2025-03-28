import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

const accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJVc2VyIjoiVXNlciJ9.du9y0K1tvI8BDiBiERa-QeKgOpot1GF8melbgs7qpOk";
const expiredAccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE3NDA3MjY2ODksImlhdCI6MTc0MDcyNjY4OX0.vOd6Gg2rZMj1-dXz_MaDSdaJH78W3QFPIuIa3IE-4nI";
const refreshToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJSZWZyZXNoIjoiUmVmcmVzaCJ9.DKVsDLHVct-qopfULCpbDOfvjYHY4iQcQ6EZ6P77Olw";

test.beforeEach(async({ page }) => {
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

  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  await form.locator(".p-button").click();

  await expect(page).toHaveURL("/specs/auth");
});

test("logout redirects to login", async({goto, page}) => {
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");
  await login(form);

  await page.getByTestId("logout").click();
  await expect(page).toHaveURL("/login");
});

test("redirects to login page when backend returns 401 error", async({goto, page}) => {
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");
  await login(form);

  await expect(page).toHaveURL("/specs/auth");
  await page.getByTestId("exception").click();
  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("refresh token when access is expired", async({goto, page}) => {
  await page.route("*/**/authentication-samples/login", async route => {
    const json = {
      access: expiredAccessToken,
      refresh: refreshToken
    };
    await route.fulfill({ json });
  });
  await goto("/specs/auth", { waitUntil: "hydration" });
  const form = page.locator("form");
  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  const responsePromise = page.waitForResponse(resp => {
    return resp.url().includes("refresh");
  });
  await form.locator(".p-button").click();

  const response = await responsePromise;
  expect(response.url()).toContain("refresh");
  await expect(page).toHaveURL("/specs/auth");
});

async function login(form) {
  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  await form.locator(".p-button").click();
}