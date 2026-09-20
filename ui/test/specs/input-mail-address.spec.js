import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";
import baked from "../utils/locators/baked";

test.beforeEach(async({ goto }) => {
  await goto("/specs/input-mail-address", { waitUntil: "hydration" });
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

    await expect(input).toHaveValue("user@example.com");
  });
});

test.describe("SetModel", () => {
  const id = "SetModel";

  test("invalid e-mail address shows message", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const error = component.locator(baked.message.base);

    await input.fill("not an e-mail address");

    await expect(error).toHaveText("Invalid e-mail address");
  });

  test("valid e-mail address sets model with the given value", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("mouseless@example.com");

    await expect(model).toHaveText("mouseless@example.com");
  });
});

test.describe("InputValidation", () => {
  const id = "InputValidation";

  test("accepts special characters in the local part", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("user.name+tag%test@example.com");

    await expect(model).toHaveText("user.name+tag%test@example.com");
  });

  test("rejects a domain that starts with a hyphen", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const error = component.locator(baked.message.base);

    await input.fill("user@-example.com");

    await expect(error).toHaveText("Invalid e-mail address");
  });

  test("rejects a one-character top-level domain", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputText.base);
    const error = component.locator(baked.message.base);

    await input.fill("user@example.c");

    await expect(error).toHaveText("Invalid e-mail address");
  });
});

test.describe("Validation", () => {
  const id = "Validation";

  test("component shows the validation message", async({ page }) => {
    const component = page.getByTestId(id);
    const message = component.locator(baked.message.base);

    await expect(message).toHaveText("this is an error message");
  });
});