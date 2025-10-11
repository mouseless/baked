import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({ goto }) => {
  await goto("/specs/menu-page", { waitUntil: "hydration" });
});

test.describe("Header and Links", () => {
  const id = "Header and Links";

  test("header", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("header")).toHaveText("PAGE TITLE");
  });

  test("actions", async({ page }) => {
    const component = page.getByTestId(id);

    for(let i = 0; i < 12; i++) {
      await expect(component.getByTestId(`LINK_${i}`)).toHaveText(`VALUE_${i}`);
    }
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Sections", () => {
  const id = "Sections";

  test("sections are listed with locale upper case titles", async({ page }) => {
    const component = page.getByTestId(id);
    const titles = component.locator("h2");

    await expect(titles.nth(0)).toHaveText("SECTION 1");
    await expect(titles.nth(1)).toHaveText("SECTION 2");
  });
});

test.describe("Filter Links", () => {
  const id = "Filter Links";

  test("show all", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("LINK_1")).toBeVisible();
    await expect(component.getByTestId("LINK_2")).toBeVisible();
  });

  test("filter", async({ page }) => {
    const component = page.getByTestId(id);

    const filter = component.locator("input");

    await filter.fill("A");

    await expect(component.getByTestId("LINK_1")).toBeVisible();
    await expect(component.getByTestId("LINK_2")).not.toBeVisible();

    await filter.fill("B");

    await expect(component.getByTestId("LINK_2")).toBeVisible();
    await expect(component.getByTestId("LINK_1")).not.toBeVisible();
  });

  test("not found", async({ page }) => {
    const component = page.getByTestId(id);

    const filter = component.locator("input");

    await filter.fill("x");

    await expect(component.getByTestId("LINK_1")).not.toBeVisible();
    await expect(component.getByTestId("LINK_2")).not.toBeVisible();
    await expect(component).toHaveText("No item available!");
  });
});

test.describe("No Header", () => {
  const id = "No Header";

  test("header not found", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator("> *")).toHaveCount(1);
  });
});
