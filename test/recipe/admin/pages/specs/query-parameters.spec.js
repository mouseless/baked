import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/query-parameters", { waitUntil: "hydration" });
});

const id = {
  component: "component",
  uniqueKey: "unique-key",
  ready: "ready",
  reset: "reset"
};

test("parameters are rendered", async({page}) => {
  const component = page.getByTestId(id.component);

  await expect(component.getByTestId("required-with-default")).toBeVisible();
  await expect(component.getByTestId("required")).toBeVisible();
  await expect(component.getByTestId("optional")).toBeVisible();
});

test("default value is set", async({page}) => {
  const component = page.getByTestId(id.component);

  await expect(component.getByTestId("required-with-default")).toHaveValue("default value");

  const params = new URLSearchParams(new URL(page.url()).search);
  expect(params.get("requiredWithDefault")).toBe("default value");
});

test("reset to default value after route to self", async({page}) => {
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

test("query string is set from input", async({page}) => {
  const component = page.getByTestId(id.component);

  await component.getByTestId("required-with-default").fill("value 1");
  await component.getByTestId("required").fill("value 2");
  await component.getByTestId("optional").fill("value 3");
  await page.waitForURL(/requiredWithDefault.*required.*optional/); // wait for above fills to take effect

  const params = new URLSearchParams(new URL(page.url()).search);
  expect(params.get("requiredWithDefault")).toBe("value 1");
  expect(params.get("required")).toBe("value 2");
  expect(params.get("optional")).toBe("value 3");
});

test("query string is set to input", async({page}) => {
  await page.goto("/specs/query-parameters?requiredWithDefault=1&required=2&optional=3");

  const component = page.getByTestId(id.component);
  await expect(component.getByTestId("required-with-default")).toHaveValue("1");
  await expect(component.getByTestId("required")).toHaveValue("2");
  await expect(component.getByTestId("optional")).toHaveValue("3");
});

test("replaces route until defaults are set", async({page}) => {
  await page.waitForURL(/requiredWithDefault/); // wait for default value to be set

  const state = await page.evaluate(() => history.state);

  expect(state.back).toBeNull();
});

test("pushes route when a parameter changes", async({page}) => {
  const component = page.getByTestId(id.component);

  await component.getByTestId("required").fill("value 1");
  await page.waitForURL(/value\+1/); // wait for above fill to take effect

  const state = await page.evaluate(() => history.state);
  expect(state.back).toBe("/specs/query-parameters?requiredWithDefault=default+value");

  await component.getByTestId("required").fill("value 2");
  await page.waitForURL(/value\+2/); // wait for above fill to take effect

  const state2 = await page.evaluate(() => history.state);
  expect(state2.back).toBe("/specs/query-parameters?requiredWithDefault=default+value&required=value+1");
});

test("ready when all required are set", async({page}) => {
  const component = page.getByTestId(id.component);
  const ready = page.getByTestId(id.ready);
  await expect(ready).toHaveText("false");

  await component.getByTestId("required").fill("x");

  await expect(ready).toHaveText("true");
});

test("unique key changes with parameter values", async({page}) => {
  const component = page.getByTestId(id.component);
  const uniqueKey = page.getByTestId(id.uniqueKey);

  await component.getByTestId("required-with-default").fill("value 1");
  await component.getByTestId("required").fill("value 2");
  await component.getByTestId("optional").fill("value 3");

  await expect(uniqueKey).toHaveText("value 1-value 2-value 3");
});
