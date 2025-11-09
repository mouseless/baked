import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/route-parameters", { waitUntil: "hydration" });
});

test.describe("Base", () =>{

  test("parse route and find correct page descriptor", async({ page }) => {
    const component = page.getByTestId("test");

    await component.locator(primevue.button.base).click();
    await page.waitForLoadState("networkidle");

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
  });

});
