import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/check", { waitUntil: "hydration" });
});

test.describe("True", () => {
  const id = "True";

  test("shows check icon when data true", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.icon.base)).toHaveClass(/pi-check/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("False", () => {
  const id = "False";

  test("shows times icon when data false", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.icon.base)).toHaveClass(/pi-times/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Null", () => {
  const id = "Null";

  test("dont show the icon whenever data does not exist ", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.icon.base)).not.toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});