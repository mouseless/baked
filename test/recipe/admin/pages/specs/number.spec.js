import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/number", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("1,499");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Millions", () => {
  const id = "Millions";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("1.5M");
  });
});

test.describe("Billions", () => {
  const id = "Billions";

  test("format", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("1.5B");
  });
});
