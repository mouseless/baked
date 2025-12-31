import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.describe("Base", () => {
  test("parse route and find correct page descriptor", async({ goto, page }) => {
    await goto("/page/with/route/pageWithRoute", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
    await expect(page.locator(primevue.panel.title)).toHaveText("Page with route");
  });

  test("parse dynamic route and extract route parameters", async({ goto, page }) => {
    await goto("/first/1", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
    await expect(page.getByTestId("params")).toHaveText("{ \"id\": \"1\" }");
  });

  test("parse nested dynamic route and extract route parameters", async({ goto, page }) => {
    await goto("/first/1/second/2", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
    await expect(page.getByTestId("params")).toHaveText("{ \"firstId\": \"1\", \"secondId\": \"2\" }");
  });
});
