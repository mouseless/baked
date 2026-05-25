import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/await-loading", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("hides content while loading", async({ page }) => {
    const component = page.getByTestId(id);

    const output = component.getByTestId("output");

    await expect(output).not.toBeAttached();
  });

  test("shows skeleton in place", async({ page }) => {
    const component = page.getByTestId(id);

    const skeleton = component.locator(primevue.skeleton.base);

    await expect(skeleton).toBeVisible();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Loaded", () => {
  const id = "Loaded";

  test("shows content", async({ page }) => {
    const component = page.getByTestId(id);

    const output = component.getByTestId("output");

    await expect(output).toHaveText("SHOWN");
  });

  test("hides skeleton", async({ page }) => {
    const component = page.getByTestId(id);

    const skeleton = component.locator(primevue.skeleton.base);

    await expect(skeleton).not.toBeAttached();
  });

  test("props are forwarded to component", async({ page }) => {
    const component = page.getByTestId(id);

    const output = component.getByTestId("output");

    await expect(output).toBeAttached();
  });
});

test.describe("Multi Children Slot", () => {
  const id = "Multi Children Slot";

  test("wraps with a div", async({ page }) => {
    const component = page.getByTestId(id);

    const wrapper = component.locator("div");

    await expect(wrapper.getByTestId("output-1")).toHaveText("1");
    await expect(wrapper.getByTestId("output-2")).toHaveText("2");
  });

  test("props are forwarded to wrapper div", async({ page }) => {
    const component = page.getByTestId(id);

    const wrapper = component.getByTestId("output");

    await expect(wrapper).toBeAttached();
  });
});

test.describe("Customized", () => {
  const id = "Customized";

  test("skeleton width and height", async({ page }) => {
    const component = page.getByTestId(id);

    const skeleton = component.locator(primevue.skeleton.base);

    await expect(skeleton).toHaveCSS("width", "80px");
    await expect(skeleton).toHaveCSS("height", "80px");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Overridden", () => {
  const id = "Overridden";

  test("shows custom content", async({ page }) => {
    const component = page.getByTestId(id);

    const output = component.getByTestId("output");

    await expect(output).toHaveText("loading...");
  });

  test("props are forwarded to loading slot", async({ page }) => {
    const component = page.getByTestId(id);

    const output = component.getByTestId("output");

    await expect(output).toBeAttached();
  });
});

test.describe("Error", () => {
  const id = "Error";

  test("error summary in body", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("error")).toHaveText("TEST ERROR");
  });

  test("summary and detail in tooltip", async({ page }) => {
    const component = page.getByTestId(id);
    const errorSpan = component.getByTestId("error").locator("span");

    await errorSpan.scrollIntoViewIfNeeded();
    await errorSpan.hover();

    await expect(page.locator(primevue.tooltip.base)).toBeAttached();
    await expect(page.locator(primevue.tooltip.base)).toBeVisible();
    await expect(page.locator(primevue.tooltip.base)).toHaveText("TEST ERROR - TEST DESCRIPTION");
  });

  test("error handled", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("handled")).toHaveText("handled");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });

  test("visual (tooltip)", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const errorSpan = component.getByTestId("error").locator("span");

    await errorSpan.scrollIntoViewIfNeeded();
    await errorSpan.hover();

    await expect(page.locator(primevue.tooltip.base)).toBeAttached();
    await expect(page.locator(primevue.tooltip.base)).toBeVisible();
    await expect(component).toHaveScreenshot();
  });
});

test.describe("Customized Error", () => {
  const id = "Customized Error";

  test("skeleton styles in error element", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("error")).toHaveCSS("width", "80px");
    await expect(component.getByTestId("error")).toHaveCSS("height", "80px");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Overridden Error", () => {
  const id = "Overridden Error";

  test("uses template", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("error")).toHaveText("! TEST ERROR - TEST DESCRIPTION !");
  });
});

test.describe("No Error", () => {
  const id = "No Error";

  test("shows content", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText(/CONTENT/);
  });

  test("does not show error", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).not.toHaveText(/TEST ERROR/);
  });

  test("does not handle error", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("handled")).toHaveText("not-handled");
  });
});