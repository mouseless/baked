import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "~/utils/giveMe";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/data-panel", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.panel.title)).toHaveText("Title");
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

test.describe("Base with computed title", () => {
  const id = "Base with computed title";

  test("title value is not localized", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.panel.title)).toHaveText("Title");
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

test.describe("Parameters", () => {
  const id = "Parameters";

  test("inputs rendered", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("required")).toBeVisible();
    await expect(component.getByTestId("optional")).toBeVisible();
  });

  test("informs only when required params are not selected", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.message.base)).toHaveText("Select required values to view this data");

    await component.getByTestId("required").fill("any text");
    await expect(component.locator(primevue.message.base)).not.toBeAttached();
  });

  test("listens ready model", async({page}) => {
    const component = page.getByTestId(id);
    const content = component.getByTestId("content");

    await expect(content).not.toBeVisible();
    await component.getByTestId("required").fill("any text");

    await expect(content).toBeVisible();
  });

  test("redraws when unique key changes", async({page}) => {
    const component = page.getByTestId(id);

    await component.getByTestId("required").fill("value");

    await expect(component.getByTestId("content")).toHaveText(/value/);
  });

  test("visual for lg", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "lg" });

    await page.setViewportSize({ ...screen });

    await expect(component.getByTestId("required")).toBeVisible();
    await expect(component.getByTestId("optional")).toBeVisible();
    await expect(component).toHaveScreenshot();
  });

  test("visual for mobile", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "sm" });

    await page.setViewportSize({ ...screen });

    await expect(component).toHaveScreenshot();
  });

  test("visual for mobile opened", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "sm" });
    const content = page.locator(primevue.popover.content);
    await page.setViewportSize({ ...screen });

    const filterIcon = component.locator(primevue.button.icon).nth(0);
    await filterIcon.click();

    await expect(content).toHaveScreenshot();
  });

  test("popover visibility based on mobile screen size", async({page}) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "sm" });
    await page.setViewportSize({ ...screen });

    const filterIcon = component.locator(primevue.button.icon).nth(0);

    await expect(component.getByTestId("required")).not.toBeAttached();
    await expect(component.getByTestId("optional")).not.toBeAttached();
    await expect(filterIcon).toBeVisible();
  });

  test("popover functionality based on mobile screen size", async({page}) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "sm" });
    const content = page.locator(primevue.popover.content);
    await page.setViewportSize({ ...screen });

    const filterIcon = component.locator(primevue.button.icon).nth(0);
    await filterIcon.click();

    await expect(content.getByTestId("required")).toBeVisible();
    await expect(content.getByTestId("optional")).toBeVisible();
  });
});