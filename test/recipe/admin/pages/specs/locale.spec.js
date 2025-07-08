import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/locale", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const datepickerTestId = "test-datepicker";
  const localesTestId = "test-locales";
  const localeTestId = "test-locale";

  test("locale is applied", async({ page }) => {
    const component = page.getByTestId(datepickerTestId);
    const weekdays = component.locator(primevue.datepicker.weekday);

    await expect(weekdays.nth(0)).toHaveText("Su");
    await expect(weekdays.nth(1)).toHaveText("Mo");
    await expect(weekdays.nth(2)).toHaveText("Tu");
    await expect(weekdays.nth(3)).toHaveText("We");
    await expect(weekdays.nth(4)).toHaveText("Th");
    await expect(weekdays.nth(5)).toHaveText("Fr");
    await expect(weekdays.nth(6)).toHaveText("Sa");
  });

  test("locales showed", async({ page }) => {
    const component = page.getByTestId(localesTestId);

    await expect(component).toHaveText("EN, TR");
  });

  test("locale showed", async({ page }) => {
    const component = page.getByTestId(localeTestId);

    await expect(component).toHaveText("EN");
  });
});
