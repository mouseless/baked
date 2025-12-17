import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/tabbed-page", { waitUntil: "hydration" });
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

  test("tabs", async({ page }) => {
    const component = page.getByTestId(id);

    const tabs = component.locator(primevue.tab.base);
    await expect(tabs).toHaveCount(2);
    await expect(tabs.nth(0).locator("span").last()).toHaveText("Tab 1");
    await expect(tabs.nth(1).locator("span").last()).toHaveText("Tab 2");
  });

  test("tab icons", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("icon 1")).toHaveText("I.");
    await expect(component.getByTestId("icon 2")).toHaveText("II.");
  });

  test("tab content", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content 1.1")).toHaveText("CONTENT 1.1");
    await expect(component.getByTestId("content 1.2")).toHaveText("CONTENT 1.2");

    await component.locator(primevue.tab.base).nth(1).click(); // switch to tab 2

    await expect(component.getByTestId("content 2.1")).toHaveText("CONTENT 2.1");
    await expect(component.getByTestId("content 2.2")).toHaveText("CONTENT 2.2");
  });

  test("tab content is deferred", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content 2.1")).not.toBeAttached();
    await expect(component.getByTestId("content 2.2")).not.toBeAttached();
  });

  test("grid added", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b-TabbedPage--grid")).toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Single Tab", () => {
  const id = "Single Tab";

  test("tab hidden when there is one tab", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content")).toBeAttached(); // required to wait for page to render
    await expect(component.locator(primevue.tab.base)).not.toBeAttached();
  });
});

test.describe("Full Page", () => {
  const id = "Full Page";

  test("grid not added", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b-TabbedPage--grid")).not.toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Narrow", () => {
  const id = "Narrow";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Show When", () => {
  const id = "Show When";

  test("tab is hidden when there is no selection", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content-2")).not.toBeAttached();
  });

  test("tab content is hidden when there is no selection", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content-1")).not.toBeAttached();
  });

  test("tab content is shown when selection is SHOW", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle

    await expect(component.getByTestId("content-1")).toHaveText("CONTENT 1");
  });

  test("tab is shown when selection is SHOW", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle
    await component.locator(".b--tab-2").click(); // click Tab 2

    await expect(component.getByTestId("content-2")).toHaveText("CONTENT 2");
  });

  test("when tab gets hidden, first shown tab is selected ", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle
    await component.locator(".b--tab-2").click(); // click Tab 2
    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle

    await expect(component.locator(".p-tab-active")).toHaveText("Tab 1");
  });

  test("when hidden tab gets shown again, it is automatically selected", async({ page }) => {
    const component = page.getByTestId(id);

    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle
    await component.locator(".b--tab-2").click(); // click Tab 2
    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle
    await component.locator(primevue.selectbutton.option).nth(0).click(); // toggle

    await expect(component.locator(".p-tab-active")).toHaveText("Tab 2");
  });
});

test.describe("Inputs", () => {
  test.beforeEach(async({ page }) => {
    // resets `useRoute().query` value between tests
    await page.reload({ waitUntil: "load" });
  });

  const id = "Inputs";

  test("inputs rendered", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("required")).toBeVisible();
    await expect(component.getByTestId("optional")).toBeVisible();
  });

  test("informs only when required params are not selected", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.message.base)).toHaveText("Select required values to view this page");

    await component.getByTestId("required").fill("any text");
    await expect(component.locator(primevue.message.base)).not.toBeAttached();
  });

  test("listens ready model", async({ page }) => {
    const component = page.getByTestId(id);
    const staticData = component.getByTestId("static-content");

    await expect(staticData).not.toBeVisible();
    await component.getByTestId("required").fill("any text");

    await expect(staticData).toBeVisible();
  });

  test("redraws when unique key changes", async({ page }) => {
    const component = page.getByTestId(id);

    await component.getByTestId("required").click();
    await component.getByTestId("required").fill("value");

    await expect(component.getByTestId("query-content")).toHaveText(/value/);
  });
});
