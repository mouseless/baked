import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/simple-page", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("title")).toHaveText("TITLE");
  });

  test("contents", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content-1")).toHaveText("CONTENT_1");
    await expect(component.getByTestId("content-2")).toHaveText("CONTENT_2");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
