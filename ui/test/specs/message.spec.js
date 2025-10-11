import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/message", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("Base", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.message.content)).toHaveText("Message");
    await expect(component.locator(primevue.message.icon)).toHaveClass("pi pi-info-circle");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("No icon", () => {
  const id = "No icon";

  test("Does not attach icon element", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.message.icon)).not.toBeAttached();
  });
});

test.describe("No data", () => {
  const id = "No data";

  test("Display single dash(-) when data is null", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.message.content)).toHaveText("-");
  });
});

test.describe("Severity", () => {
  test("Info", async({ page }) => {
    const component = page.getByTestId("Info");
    const color = await component.evaluate(element =>
      globalThis.getComputedStyle(element).getPropertyValue("--p-message-info-color")
    );

    await expect(component.locator(primevue.message.base)).toHaveCSS("color", hexToRGB(color));
  });

  test("Warning", async({ page }) => {
    const component = page.getByTestId("Warning");
    const color = await component.evaluate(element =>
      globalThis.getComputedStyle(element).getPropertyValue("--p-message-warn-color")
    );

    await expect(component.locator(primevue.message.base)).toHaveCSS("color", hexToRGB(color));
  });

  test("Error", async({ page }) => {
    const component = page.getByTestId("Error");
    const color = await component.evaluate(element =>
      globalThis.getComputedStyle(element).getPropertyValue("--p-message-error-color")
    );

    await expect(component.locator(primevue.message.base)).toHaveCSS("color", hexToRGB(color));
  });
});

function hexToRGB(hex) {
  return `rgb(${hex.replace("#","").match(/.{1,2}/g).map(e=>parseInt(e, 16)).join(", ")})`;
}
