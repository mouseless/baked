import { expect, test } from "@nuxt/test-utils/playwright";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto, page }) => {
  await page.route("*/**/report/first", async route => {
    await route.fulfill("fake-response");
  });
  await page.route("*/**/route-parameters-samples/*", async route => {
    await route.fulfill("fake-response");
  });
  await goto("/specs/bake", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("component render", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("TEST");
  });

  test("component has marker class for bake path", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b--variants.b--base")).toBeAttached();
  });

  test("component has marker class for component type", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b-component--Expected")).toBeAttached();
  });
});

test.describe("Parent Data", () => {
  const id = "Parent Data";

  test("reads data from parent", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("child-root"))
      .toHaveText(`
        {
          "child": "CHILD VALUE"
        }`
      );
  });

  test("reads data from parent using given prop", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("child-prop")).toHaveText("CHILD VALUE");
  });
});

test.describe("Data Descriptor", () => {
  const id = "Data Descriptor";

  test("provides data parameters to child", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("test")).toHaveText(/computed/);
    await expect(component.getByTestId("test")).toHaveText(/RequiredWithDefault1/);
    await expect(component.getByTestId("test")).toHaveText(/Required1/);
  });

  test("builds path with given params data", async({ page }) => {
    const requestPromise = page.waitForRequest(req => req.url().includes("/route-parameters-samples"));

    const request = await requestPromise;
    expect(request.url()).toContain("/route-parameters-samples/7b6b67bb-30b5-423e-81b4-a2a0cd59b7f9");
  });
});

test.describe("Model Update", () => {
  const id = "Model Update";

  test("updates model value", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputeText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("Test");

    await expect(model).toHaveText("Test");
  });

  test("model data can be injected", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputeText.base);
    const text = component.locator(baked.string.text);

    await input.fill("Test");

    await expect(text).toHaveText("Test");
  });
});
