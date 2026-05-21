import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/form-validation", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeDisabled();
  });

  test("show a red border on required fields when the user leaves without entry", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.locator(primevue.inputText.base).first();
    const input2 = component.locator(primevue.inputText.base).nth(1);

    await input1.focus();
    await input2.focus();

    await expect(input1).toContainClass("p-invalid");
  });

  test("show optionality label when input is optional", async({ page }) => {
    const component = page.getByTestId(id);
    const label = component.locator(primevue.floatLabel.base).nth(1);

    await expect(label).toHaveText("Test Label (Optional)");
  });
});