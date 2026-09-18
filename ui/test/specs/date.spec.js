import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/date", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("initial data", async({ page }) => {
    const component = page.getByTestId(id);
    const content = component.locator(baked.date.base);

    await expect(content).toHaveText("2026-01-15");
  });
});

test.describe("Format Positioning", () => {
  const id = "Format Positioning";

  test("format with the month, days and year positioning", async({ page }) => {
    const component = page.getByTestId(id);
    const dates = component.locator(baked.date.base);

    await expect(dates.nth(0)).toHaveText("01-15-2026");
    await expect(dates.nth(1)).toHaveText("15-01-2026");
    await expect(dates.nth(2)).toHaveText("2026-01-15");
  });
});

test.describe("Format divide char", () => {
  const id = "Format divide char";

  test("format with the month, days and year dividing with giving format", async({ page }) => {
    const component = page.getByTestId(id);
    const dates = component.locator(baked.date.base);

    await expect(dates.nth(0)).toHaveText("15.01.2026");
    await expect(dates.nth(1)).toHaveText("15/01/2026");
  });
});

test.describe("Prop", () => {
  const id = "Prop";

  test("display date from object prop", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.date.base)).toHaveText("2026-01-15");
  });
});
