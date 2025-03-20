import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/data-table", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("columns", async({page}) => {
    const component = page.getByTestId(id);
    const columns = component.locator("th");

    await expect(columns.nth(0)).toHaveText("Label");
    await expect(columns.nth(1)).toHaveText("Data 1");
    await expect(columns.nth(2)).toHaveText("Data 2");
    await expect(columns.nth(3)).toHaveText("Data 3");
    await expect(columns.nth(4)).toHaveText("Data 4");
  });

  test("cells", async({page}) => {
    const component = page.getByTestId(id);
    const cells = component.locator("td");
    const cellNo = (x, y) => (x - 1) * 5 + y - 1;

    await expect(cells).toHaveCount(25);
    await expect(cells.nth(cellNo(1, 1))).toHaveText("Row 1");
    await expect(cells.nth(cellNo(3, 3))).toHaveText("Cell 3.2");
    await expect(cells.nth(cellNo(5, 5))).toHaveText("Cell 5.4");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Loading", () => {
  const id = "Loading";

  test("row count when loading", async({page}) => {
    const component = page.getByTestId(id);
    const rows = component.locator("tr");

    await expect(rows).toHaveCount(1 + 3);
  });
});
