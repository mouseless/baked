import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/auth", { waitUntil: "hydration" });
});

test("redirect to login", async({page}) => {
  await expect(page).toHaveURL("login?redirect=/specs/auth");
});

test("view authorized page", async({page}) => {
  const form = page.locator("form");
  await page.route("*/**/authentication-samples/login", async route => {
    const json = {
      access: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJVc2VyIjoiVXNlciJ9.du9y0K1tvI8BDiBiERa-QeKgOpot1GF8melbgs7qpOk",
      refresh: "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJJc3N1ZXIiLCJpYXQiOjE3NDMwMTcwNDAsImV4cCI6NDA3ODIzNjI0MCwiYXVkIjoiQXVkaWVuY2UiLCJzdWIiOiIiLCJSZWZyZXNoIjoiUmVmcmVzaCJ9.DKVsDLHVct-qopfULCpbDOfvjYHY4iQcQ6EZ6P77Olw"
    };
    await route.fulfill({ json });
  });

  await form.getByPlaceholder("Username").fill("Username");
  await form.getByPlaceholder("Password").fill("Password");
  await form.locator(".p-button").click();

  await expect(page).toHaveURL("/specs/auth");
});