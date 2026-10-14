import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked.js";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ page, goto }) => {
  await page.route("*/**/exception-samples/handled", async route => {
    await route.fulfill({
      status: 400,
      json: {
        title: "Test Service Handled",
        detail: "A handled exception was thrown"
      }
    });
  });

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

test.describe("Dialog", () => {
  const id = "Dialog";

  test("header", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog.locator(primevue.dialog.header)).toHaveText("Header");
  });

  test("content", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const content = dialog.getByTestId("content");

    await button.click();

    await expect(content).toBeAttached();
    await expect(content).toHaveText("Message");
  });

  test("buttons", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const footer = dialog.locator(primevue.dialog.footer);
    const cancel = footer.locator(primevue.button.base).nth(0);
    const submit = footer.locator(primevue.button.base).nth(1);

    await button.click();

    await expect(footer).toBeAttached();
    await expect(cancel).toBeAttached();
    await expect(cancel).toHaveText("Cancel");
    await expect(submit).toBeAttached();
    await expect(submit).toHaveText("Submit");
  });

  test("execute action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const footer = dialog.locator(primevue.dialog.footer);
    const submit = footer.locator(primevue.button.base).nth(1);

    await button.click();
    await submit.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Confirmed!");
    await expect(dialog).not.toBeAttached();
  });

  test("no action when closed", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const close = dialog.locator(primevue.dialog.close);

    await button.click();
    await close.click();

    await expect(dialog).not.toBeAttached();
    await expect(page.locator(primevue.toast.base)).not.toBeVisible();
  });

  test("dismiss", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();
    await page.mouse.click(1, 1);

    await expect(dialog).not.toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();

    await expect(dialog).toBeVisible();
    await expect(dialog).toHaveScreenshot();
  });
});

test.describe("Dialog Form", () => {
  const id = "Dialog Form";

  test("open button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await expect(button).toBeAttached();
    await expect(button).toHaveText("Button");
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

  test("executes action before close", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submitButton = dialog.locator(primevue.dialog.footer).locator(primevue.button.base).nth(1);
    const text = dialog.locator(primevue.dialog.content).getByTestId("input");

    await button.click();
    await text.fill("text");
    await submitButton.click();

    await expect(dialog).toBeVisible();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(dialog).not.toBeAttached();
  });

  test("no action when closed", async({ page }) => {
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

  test("prevents dismiss", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);

    await button.click();
    await page.mouse.click(1, 1);
    await page.waitForTimeout(500); // make sure enough time pass for dialog to close, otherwise it is a false-positive

    await expect(dialog).toBeAttached();
  });

  test("visual with transported submit", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submit = dialog.locator(primevue.button.base).nth(1);

    await button.click();

    await expect(dialog).toBeVisible();
    await expect(submit).toBeVisible();
    await expect(dialog).toHaveScreenshot();
  });
});

test.describe("Dialog Form Error", () => {
  const id = "Dialog Form Error";

  test("shows error using inline error in a popover", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const footer = dialog.locator(primevue.dialog.footer);
    const submit = footer.locator(primevue.button.base).nth(0);
    const popover = page.locator(primevue.popover.base);
    const message = popover.locator(baked.message.base);

    await button.click();
    await submit.click();

    await expect(popover).toBeAttached();
    await expect(message).toHaveClass(/b--error/);
    await expect(message).toHaveClass(/message-error/);
    await expect(message).toHaveText(/Test Service Handled/);
    await expect(message).toHaveText(/A handled exception was thrown/);
  });

  test("visual without cancel", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);
    const dialog = page.locator(primevue.dialog.base);
    const submit = dialog.locator(primevue.button.base).nth(1);

    await button.click();

    await expect(dialog).toBeVisible();
    await expect(submit).toBeVisible();
    await expect(dialog).toHaveScreenshot();
  });
});