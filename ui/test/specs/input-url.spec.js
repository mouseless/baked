import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-url", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("placeholder", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.floatLabel.base)).toHaveText("Label");
  });

  test("initial value", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(baked.inputUrl.base);

    await expect(input).toHaveValue("https://baked.mouseless.codes");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("SetModel", () => {
  const id = "SetModel";

  test("invalid url shows message", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const error = component.locator(baked.message.base);

    await input.fill("not a url");

    await expect(error).toHaveText("Invalid URL");
  });

  test("valid url sets model with the given value", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("mouseless.org");

    await expect(model).toHaveText("https://mouseless.org");
  });
});

test.describe("Validation", () => {
  const id = "Validation";

  test("component shows the message component under the input url", async({ page }) => {
    const component = page.getByTestId(id);
    const message = component.locator(baked.message.base);

    await expect(message).toHaveText("this is an error message");
  });
});
