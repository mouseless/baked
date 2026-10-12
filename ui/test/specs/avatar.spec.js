import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/avatar", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("display label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.avatar.base)).toHaveText("TS");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Large", () => {
  const id = "Large";

  test("size large with icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.avatar.icon)).toHaveClass(/pi pi-box/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("XLarge", () => {
  const id = "XLarge";

  test("size xlarge with icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.avatar.icon)).toHaveClass(/pi pi-box/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});