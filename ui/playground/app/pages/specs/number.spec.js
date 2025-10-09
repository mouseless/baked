import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "~/utils/locators/baked.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/number", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    const numbers = component.locator(baked.number.base);
    await expect(numbers.nth(0)).toHaveText("1,499");
    await expect(numbers.nth(1)).toHaveText("-1,499");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Decimal Digits", () => {
  const id = "Decimal Digits";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    const numbers = component.locator(baked.number.base);
    await expect(numbers.nth(0)).toHaveText("999.99");
    await expect(numbers.nth(1)).toHaveText("-999.99");
  });
});

test.describe("Fractionless Trailing Zeros", () => {
  const id = "Fractionless Trailing Zeros";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    const numbers = component.locator(baked.number.base);
    await expect(numbers.nth(0)).toHaveText("200");
    await expect(numbers.nth(1)).toHaveText("-200");
  });
});

test.describe("Millions", () => {
  const id = "Millions";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    const numbers = component.locator(baked.number.base);
    await expect(numbers.nth(0)).toHaveText("1.5M");
    await expect(numbers.nth(1)).toHaveText("-1.5M");
  });
});

test.describe("Billions", () => {
  const id = "Billions";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    const numbers = component.locator(baked.number.base);
    await expect(numbers.nth(0)).toHaveText("1.5B");
    await expect(numbers.nth(1)).toHaveText("-1.5B");
  });
});
