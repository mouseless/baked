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

test.describe("Mutable", () => {
  const id = "Mutable";

  test("enable submit button when required input is valid", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("input-1");
    const input2 = component.getByTestId("input-2");
    const button = component.locator(primevue.button.base);

    await input1.fill("its not restricted");
    await input2.fill("required");

    await expect(button).toBeEnabled();
  });

  test("disable submit button when mutableValidation is not valid", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("input-1");
    const input2 = component.getByTestId("input-2");
    const button = component.locator(primevue.button.base);

    await input1.fill("error");
    await input2.fill("required");

    await expect(button).toBeDisabled();
  });

  test("show validation result on submit button with tooltip", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("input-1");
    const button = component.locator(primevue.button.base);

    await input1.fill("error");
    await button.scrollIntoViewIfNeeded();
    await button.hover();

    await expect(button).toBeDisabled();
    await expect(page.locator(primevue.tooltip.top)).toBeVisible();
    await expect(page.locator(primevue.tooltip.top)).toContainText("error is restricted");
  });
});

test.describe("Nested Validation", () => {
  const id = "Nested Validation";

  test("child validation hides itself in nested validations", async({ page }) => {
    const component = page.getByTestId(id);
    const inputGroup = component.locator(primevue.inputGroup.base).first();

    await expect(inputGroup.locator(".b-Validation")).not.toBeAttached();
  });
});