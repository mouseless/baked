import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/query-bound-inputs", { waitUntil: "hydration" });
});

const id = {
  component: "component",
  uniqueKey: "unique-key",
  ready: "ready",
  reset: "reset"
};

test("parameters are rendered", async({ page }) => {
  const component = page.getByTestId(id.component);

  await expect(component.getByTestId("required-with-default")).toBeVisible();
  await expect(component.getByTestId("required-with-default-self-managed")).toBeVisible();
  await expect(component.getByTestId("required")).toBeVisible();
  await expect(component.getByTestId("optional")).toBeVisible();
  await expect(component.getByTestId("num-required")).toBeVisible();
});

test("default value is set", async({ page }) => {
  const component = page.getByTestId(id.component);

  await expect(component.getByTestId("required-with-default")).toHaveValue("default value");

  const params = new URLSearchParams(new URL(page.url()).search);
  expect(params.get("requiredWithDefault")).toBe("default value");
});

test("reset to default value after route to self", async({ page }) => {
  const component = page.getByTestId(id.component);
  const reset = page.getByTestId(id.reset);

  await component.getByTestId("required").fill("x");
  await reset.click();

  await expect(component.getByTestId("required-with-default")).toHaveValue("default value");
  await expect(component.getByTestId("required")).not.toHaveValue("x");

  const params = new URLSearchParams(new URL(page.url()).search);
  expect(params.get("requiredWithDefault")).toBe("default value");
  expect(params.get("required")).toBeNull();
});

test("query string is set from input", async({ page }) => {
  const component = page.getByTestId(id.component);

  await component.getByTestId("required-with-default").fill("value 1");
  await component.getByTestId("required-with-default-self-managed").fill("value 2");
  await component.getByTestId("required").fill("value 3");
  await component.getByTestId("optional").fill("value 4");
  await page.waitForURL(/requiredWithDefault.*requiredWithDefaultSelfManaged.*required.*optional/); // wait for above fills to take effect

  const params = new URLSearchParams(new URL(page.url()).search);
  expect(params.get("requiredWithDefault")).toBe("value 1");
  expect(params.get("requiredWithDefaultSelfManaged")).toBe("value 2");
  expect(params.get("required")).toBe("value 3");
  expect(params.get("optional")).toBe("value 4");
});

test("query string is set to input", async({ page }) => {
  await page.goto("/specs/query-bound-inputs?requiredWithDefault=1&requiredWithDefaultSelfManaged=2&required=3&optional=4");

  const component = page.getByTestId(id.component);
  await expect(component.getByTestId("required-with-default")).toHaveValue("1");
  await expect(component.getByTestId("required-with-default-self-managed")).toHaveValue("2");
  await expect(component.getByTestId("required")).toHaveValue("3");
  await expect(component.getByTestId("optional")).toHaveValue("4");
});

test("replaces route until defaults are set", async({ page }) => {
  await page.waitForURL(/requiredWithDefault/);

  const state = await page.evaluate(() => history.state);

  expect(state.back).toBeNull();
});

test("pushes route when a parameter changes", async({ page }) => {
  const component = page.getByTestId(id.component);

  await component.getByTestId("required").fill("value 1");
  await page.waitForURL(/value\+1/);

  const state = await page.evaluate(() => history.state);
  expect(state.back).toBe("/specs/query-bound-inputs?requiredWithDefault=default+value&requiredWithDefaultSelfManaged=default");

  await component.getByTestId("required").fill("value 2");
  await page.waitForURL(/value\+2/);

  const state2 = await page.evaluate(() => history.state);
  expect(state2.back).toBe("/specs/query-bound-inputs?requiredWithDefault=default+value&requiredWithDefaultSelfManaged=default&required=value+1");
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

test("unique key changes with parameter values", async({ page }) => {
  const component = page.getByTestId(id.component);
  const uniqueKey = page.getByTestId(id.uniqueKey);

  await component.getByTestId("required-with-default").fill("value 1");
  await component.getByTestId("required-with-default-self-managed").fill("value 2");
  await component.getByTestId("required").fill("value 3");
  await component.getByTestId("optional").fill("value 4");
  await component.locator(primevue.inputNumber.base).click();
  await page.keyboard.press("Digit0");

  await expect(uniqueKey).toHaveText("value 1-value 2-value 3-value 4-0");
});
