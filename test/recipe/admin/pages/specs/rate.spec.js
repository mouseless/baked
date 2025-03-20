import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/rate", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("50%");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Decimal Point", () => {
  const id = "Decimal Point";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("49.9%");
  });
});

