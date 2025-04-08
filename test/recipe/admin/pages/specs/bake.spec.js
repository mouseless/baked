import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/bake", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("component render", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("TEST");
  });
});

test.describe("Parent Data", () => {
  const id = "Parent Data";

  test("reads data from parent", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("child-root"))
      .toHaveText(`
        {
          "child": "CHILD VALUE"
        }`
      );
  });

  test("reads data from parent using given prop", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("child-prop")).toHaveText("CHILD VALUE");
  });
});
