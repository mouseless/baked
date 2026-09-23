import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-textarea", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("label", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("initial value", async({ page }) => {
    const component = page.getByTestId(id);
    const textarea = component.locator(primevue.inputTextarea.base);

    await expect(textarea).toHaveValue("initial value");
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