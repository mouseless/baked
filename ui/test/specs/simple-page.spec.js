import { test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/simple-page", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    // TODO write test

    console.log(component);
    // await expect(component.locator("h1")).toHaveText("Title");
  });

  test("contents", async({ page }) => {
    const component = page.getByTestId(id);

    // TODO write test

    console.log(component);
    // await expect(component.getByTestId("content 1.1")).toHaveText("CONTENT 1.1");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    // TODO write test

    console.log(component);
    // await expect(component).toHaveScreenshot();
  });
});
