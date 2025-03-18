import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/report-page", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("header", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("header")).toHaveText("PAGE TITLE");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
