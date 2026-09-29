import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "../utils/giveMe";
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

  test("text", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("Dynamic");
  });

  test("address", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveAttribute("href", "/test-path/test-id?query=value");
  });
});

test.describe("Summary", () => {
  const id = "Summary";

  test("show summary in popover", async({ page }) => {
    const component = page.getByTestId(id);
    const link = component.locator(primevue.button.base);
    const popover = page.locator(primevue.popover.content).first();

    await link.hover();

    await expect(popover).toBeAttached();
    await expect(popover).toHaveText("NavLink summary content");
  });

  test("show summary in popover on mobile", async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "sm" });
    const icon = component.locator(primevue.icon.base);
    const popover = page.locator(primevue.popover.content).first();

    await page.setViewportSize({ ...screen });
    await expect(icon).toBeVisible();

    await icon.click();

    await expect(popover).toBeAttached();
    await expect(popover).toHaveText("NavLink summary content");
  });
});

test.describe("Max Length", () => {
  const id = "Max Length";

  test("truncate link label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("This is...");
  });
});

test.describe("Inline Error", () => {
  const id = "Inline Error";

  test("inline error", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("Test Service Handled");
  });
});