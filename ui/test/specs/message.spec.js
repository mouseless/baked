import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/message", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("Base", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.body)).toHaveText("Message");
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

test.describe("Severity", () => {
  [
    { id: "Info", expected: "rgb(37, 99, 235)" },
    { id: "Warning", expected:  "rgb(202, 138, 4)" },
    { id: "Error", expected: "rgb(220, 38, 38)" },
    { id: "Success", expected: "rgb(22, 163, 74)" },
    { id: "Contrast", expected: "rgb(2, 6, 23)" },
    { id: "Secondary", expected: "rgb(100, 116, 139)" }
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