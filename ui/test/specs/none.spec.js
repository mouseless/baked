import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue.js";

test.beforeEach(async({ goto }) => {
  await goto("/specs/none", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("shows loaded data", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("Sample Data");
  });

  test("opens dialog when clicked", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
  });

  test("dialog contains component path", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toHaveText(/test\/path/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });

  test("visual for dialog", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base)).toHaveScreenshot();
  });
});

test.describe("Type", () => {
  const id = "Type";

  test("shows configure button", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("button")).toHaveText("Configure");
  });

  test("shows configuration helper", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base).locator("pre")).toHaveText(
      String.raw`builder.Conventions.AddTypeComponent(
    when: c => c.Type.Is<TestPage>(),
    where: cc => cc.Path.EndsWith("page"),
    component: () => B.Text()
);`
    );
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });

  test("visual for dialog", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base)).toHaveScreenshot();
  });
});

test.describe("Property", () => {
  const id = "Property";

  test("shows configuration helper", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base).locator("pre")).toHaveText(
      String.raw`builder.Conventions.AddPropertyComponent(
    when: c => c.Type.Is<Record>() && c.Property.Name == nameof(Record.Text),
    where: cc => cc.Path.EndsWith("page", "data-table", "columns", "text"),
    component: () => B.Text()
);`
    );
  });
});

test.describe("Method", () => {
  const id = "Method";

  test("shows configuration helper", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base).locator("pre")).toHaveText(
      String.raw`builder.Conventions.AddMethodComponent(
    when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
    where: cc => cc.Path.EndsWith("page", "data-panel"),
    component: () => B.Text()
);`
    );
  });
});

test.describe("Parameter", () => {
  const id = "Parameter";

  test("shows configuration helper", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base).locator("pre")).toHaveText(
      String.raw`builder.Conventions.AddParameterComponent(
    when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData) && c.Parameter.Name == "panel",
    where: cc => cc.Path.EndsWith("page", "data-panel", "parameters"),
    component: () => B.InputText()
);`
    );
  });
});

test.describe("TypeMetadata", () => {
  const id = "TypeMetadata";

  test("metadata types also shows type configuration helper", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator("button").click();

    await expect(page.locator(primevue.dialog.base)).toBeAttached();
    await expect(page.locator(primevue.dialog.base).locator("pre")).toHaveText(/AddTypeComponent/);
  });
});
