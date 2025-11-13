import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ page }) => {
  await page.route("*/**/route-parameters-samples/*/items", async route => {
    const url = new URL(route.request().url());
    const parts = url.pathname.split("/");
    const idParameter = parts[parts.indexOf("route-parameters-samples") + 1];

    const json = [{ id: 1, value: `${idParameter}-1` }];
    await route.fulfill({ json });
  });
});

test.describe("Base", () =>{

  test("parse route and find correct page descriptor", async({ goto, page }) => {
    await goto("/route-parameters-sample/42", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
  });

  test("uses provided route parameters when fetching data", async({ goto, page }) => {
    await goto("/route-parameters-sample/42", { waitUntil: "hydration" });

    const cells = page.locator("td");
    const cellNo = (x, y) => (x - 1) * 5 + y - 1;

    await expect(cells.nth(cellNo(1, 1))).toHaveText("42-1");
  });

});
