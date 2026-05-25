import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/message", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("Base", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.content)).toHaveText("Message");
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

    await expect(component.locator(baked.message.content)).toHaveText("-");
  });
});

test.describe("Severity", () => {
  test("Info", async({ page }) => {
    const component = page.getByTestId("Info");
    const color = "rgb(37, 99, 235)";

    await expect(component.locator(baked.message.base)).toHaveCSS("color", color);
  });

  test("Warning", async({ page }) => {
    const component = page.getByTestId("Warning");
    const color = "rgb(202, 138, 4)";

    await expect(component.locator(baked.message.base)).toHaveCSS("color", color);
  });

  test("Error", async({ page }) => {
    const component = page.getByTestId("Error");
    const color = "rgb(220, 38, 38)";

    await expect(component.locator(baked.message.base)).toHaveCSS("color", color);
  });
});