import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "~/utils/locators/baked.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/error-page", { waitUntil: "hydration" });
});

const id = "error-page";

test("error status code as tag", async({page}) => {
  const errorPage = page.getByTestId(id);

  await expect(errorPage.locator(baked.errorPage.tag)).toHaveText("403");
});

test("title from schema error infos", async({page}) => {
  const errorPage = page.getByTestId(id);

  await expect(errorPage.locator(baked.errorPage.title)).toHaveText("Access Denied");
});

test("message from schema error infos", async({page}) => {
  const errorPage = page.getByTestId(id);

  const message = await errorPage.locator(baked.errorPage.message).first();
  expect(message).toHaveText("You do not have the permision to view the address or data specified.");
});

test("links from schema safe links", async({page}) => {
  const errorPage = page.getByTestId(id);

  await expect(errorPage.locator(baked.errorPage.links)).toBeVisible();

  const cardlinks = await errorPage.locator(baked.errorPage.links).locator("a").all();
  expect(cardlinks).toHaveLength(2);
  for(const link of cardlinks)
    await expect(link).toBeVisible();
});

