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

test.describe("Dynamic", () => {
  const id = "Dynamic";

  test("redirects to given dynamic route", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(0);

    await button.click();

    await expect(page).toHaveURL("/page/with/route/42");
  });

  test("redirects to the given dynamic route without the route parameter in the query", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(1);

    await button.click();

    await expect(page).toHaveURL("/page/with/route/42?queryTest=true");
  });
});

test.describe("Query", () => {
  const id = "Query";

  test("redirects with query parameters", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(0);

    await button.click();

    await expect(page).toHaveURL("/page/with/route?query1=true&query2=true");
  });

  test("redirects with only included query parameters", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(1);

    await button.click();

    await expect(page).toHaveURL("/page/with/route?included=true");
  });

  test("redirects without excluded query parameters", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(2);

    await button.click();

    await expect(page).toHaveURL("/page/with/route?excluded=true");
  });

  test("null values filter out in query", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(3);

    await button.click();

    await expect(page).toHaveURL("/page/with/route");
  });
});