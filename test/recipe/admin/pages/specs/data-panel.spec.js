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
    const expand = component.locator(primevue.panel.header).locator(primevue.button.base);

    await expand.click();

    await expect(component.getByTestId("content")).toHaveText("DISPLAY ON EXPAND");
  });
});
