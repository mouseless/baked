import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-date", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("placeholder", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("initial value", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await expect(input).toHaveValue("15/01/2026");
    await expect(model).toHaveText("2026-01-15");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Use Picker", () => {
  const id = "Use Picker";

  test("renders date picker", async({ page }) => {
    const component = page.getByTestId(id);
    const calendar = page.locator(primevue.datepicker.calendar);
    component.click();

    await expect(calendar).toBeAttached();
  });

  test("sets the initial model value", async({ page }) => {
    const model = page.getByTestId(`${id}:model`);

    await expect(model).toHaveText("2026-01-15");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const calendar = page.locator(primevue.datepicker.calendar);
    component.click();

    await expect(calendar).toHaveScreenshot();
  });
});

test.describe("Format", () => {
  const id = "Format";

  test("uses the given format", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await expect(input).toHaveAttribute("placeholder", "__/__/____");
    await expect(input).toHaveValue("15.01.2026");
    await expect(model).toHaveText("2026-01-15");
  });
});

test.describe("Validation", () => {
  const id = "Validation";

  test("component shows the message component under the input date", async({ page }) => {
    const component = page.getByTestId(id);
    const message = component.locator(baked.message.base);

    await expect(message).toHaveText("this is an error message");
  });
});