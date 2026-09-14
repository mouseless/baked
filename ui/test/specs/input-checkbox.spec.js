import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-checkbox", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("initial value", async({ page }) => {
    const component = page.getByTestId(id);
    const checkbox = component.locator(primevue.checkbox.base);

    await expect(checkbox).toHaveAttribute("data-p-checked", "true");
  });
});

test.describe("Indeterminate", () => {
  const id = "Indeterminate";

  test("has indeterminate flag in the checkbox", async({ page }) => {
    const component = page.getByTestId(id);
    const checkbox = component.locator(primevue.checkbox.base);

    await expect(checkbox).toHaveAttribute("data-p-indeterminate", "true");
  });

  test("check indeterminate changes", async({ page }) => {
    const component = page.getByTestId(id);
    const checkbox = component.locator(primevue.checkbox.base);

    // initial value is undefined
    await checkbox.click(); // set true
    await checkbox.click(); // set false
    await checkbox.click(); // set undefined

    await expect(checkbox).toHaveAttribute("data-p-indeterminate", "true");
  });
});

test.describe("Validation", () => {
  const id = "Validation";

  test("component shows the message component under the component", async({ page }) => {
    const component = page.getByTestId(id);
    const message = component.locator(baked.message.base);

    await expect(message).toHaveText("this is an error message");
  });
});
