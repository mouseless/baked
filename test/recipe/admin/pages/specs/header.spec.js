/*
import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../../utils/locators/primevue.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/header", { waitUntil: "hydration" });
});

test.describe("Multi Level", () => {
  const id = "Multi Level";

  test("home and three levels", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.item)).toHaveCount(4);
  });

  test("item icons", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.icon)).toHaveClass([
      /pi pi-home/,
      /pi pi-heart/,
      /pi pi-wave-pulse/,
      /pi pi-sun/
    ]);
  });

  test("item titles", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.label)).toHaveText([
      "Root Page",
      "Mid Page",
      "Leaf Page"
    ]);
  });

  test("not selected pages (root, mid) are link", async({page}) => {
    const component = page.getByTestId(id);

    const anchors = component.locator("a");
    await expect(anchors).toHaveCount(3);
    await expect(anchors.nth(0)).toHaveAttribute("href", "/");
    await expect(anchors.nth(1)).toHaveAttribute("href", "/root");
    await expect(anchors.nth(2)).toHaveAttribute("href", "/root/mid");
  });

  test("selected page (leaf) is not link", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(`span${primevue.breadcrumb.link}`)).toHaveText("Leaf Page");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Hidden at Home", () => {
  const id = "Hidden at Home";

  test("breadcrumb not attached", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.base)).not.toBeAttached();
  });
});

test.describe("Hidden at Unknown", () => {
  const id = "Hidden at Unknown";

  test("breadcrumb not attached", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.base)).not.toBeAttached();
  });
});

test.describe("No Title", () => {
  const id = "No Title";

  test("label not attached", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.label)).not.toBeAttached();
  });
});

test.describe("No Icon", () => {
  const id = "No Icon";

  test("only root has icon", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.icon)).toHaveCount(1);
  });
});
*/
