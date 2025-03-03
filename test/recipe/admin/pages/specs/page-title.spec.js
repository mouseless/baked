import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/page-title", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title and description", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("title")).toHaveText("PAGE TITLE");
    await expect(component.getByTestId("description")).toHaveText("PAGE DESCRIPTION");
  });
});

test.describe("Actions", () => {
  const id = "Actions";

  test("actions ", async({page}) => {
    const component = page.getByTestId(id);

    expect(component.getByTestId("ACTION_1")).toHaveText("VALUE_1");
    expect(component.getByTestId("ACTION_2")).toHaveText("VALUE_2");
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

    expect(component.getByTestId("description")).toHaveText(" ");
  });
});

