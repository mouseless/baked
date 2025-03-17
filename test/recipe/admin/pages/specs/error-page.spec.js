import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "~/utils/locators/baked.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/error-page", { waitUntil: "hydration" });
});

test.describe("Basic", () =>{
  const id = "Basic";

  test("error status code as tag", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.errorPage.tag)).toHaveText("403");
  });

  test("title from schema error infos", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.errorPage.title)).toHaveText("Access Denied");
  });

  test("message from schema error infos", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.errorPage.message).first()).toHaveText("You do not have the permision to view the address or data specified.");
  });

  test("links from schema safe links", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("LINK_1")).toHaveText("VALUE_1");
    await expect(component.getByTestId("LINK_2")).toHaveText("VALUE_2");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
