import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto, page }) => {
  await page.route("*/**/route-parameters-samples/*/items", async route => {
    const url = new URL(route.request().url());
    const parts = url.pathname.split("/");
    const idParameter = parts[parts.indexOf("route-parameters-samples") + 1];

    const json = [{ id: 1, value: `${idParameter}-1` }];
    await route.fulfill({ json });
  });

  await goto("/specs/route-parameters", { waitUntil: "hydration" });
});

test.describe("Base", () =>{

  test("parse route and find correct page descriptor", async({ page }) => {
    const component = page.getByTestId("test");

    await component.locator(primevue.button.base).click();
    await page.waitForLoadState("networkidle");

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
  });

  test("provide route parameters", async({ page }) => {
    const component = page.getByTestId("test");

    await component.locator(primevue.button.base).click();
    await page.waitForLoadState("networkidle");

    const cells = page.locator("td");
    const cellNo = (x, y) => (x - 1) * 5 + y - 1;

    await expect(cells.nth(cellNo(1, 1))).toHaveText("42-1");
  });

});
