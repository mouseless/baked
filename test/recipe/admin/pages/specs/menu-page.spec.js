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

    await expect(component.getByTestId("SECTION_1_NAME")).toHaveText("Section 1");
    await expect(component.getByTestId("SECTION_2_NAME")).toHaveText("Section 2");
  });

  test("links under sections", async({page}) => {
    const component = page.getByTestId(id);
    const section1 = component.getByTestId("SECTION_1");
    const section2 = component.getByTestId("SECTION_2");

    await expect(section1.getByTestId("LINK_1")).toBeVisible();
    await expect(section2.getByTestId("LINK_2")).toBeVisible();
  });
});

test.describe("No Header", () => {
  const id = "No Header";

  test("header not found", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("> *")).toHaveCount(1);
  });
});
