import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "~/utils/locators/baked.js";
import mockMe from "~/utils/mockMe";
import giveMe from "~/utils/giveMe";

test.beforeEach(async({goto, page}) => {
  await goto("/specs", { waitUntil: "hydration" });
  const token = giveMe.aToken();
  await mockMe.theSession(page, token);
  await goto("/specs/error-page", { waitUntil: "hydration" });
});

test.describe("Base", () =>{
  const id = "Base";

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

  test("safe links message from schema", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.errorPage.message).last()).toHaveText("Safe links message");
  });

  test("links from schema safe links", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("LINK_1")).toHaveText("VALUE_1");
    await expect(component.getByTestId("LINK_2")).toHaveText("VALUE_2");
  });

  test("links require authenticated user", async({goto, page}) => {
    await mockMe.theSession(page, { });
    await goto("/specs/error-page", { waitUntil: "hydration" });
    const component = page.getByTestId(id);

    await expect(component.getByText("VALUE_1")).toHaveCount(0);
    await expect(component.getByText("VALUE_2")).toHaveCount(0);
  });

  test("footer info from schema", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.errorPage.footer)).toHaveText("Footer info");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
