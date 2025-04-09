import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/menu-page", { waitUntil: "hydration" });
});

test.describe("Header and Links", () => {
  const id = "Header and Links";

  test("header", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("header")).toHaveText("PAGE TITLE");
  });

  test("actions", async({page}) => {
    const component = page.getByTestId(id);

    for(let i = 0; i < 12; i++) {
      await expect(component.getByTestId(`LINK_${i}`)).toHaveText(`VALUE_${i}`);
    }
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Sections", () => {
  const id = "Sections";

  test("sections", async({page}) => {
    const component = page.getByTestId(id);
    const titles = component.locator("h2");

    await expect(titles.nth(0)).toHaveText("Section 1");
    await expect(titles.nth(1)).toHaveText("Section 2");
  });

  test("links under sections", async({page}) => {
    const component = page.getByTestId(id);
    const sections = component.locator("div:has(h2)");

    await expect(sections.nth(0).getByTestId("LINK_1")).toBeVisible();
    await expect(sections.nth(1).getByTestId("LINK_2")).toBeVisible();
  });
});

test.describe("Filter Links", () => {
  const id = "Filter Links";

  test("filter", async({page}) => {
    const component = page.getByTestId(id);

    const filter = component.locator("input");

    await filter.fill("A");

    await expect(component.getByTestId("LINK_1")).toBeVisible();
    await expect(component.getByTestId("LINK_2")).not.toBeVisible();

    await filter.fill("B");

    await expect(component.getByTestId("LINK_2")).toBeVisible();
    await expect(component.getByTestId("LINK_1")).not.toBeVisible();
  });
});

test.describe("No Header", () => {
  const id = "No Header";

  test("header not found", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("> *")).toHaveCount(1);
  });
});
