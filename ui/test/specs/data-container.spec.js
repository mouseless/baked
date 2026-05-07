import { expect, test } from "@nuxt/test-utils/playwright";
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

    await expect(component.locator(primevue.message.base)).toHaveText("Select required values to view this data");

    await component.getByTestId("required").fill("any text");
    await expect(component.locator(primevue.message.base)).not.toBeAttached();
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
