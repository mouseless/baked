import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/inputs--form-mode", { waitUntil: "hydration" });
});

const id = {
  component: "component",
  ready: "ready",
  values: "values"
};

test("ready becomes false when required input is cleared", async({ page }) => {
  const input = page.getByTestId(id.component).getByTestId("required-with-default");
  const ready = page.getByTestId(id.ready);

  await input.fill("");

  await expect(ready).toHaveText("false");
});

test("input stays empty when cleared", async({ page }) => {
  const input = page.getByTestId(id.component).getByTestId("required-with-default");

  await input.fill("x");
  await input.fill("");

  await expect(input).toHaveValue("");
});

test("values becomes empty when input with default is cleared", async({ page }) => {
  const input = page.getByTestId(id.component).getByTestId("required-with-default");
  const values = page.getByTestId(id.values);

  await input.fill("");

  await expect(values).toHaveText("{}");
});
