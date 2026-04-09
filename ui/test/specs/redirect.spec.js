import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/redirect", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("redirects to given route", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page).toHaveURL("/page/with/route/pageWithRoute");
  });
});

test.describe("Conditional", () => {
  const id = "Conditional";

  test("does not redirect to given route when condition is not satisfied", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);

    await input.fill("something else");

    await expect(page).not.toHaveURL("/page/with/route/pageWithRoute");
  });

  test("redirects to given route when condition is satisfied", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);

    await input.fill("redirect");

    await expect(page).toHaveURL("/page/with/route/pageWithRoute");
  });
});