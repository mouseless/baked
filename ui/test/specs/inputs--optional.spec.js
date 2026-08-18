import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/inputs--optional", { waitUntil: "hydration" });
});

const id = {
  component: "component",
  ready: "ready",
  uniqueKey: "unique-key"
};

test("ready is emitted true when all inputs are optional and untouched", async({ page }) => {
  const ready = page.getByTestId(id.ready);

  await expect(ready).toHaveText("true");
});

test("changed is emitted even when all inputs are optional and untouched", async({ page }) => {
  const uniqueKey = page.getByTestId(id.uniqueKey);

  await expect(uniqueKey).toHaveText("");
});