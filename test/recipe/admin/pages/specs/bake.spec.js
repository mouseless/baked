import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto, page}) => {
  await page.route("*/**/report/first", async route => {
    await route.fulfill("fake-response");
  });
  await goto("/specs/bake", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("component render", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("TEST");
  });

  test("component has marker class for bake path", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b--variants.b--base")).toBeAttached();
  });

  test("component has marker class for component type", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b-component--Expected")).toBeAttached();
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

test.describe("Data Descriptor", () => {
  const id = "Data Descriptor";

  test("provides data parameters to child", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("test")).toHaveText(/computed/);
    await expect(component.getByTestId("test")).toHaveText(/remote-1/);
    await expect(component.getByTestId("test")).toHaveText(/remote-2/);
  });
});
