import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "../utils/giveMe";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/page-title", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h1")).toHaveText("Title");
  });

  test("description is visible on md and above", async({ page }) => {
    const component = page.getByTestId(id);
    const description = component.getByTestId("description");
    const infoIcon = component.locator(primevue.button.icon);

    const tablet = giveMe.aScreenSize({ name: "md" });
    await page.setViewportSize({ ...tablet });

    await expect(description).toBeVisible();
    await expect(infoIcon).toBeHidden();
  });

  test("description is hidden on sm and below", async({ page }) => {
    const component = page.getByTestId(id);
    const description = component.getByTestId("description");
    const infoIcon = component.locator(primevue.button.icon);

    const mobile = giveMe.aScreenSize({ name: "sm" });
    await page.setViewportSize({ ...mobile });

    await expect(description).toBeHidden();
    await expect(infoIcon).toBeVisible();
  });

  test("description appears on tooltip when clicked to info icon", async({ page }) => {
    const component = page.getByTestId(id);
    const infoIcon = component.locator(primevue.button.icon);

    const mobile = giveMe.aScreenSize({ name: "sm" });
    await page.setViewportSize({ ...mobile });
    await infoIcon.click();

    await expect(page.locator(primevue.tooltip.bottom)).toBeAttached();
    await expect(page.locator(primevue.tooltip.bottom)).toBeVisible();
    await expect(page.locator(primevue.tooltip.bottom)).toHaveText("Description");
  });
});

test.describe("Actions", () => {
  const id = "Actions";

  test("actions", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("button").nth(1)).toHaveText("ACTION_1");
    await expect(component.locator("button").nth(2)).toHaveText("ACTION_2");
  });

  test("action label hidden for iconed below sm", async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "xs" });

    await page.setViewportSize({ ...screen });

    await expect(component.locator("button").nth(1).getByText("ACTION_1")).toBeVisible();
    await expect(component.locator("button").nth(2).getByText("ACTION_2")).not.toBeVisible();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });

  test("visual (early wraps actions)", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "2xs" });

    await page.setViewportSize({ ...screen });

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Dynamic", () => {
  const id = "Dynamic";

  test("title", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h1")).toHaveText("From Data");
  });

  test("icon", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("icon")).toHaveText("PT");
  });

  test("info fields", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText(/Info 1/);
    await expect(component.getByText("Info 1")).toBeVisible();
    await expect(component.getByTestId("info-1")).toHaveText("info-1");
    await expect(component).toHaveText(/Info 2/);
    await expect(component.getByText("Info 2")).toBeVisible();
    await expect(component.getByTestId("info-2")).toHaveText("info-2");
  });

  test("info labels hidden below sm", async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "xs" });
    await page.setViewportSize({ ...screen });

    await expect(component.getByText("Info 1")).not.toBeVisible();
    await expect(component.getByTestId("info-1")).toHaveText("info-1");
    await expect(component.getByText("Info 2")).not.toBeVisible();
    await expect(component.getByTestId("info-2")).toHaveText("info-2");
  });

  test("info labels hidden below xs", async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "2xs" });
    await page.setViewportSize({ ...screen });

    await expect(component.getByText("Info 1")).not.toBeVisible();
    await expect(component.getByTestId("info-1")).not.toBeVisible();
    await expect(component.getByText("Info 2")).not.toBeVisible();
    await expect(component.getByTestId("info-2")).not.toBeVisible();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Inputs", () => {
  const id = "Inputs";

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });

  test("visual mini", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "2xs" });

    await page.setViewportSize({ ...screen });
    await expect(component).toHaveScreenshot();
  });

  test("visual mobile", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "xs" });

    await page.setViewportSize({ ...screen });
    await expect(component).toHaveScreenshot();
  });

  test("visual tablet", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "sm" });

    await page.setViewportSize({ ...screen });
    await expect(component).toHaveScreenshot();
  });

  test("visual small window", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "md" });

    await page.setViewportSize({ ...screen });
    await expect(component).toHaveScreenshot();
  });
});