import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";
import baked from "../utils/locators/baked.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/text", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("display full text", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("This is a text");
  });

  test("tooltip is disabled", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(baked.string.text);

    await text.scrollIntoViewIfNeeded();
    await text.hover();

    await expect(page.locator(primevue.tooltip.base)).not.toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Max Length", () => {
  const id = "Max Length";

  test("truncate string with elipsis", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("This text sho...");
  });

  test("show tooltip", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(baked.string.text);

    await text.scrollIntoViewIfNeeded();
    await text.hover();

    await expect(page.locator(primevue.tooltip.base)).toBeAttached();
    await expect(page.locator(primevue.tooltip.base)).toBeVisible();
    await expect(page.locator(primevue.tooltip.base)).toHaveText("This text should be truncated with elipsis when exceeds max length");
  });
});

test.describe("No Data", () => {
  const id = "No Data";

  test("display single dash(-)", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("-");
  });

  test("tooltip is disabled", async({ page }) => {
    const component = page.getByTestId(id);
    const text = component.locator(baked.string.text);

    await text.scrollIntoViewIfNeeded();
    await text.hover();

    await expect(page.locator(primevue.tooltip.base)).not.toBeAttached();
  });
});

test.describe("Prop", () => {
  const id = "Prop";

  test("display text from object prop", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("Text from object");
  });
});