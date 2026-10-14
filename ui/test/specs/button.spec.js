import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked.js";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/button", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.icon)).toHaveClass(/pi-play-circle/);
  });

  test("label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveText("Button");
  });

  test("loading", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(button).toHaveClass(/p-button-loading/);
  });

  test("Execute action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Execute Action");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Small", () => {
  const id = "Small";

  test("size ", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveClass(/b--small/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Large", () => {
  const id = "Large";

  test("size", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveClass(/b--large/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe.skip("Dialog", () => {
  const id = "Dialog";

  test("submit button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.footer)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base)).toBeAttached();
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base)).toHaveText("Submit");
  });

  test("execute action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const dialogAction = dialog.locator(primevue.dialog.footer).locator(primevue.button.base);

    await button.click();
    await dialogAction.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Execute Action");
    await expect(dialog).not.toBeAttached();
  });

  test("does not execute action when closed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const dialogClose = dialog.locator(primevue.dialog.close);

    await button.click();
    await dialogClose.click();

    await expect(page.locator(primevue.toast.base)).not.toBeVisible();
  });

  test.fixme("dismiss", () => { });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe.skip("Dialog Form", () => {
  const id = "Dialog Form";

  test("open button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Simple Form");
  });

  test("header", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.header)).toHaveText("Simple Form");
  });

  test("message", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.content)).toHaveText(/Message/);
  });

  test("input", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.content).getByTestId("input")).toBeAttached();
  });

  test("buttons", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(0)).toHaveText("Cancel");
    await expect(dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1)).toHaveText("Submit");
  });

  test("validation tooltip", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const footerButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1);
    const tooltip = page.locator(".p-tooltip");

    await button.click();
    await footerButton.hover();

    await expect(tooltip.locator(".p-tooltip-text")).toContainText("- Input cannot be empty");
  });

  test("executes action after close", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submitButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1);
    const text = dialog.locator(primevue.dialog.content).getByTestId("input");

    await button.click();
    await text.fill("text");
    await submitButton.click();

    await expect(dialog).not.toBeAttached();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
  });

  test("does not execute action when closed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const dialogClose = dialog.locator(primevue.dialog.close);
    const cancelButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(0);

    await button.click();
    await dialogClose.click();

    await expect(page.locator(primevue.toast.base)).not.toBeVisible();

    await button.click();
    await cancelButton.click();

    await expect(page.locator(primevue.toast.base)).not.toBeVisible();
  });

  test.fixme("prevents dismiss", () => { });

  test("visual with transported submit", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe.skip("Dialog Form Error", () => {
  const id = "Dialog Form Error";

  test("shows error using inline error in a popover", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submitButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1);
    const popover = page.locator(primevue.popover.base);

    await button.click();
    await submitButton.click();

    await expect(popover).toBeAttached();
    await expect(popover.locator(baked.message.base)).toHaveClass(/b--error/);
    await expect(popover.locator(baked.message.base)).toHaveClass(/message-error/);
    await expect(popover.locator(baked.message.base)).toHaveText(/Test Service Handled/);
    await expect(popover.locator(baked.message.base)).toHaveText(/A handled exception was thrown/);
  });

  test("visual without cancel", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});