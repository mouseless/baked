import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto, page }) => {
  await page.route("*/**/exception-samples/handled", async route => {
    await route.fulfill({
      status: 400,
      json: {
        title: "Test Service Handled",
        detail: "A handled exception was thrown"
      }
    });
  });

  await goto("/specs/simple-form", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);
    const title = component.locator("h2");

    await expect(title).toBeAttached();
    await expect(title).toHaveText("Simple Form");
  });

  test("inputs", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("input")).toBeAttached();
  });

  test("button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Submit");
  });

  test("button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base);

    await input.fill("");

    await expect(button).toBeDisabled();
  });

  test("button shows validation tooltip when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base);
    const tooltip = page.locator(".p-tooltip");

    await input.fill("");
    await button.hover();

    await expect(tooltip.locator(".p-tooltip-text")).toContainText("- Input cannot be empty");
  });

  test("button is enabled when inputs are ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).not.toBeDisabled();
  });

  test("action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("default");
  });

  test("button is disabled until action is completed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(button).toBeDisabled();
    const spinner = button.locator(".p-button-loading-icon, .pi-spinner");
    await expect(spinner).toBeVisible();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(button).not.toBeDisabled();
  });

  test("inputs are in form mode", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");

    await input.fill("");

    await expect(input).toHaveValue("");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Error", () => {
  const id = "Error";

  test("shows error using inline error in a popover", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const popover = page.locator(primevue.popover.base);

    await button.click();

    await expect(popover).toBeAttached();
    await expect(popover.locator(baked.message.base)).toHaveClass(/b--error/);
    await expect(popover.locator(baked.message.base)).toHaveClass(/message-error/);
    await expect(popover.locator(baked.message.base)).toHaveText(/Test Service Handled/);
    await expect(popover.locator(baked.message.base)).toHaveText(/A handled exception was thrown/);
  });
});

test.describe("Horizontal", () => {
  const id = "Horizontal";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Multiple Inputs", () => {
  const id = "Multiple Inputs";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Validation", () => {
  const id = "Validation";

  test("show a red border on required fields when the user leaves without entry", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("input-1");
    const input2 = component.getByTestId("input-2");

    await input1.focus();
    await input2.focus();

    await expect(input1).toContainClass("p-invalid");
  });
});