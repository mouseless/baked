import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/composite", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("parts", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("component-1")).toHaveText("COMPONENT_1");
    await expect(component.getByTestId("component-2")).toHaveText("COMPONENT_2");
  });
});
