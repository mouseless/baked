/*
import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/menu-page", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h1")).toHaveText("PAGE TITLE");
  });

  test("description", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).toHaveText("PAGE DESCRIPTION");
  });
});

test.describe("Links", () => {
  const id = "Links";

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

test.describe("No Description", () => {
  const id = "No Description";

  test("description still available with nbsp", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).toHaveText(" ");
  });
});
*/
