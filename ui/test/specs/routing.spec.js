import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.describe("Base", () => {
  test("parse route and find correct page descriptor", async({ goto, page }) => {
    await goto("/page/with/route/pageWithRoute", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
    await expect(page.getByTestId(primevue.panel.title)).toHaveText("Page with route");
  });

  test("parse dynamic route and extract route parameters", async({ goto, page }) => {
    await goto("/parent/a6db0515-a885-4cf6-8343-c2c4a0510392", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
    await expect(page.getByTestId("params")).toHaveText("{ \"id\": \"a6db0515-a885-4cf6-8343-c2c4a0510392\" }");
  });

  test("parse nested dynamic route and extract route parameters", async({ goto, page }) => {
    await goto("/parent/1/children/2", { waitUntil: "hydration" });

    await expect(page.getByText(404).nth(0)).not.toBeAttached();
    await expect(page.getByText("Application Error").nth(0)).not.toBeAttached();
    await expect(page.getByTestId("params")).toHaveText("{ \"parentId\": \"1\", \"childId\": \"2\" }");
  });
});
