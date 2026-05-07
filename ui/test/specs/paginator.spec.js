import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/paginator", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("displays a component with previous next and display", async({ page }) => {
    const component = page.getByTestId(id);
    const previous = component.locator(primevue.button.base).nth(0);
    const next = component.locator(primevue.button.base).nth(1);
    const display = component.locator("span").nth(0);

    await expect(previous).toBeAttached();
    await expect(previous.locator("span")).toContainClass("pi pi-chevron-left");
    await expect(previous).toBeDisabled();
    await expect(next).toBeAttached();
    await expect(next.locator("span")).toContainClass("pi pi-chevron-right");
    await expect(next).toBeDisabled();
    await expect(display).toHaveText("Page NaN");
  });

  test("next is enabled when length is equal or greater than length", async({ page }) => {
    const component = page.getByTestId(id);
    const next = component.locator(primevue.button.base).nth(1);
    const length = page.getByTestId("length");
    const take = page.getByTestId("take");

    await length.click();
    await page.keyboard.press("Digit5");
    await take.click();
    await page.keyboard.press("Digit5");

    await expect(component.locator("span").nth(0)).toHaveText("Page 1");
    await expect(next).toBeEnabled();
  });

  test("next is disabled when length is less than take", async({ page }) => {
    const component = page.getByTestId(id);
    const next = component.locator(primevue.button.base).nth(1);
    const length = page.getByTestId("length");
    const take = page.getByTestId("take");

    await length.click();
    await page.keyboard.press("Digit1");
    await take.click();
    await page.keyboard.press("Digit2");

    await expect(next).toBeDisabled();
  });

  test("next increases page and previous enabled when page is greater than 1", async({ page }) => {
    const component = page.getByTestId(id);
    const next = component.locator(primevue.button.base).nth(1);
    const previous = component.locator(primevue.button.base).nth(0);
    const length = page.getByTestId("length");
    const take = page.getByTestId("take");

    await length.click();
    await page.keyboard.press("Digit5");
    await take.click();
    await page.keyboard.press("Digit5");

    await next.click();

    await expect(component.locator("span").nth(0)).toHaveText("Page 2");
    await expect(previous).toBeEnabled();
  });

  test("model value is page minus one times take", async({ page }) => {
    const component = page.getByTestId(id);
    const next = component.locator(primevue.button.base).nth(1);
    const length = page.getByTestId("length");
    const take = page.getByTestId("take");
    const model = page.getByTestId(`${id}:model`);

    await length.click();
    await page.keyboard.press("Digit5");
    await take.click();
    await page.keyboard.press("Digit5");

    await expect(model).toHaveText("0");

    await next.click();

    await expect(model).toHaveText("5");

    await next.click();

    await expect(model).toHaveText("10");
  });
});