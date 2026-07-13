import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/message", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("Base", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.body)).toContainText("Message");
    await expect(component.locator(baked.message.body)).toContainText("This is a content slot for message");
    await expect(component.locator(baked.message.icon)).toHaveClass(/pi pi-info-circle/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("No icon", () => {
  const id = "No icon";

  test("does not attach icon element", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.icon)).not.toBeAttached();
  });
});

test.describe("No data", () => {
  const id = "No data";

  test("display single dash(-) when data is null", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.body)).toHaveText("-");
  });
});

test.describe("Action", () => {
  const id = "Action";

  test("displays action button", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.action).locator(primevue.button.base)).toBeAttached();
  });
});

test.describe("Severity", () => {
  [
    { id: "Info", expected: "oklch(0.546 0.245 262.881)" }, // blue-600
    { id: "Warning", expected: "oklch(0.681 0.162 75.834)" }, // yellow-600
    { id: "Error", expected: "oklch(0.577 0.245 27.325)" }, // red-600
    { id: "Success", expected: "oklch(0.627 0.194 149.214)" }, // green-600
    { id: "Contrast", expected: "oklch(0.129 0.042 264.695)" }, // slate-950
    { id: "Secondary", expected: "oklch(0.554 0.046 257.417)" } // slate-500
  ].forEach(({ id, expected }) => {
    test(`Severity ${id}`, async({ page }) => {
      const component = page.getByTestId(id);

      await expect(component.locator(baked.message.base)).toHaveCSS("color", expected);
    });
  });
});

test.describe("Simple Variant", () => {
  const id = "Simple Variant";

  test("simple variant class should be set correctly", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.base)).toContainClass("message-simple");
  });

  test("simple variant does not have any outline", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.base)).not.toContainClass("outline");
    await expect(component.locator(baked.message.base)).not.toContainClass("outline-1");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});