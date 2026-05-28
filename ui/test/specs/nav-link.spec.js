import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto, page }) => {
  await page.route("*/**/exception-samples/handled", async route => {
    await route.fulfill({
      status: 400,
      json: {
        title: "Test Service Handled",
        detail: "A handled exception was thrown"
      }
    });
  });

  await goto("/specs/nav-link", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base).locator(primevue.button.icon)).toHaveClass(/pi-eye/);
  });

  test("address", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveAttribute("href", "/specs");
  });

  test("text", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("Link");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Dynamic", () => {
  const id = "Dynamic";

  test("address", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveAttribute("href", "/test-path/test-id?query=value");
  });
});

test.describe("Inline Error", () => {
  const id = "Inline Error";

  test("inline error", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("Test Service Handled");
  });
});