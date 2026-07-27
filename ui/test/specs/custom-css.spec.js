import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "../utils/giveMe";

test.beforeEach(async({ goto }) => {
  await goto("/specs/custom-css", { waitUntil: "hydration" });
});

const id = "test";

test("custom css hides", async({ page }) => {
  const component = page.getByTestId(id);

  await expect(component.locator(".custom-css-visible")).toBeVisible();
});

test("custom css shows", async({ page }) => {
  const component = page.getByTestId(id);

  await expect(component.locator(".custom-css-hidden")).not.toBeVisible();
});

test("styles from the baked theme reference are used in custom css", async({ page }) => {
  const component = page.getByTestId(id);
  const screen = giveMe.aScreenSize({ name: "2xs" });

  await page.setViewportSize({ ...screen });

  await expect(component.locator(".screen-css-visible")).toBeVisible();
});
