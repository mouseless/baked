import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/conditional", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("component changes based on data", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("component-2")).toBeAttached();
  });
});

test.describe("Fallback", () => {
  const id = "Fallback";

  test("returns fallback component when no condition is matches", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("component-3")).toBeAttached();
  });
});