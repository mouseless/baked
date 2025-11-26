import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/inputs", { waitUntil: "hydration" });
});

const id = {
  component: "component",
  onReadyValues: "onReadyValues-key",
  uniqueKey: "unique-key",
  ready: "ready"
};

test("inputs are rendered", async({ page }) => {
  const component = page.getByTestId(id.component);

  await expect(component.getByTestId("required-with-default")).toBeVisible();
  await expect(component.getByTestId("required")).toBeVisible();
  await expect(component.getByTestId("optional")).toBeVisible();
  await expect(component.getByTestId("num-required")).toBeVisible();
});

test("default value is set", async({ page }) => {
  const component = page.getByTestId(id.component);

  await expect(component.getByTestId("required-with-default")).toHaveValue("default value");
});

test("ready when all required are set", async({ page }) => {
  const component = page.getByTestId(id.component);
  const ready = page.getByTestId(id.ready);
  await expect(ready).toHaveText("false");

  await component.getByTestId("required").fill("x");
  await component.locator(primevue.inputNumber.base).click();
  await page.keyboard.press("Digit0");

  await expect(ready).toHaveText("true");
});

test("unique key changes with input values", async({ page }) => {
  const component = page.getByTestId(id.component);
  const uniqueKey = page.getByTestId(id.uniqueKey);

  await component.getByTestId("required-with-default").fill("value 1");
  await component.getByTestId("required").fill("value 2");
  await component.getByTestId("optional").fill("value 3");
  await component.locator(primevue.inputNumber.base).click();
  await page.keyboard.press("Digit1");

  await expect(uniqueKey).toHaveText("value 1-value 2-value 3-1");
});

test("'onChanged' is emitted before 'onReady' when initialized", async({ page }) => {
  const readyValues = page.getByTestId(id.onReadyValues);
  const uniqueKey = page.getByTestId(id.uniqueKey);

  await expect(readyValues).toHaveText("default value");
  await expect(uniqueKey).toHaveText("default value");
});

test("'onChanged' is emitted before 'onReady' when inputs are changed", async({ page }) => {
  const component = page.getByTestId(id.component);
  const readyValues = page.getByTestId(id.onReadyValues);
  const uniqueKey = page.getByTestId(id.uniqueKey);

  await component.getByTestId("required-with-default").fill("value 1");

  await expect(readyValues).toHaveText("value 1");
  await expect(uniqueKey).toHaveText("value 1");
});
