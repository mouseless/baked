import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "../utils/giveMe";
import baked from "../utils/locators/baked";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/data-container", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("content", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content")).toHaveText("TEST DATA");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Inputs", () => {
  const id = "Inputs";

  test("inputs rendered", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("required")).toBeVisible();
    await expect(component.getByTestId("optional")).toBeVisible();
  });

  test("informs only when required inputs are not selected", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(baked.message.base)).toHaveText("Select required values to view this data");

    await component.getByTestId("required").fill("any text");
    await expect(component.locator(baked.message.base)).not.toBeAttached();
  });

  test("listens ready model", async({ page }) => {
    const component = page.getByTestId(id);
    const content = component.getByTestId("content");

    await expect(content).not.toBeVisible();
    await component.getByTestId("required").fill("any text");

    await expect(content).toBeVisible();
  });

  test("redraws when unique key changes", async({ page }) => {
    const component = page.getByTestId(id);

    await component.getByTestId("required").fill("value");

    await expect(component.getByTestId("content")).toHaveText(/value/);
  });
});

test.describe("Actions", () => {
  const id = "Actions";

  test("actions rendered", async({ page }) => {
    const component = page.getByTestId(id);
    const actions = component.locator(primevue.button.base);

    await expect(actions.nth(0)).toBeAttached();
    await expect(actions.nth(1)).toBeAttached();
  });

  test("action label hidden for iconed below sm", async({ page }) => {
    const component = page.getByTestId(id);
    const actions = component.locator(primevue.button.base);
    const screen = giveMe.aScreenSize({ name: "xs" });

    await page.setViewportSize({ ...screen });

    await expect(actions.nth(0).getByText("ACTION_1")).toBeVisible();
    await expect(actions.nth(1)).toBeAttached();
    await expect(actions.nth(1).locator(primevue.button.icon)).toBeVisible();
    await expect(actions.nth(1).getByText("ACTION_2")).not.toBeVisible();
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
