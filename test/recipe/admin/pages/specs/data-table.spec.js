import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

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

test.describe("Pagination", () => {
  const id = "Pagination";

  test("paginator", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.paginator.base)).toBeVisible();
  });

  test("page count", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.paginator.page)).toHaveCount(2);
  });

  test("data update on page changes", async({page}) => {
    const component = page.getByTestId(id);
    const pages = component.locator(primevue.paginator.page);

    await pages.nth(1).click();

    await expect(component.locator("td")).toHaveText("Row 3");
  });
});

test.describe("Row Based Component", () => {
  const id = "Row Based Component";

  test("component changes based on data", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("component-1")).toHaveText("Data 1");
    await expect(component.getByTestId("component-2")).toHaveText("Data 2");
  });
});

test.describe("Auto Hide Pagination", () => {
  const id = "Auto Hide Pagination";

  test("no paginator", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.paginator.base)).not.toBeVisible();
  });
});

test.describe("Footer", () => {
  const id = "Footer";

  test("show footer row when configured in schema", async({page}) => {
    const component = page.getByTestId(id);
    const footer = component.locator(primevue.datatable.footer);

    await expect(footer.locator("td")).toHaveCount(3);
    await expect(footer.locator("td").first()).toHaveText("Total");
    expect(await footer.locator("td").first().getAttribute("colspan")).toBe("3");
    await expect(footer.locator("td").nth(1)).toHaveText("3");
    await expect(footer.locator("td").nth(2)).toHaveText("30");
  });
});

test.describe("Export", () => {
  const id = "Export";

  test("show export button when configured", async({page}) => {
    const component = page.getByTestId(id);
    const header = component.locator("th").last();

    await expect(header.locator(primevue.button.icon)).toHaveClass("p-button-icon pi pi-file-export");
  });

  // TODO unable to locate tooltip, will be solved later
  //
  // test("show tooltip on hover", async({page}) => {
  //   const component = page.getByTestId(id);
  //   const header = component.locator("th").last();

  //   await header.locator(primevue.button.base).hover();

  //   await expect(page.locator(primevue.tooltip.left)).toBeAttached();
  //   await expect(page.locator(primevue.tooltip.left)).toBeVisible();
  //   await expect(page.locator(primevue.tooltip.left)).toHaveText("CSV");
  // });
});

test.describe("Loading", () => {
  const id = "Loading";

  test("row count when loading", async({page}) => {
    const component = page.getByTestId(id);
    const rows = component.locator("tr");

    await expect(rows).toHaveCount(1 + 3);
  });
});
