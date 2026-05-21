import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

const consoleErrors = [];

test.beforeEach(async({ goto, page }) => {
  consoleErrors.length = 0;

  page.on("console", msg => {
    if(msg.type() === "error") {
      consoleErrors.push(msg.text());
    }
  });
  await goto("/specs/form-validation", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeDisabled();
  });

  test("missing composable should not break validation flow", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    expect(consoleErrors.some(e => e.includes("mustSkipThisComposables"))).toBe(true);
    await expect(button).toBeDisabled();
  });

  test("show a red border on required fields when the user leaves without entry", async({ page }) => {
    const component = page.getByTestId(id);
    const textInput = component.locator(primevue.inputText.base);
    const numberInput = component.locator(primevue.inputNumber.base);

    await textInput.focus();
    await numberInput.focus();

    await expect(textInput).toContainClass("p-invalid");
  });

  test.skip("show validated label when validateLabel is on", async() => {});
});