import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/form-page", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h1")).toHaveText("Title");
  });

  test("description", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).toHaveText("Description");
  });

  test("inputs", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("input")).toBeAttached();
  });

  test("button", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(1);

    await expect(button).toHaveText("Submit");
  });

  test("button is disabled when inputs are not ready", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base).nth(1);

    await expect(button).toBeDisabled();
  });

  test("button is enabled when inputs are ready", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base).nth(1);

    await input.fill("text");

    await expect(button).not.toBeDisabled();
  });

  test("action", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base).nth(1);

    await input.fill("text");
    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("text");
  });

  test("button is disabled until action is completed", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    const button = component.locator(primevue.button.base).nth(1);

    await input.fill("text");
    await button.click();

    await expect(button).toBeDisabled();
    const spinner = button.locator(".p-button-loading-icon, .pi-spinner");
    await expect(spinner).toBeVisible();
    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(button).not.toBeDisabled();
  });

  test("section is hidden when there is a single section", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).not.toHaveText(/Default/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Multiple Inputs", () => {
  const id = "Multiple Inputs";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Sections", () => {
  const id = "Sections";

  test("display sections with localized labels", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText(/Section 1/);
    await expect(component).toHaveText(/Section 2/);
  });
});

test.describe("Validations", () => {
  const id = "Validations";

  test("show a red border on required fields when the user leaves without entry", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("param-1");
    const input2 = component.getByTestId("param-2");

    await input1.focus();
    await input2.focus();

    await expect(input1).toContainClass("p-invalid");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("param-1");
    const input2 = component.getByTestId("param-2");

    await input1.focus();
    await input2.focus();

    await expect(component).toHaveScreenshot();
  });

  test("disable form submitting when return invalid state by non-required input", async({ page }) => {
    const component = page.getByTestId(id);
    const input1 = component.getByTestId("param-1");
    const input2 = component.getByTestId("param-2");
    const button = component.locator(primevue.button.base).nth(1);

    await input1.fill("text");
    await input2.fill("custom");

    await expect(button).toBeDisabled();
  });

});

test.describe("Layout Options", () => {
  const id = "Layout Options";

  test("input flow goes column based", async({ page }) => {
    const component = page.getByTestId(id);

    const grids = component.locator(".b-Contents .grid .grid");
    const count = await grids.count();

    await expect(grids).toHaveClass(Array(count).fill(/grid-flow-col/));
  });

  test("grouped inputs fill one cell", async({ page }) => {
    const component = page.getByTestId(id);

    const grids = component.locator(".b-Contents .grid .grid");
    const count = await grids.count();

    await expect(grids).toHaveClass(Array(count).fill(/grid-flow-col/));
  });

  test("inputs are split by wide groups", async({ page }) => {
    const component = page.getByTestId(id);

    const grids = component.locator(".b-Contents .grid .grid");

    await expect(grids).toHaveCount(4);

    await expect(grids.nth(0).locator("input")).toHaveCount(5);
    await expect(grids.nth(1).locator("input")).toHaveCount(1);
    await expect(grids.nth(2).locator("input")).toHaveCount(3);
    await expect(grids.nth(3).locator("input")).toHaveCount(1);
  });

  test("wide group spans to two cells", async({ page }) => {
    const component = page.getByTestId(id);

    const grids = component.locator(".b-Contents .grid .grid");
    const group1 = grids.nth(0).locator(".flex").nth(0).locator("input");
    const group2 = grids.nth(2).locator(".flex").nth(0).locator("input");

    await expect(group1).toHaveCount(2);
    await expect(group2).toHaveCount(3);
  });

  test("wide groups reset min width to make them fit into cell", async({ page }) => {
    const component = page.getByTestId(id);

    const grids = component.locator(".b-Contents .grid .grid");
    const group1 = grids.nth(0).locator(".flex").nth(0);
    const group2 = grids.nth(2).locator(".flex").nth(0);

    await expect(group1).toHaveClass(/reset-min-w/);
    await expect(group2).toHaveClass(/reset-min-w/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});