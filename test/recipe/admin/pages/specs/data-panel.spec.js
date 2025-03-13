import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/data-panel", { waitUntil: "hydration" });
});


test.describe("Base", () => {
  const id = "Base";

  test("title", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.panel.title)).toHaveText("TITLE");
  });

  test("content", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content")).toHaveText("TEST DATA");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Collapsed", () => {
  const id = "Collapsed";

  test("panel is collapsed", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content")).not.toBeAttached();
  });

  test("lazy load", async({page}) => {
    const component = page.getByTestId(id);
    const toggle = component.locator(primevue.panel.header).locator(primevue.button.base);

    await toggle.click(); // expand

    await expect(component.getByTestId("content")).toHaveText("DISPLAY ON EXPAND");
  });

  test("keep content after collapse", async({page}) => {
    const component = page.getByTestId(id);
    const toggle = component.locator(primevue.panel.header).locator(primevue.button.base);

    await toggle.click(); // expand
    await toggle.click(); // collapse

    await expect(component.getByTestId("content")).toBeAttached(); // assert it is still there
  });

  test("keep panel state", async({page}) => {
    const component = page.getByTestId(id);
    const toggle = component.locator(primevue.panel.header).locator(primevue.button.base);
    await toggle.click(); // expand
    await page.locator("a[href='/specs']").nth(0).click(); // go back to specs
    await page.locator("a[href='/specs/data-panel']").nth(0).click(); // go to data panel again

    await expect(component.getByTestId("content")).toBeAttached(); // assert it is still there
  });
});
