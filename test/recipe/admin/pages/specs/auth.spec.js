import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/auth", { waitUntil: "hydration" });
});

test("redirect to login", async({page}) => {
  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("show toast for 400 errors when at login page", async({page}) => {
  await page.route("*/**/authentication-samples/login", async route => await route.fulfill({
    status: 400,
    response:{
      title: "Bad Request",
      detail: "Invalid credentials"
    }
  }));
  const form = page.locator("form");

  await form.locator(".p-button").click();

  await expect(page.locator(primevue.toast.base)).toBeVisible();
});


test("view authorized page", async({page}) => {
  await page.route("*/**/authentication-samples/login", async route => {
    const json = {
      access: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJVc2VyIjoiVXNlciJ9.du9y0K1tvI8BDiBiERa-QeKgOpot1GF8melbgs7qpOk",
      refresh: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJSZWZyZXNoIjoiUmVmcmVzaCJ9.DKVsDLHVct-qopfULCpbDOfvjYHY4iQcQ6EZ6P77Olw"
    };
    await route.fulfill({ json });
  });
  const form = page.locator("form");

  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  await form.locator(".p-button").click();

  await expect(page).toHaveURL("/specs/auth");
});

test("logout redirects to login", async({page}) => {
  await page.route("*/**/authentication-samples/login", async route => {
    const json = {
      access: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJVc2VyIjoiVXNlciJ9.du9y0K1tvI8BDiBiERa-QeKgOpot1GF8melbgs7qpOk",
      refresh: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJSZWZyZXNoIjoiUmVmcmVzaCJ9.DKVsDLHVct-qopfULCpbDOfvjYHY4iQcQ6EZ6P77Olw"
    };
    await route.fulfill({ json });
  });
  const form = page.locator("form");
  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  await form.locator(".p-button").click();

  await expect(page).toHaveURL("/specs/auth");
  await page.locator(".p-panel .p-button").click();
  await expect(page).toHaveURL("/login");
});