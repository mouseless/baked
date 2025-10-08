import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue.js";
import baked from "~/utils/locators/baked.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/string", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("display full text", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("This is a text");
  });

  test("tooltip is disabled", async({page}) => {
    const component = page.getByTestId(id);

    await component.locator(baked.string.text).hover();
    await expect(page.locator(primevue.tooltip.bottom)).not.toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Max Length", () => {
  const id = "Max Length";

  test("truncate string with elipsis", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("This text sho...");
  });

  test("show tool tip", async({page}) => {
    const component = page.getByTestId(id);

    await component.locator(baked.string.text).hover();

    await expect(page.locator(primevue.tooltip.bottom)).toBeAttached();
    await expect(page.locator(primevue.tooltip.bottom)).toBeVisible();
    await expect(page.locator(primevue.tooltip.bottom)).toHaveText("This text should be truncated with elipsis when exceeds max length");
  });
});


test.describe("No Data", () => {
  const id = "No Data";

  test("display single dash(-)", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.string.text)).toHaveText("-");
  });

  test("tooltip is disabled", async({page}) => {
    const component = page.getByTestId(id);

    await component.locator(baked.string.text).hover();
    await expect(page.locator(primevue.tooltip.bottom)).not.toBeAttached();
  });
});