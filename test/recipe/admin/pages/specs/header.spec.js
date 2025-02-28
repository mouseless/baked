import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/header", { waitUntil: "hydration" });
});

test.describe("Multi Level", () => {
  const id = "Base";

  test("has root and three levels", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.breadcrumb.item)).toHaveCount(4);
  });

  test("item icon", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });

  test("item title", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });

  test("not selected pages (root, mid) are link", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });

  test("selected page (leaf) is not link", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });
});

test.describe("Hidden at Home", () => {
  const id = "No Icon";

  test("only title", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });
});

test.describe("No Title", () => {
  const id = "No Title";

  test("only icon", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });
});

test.describe("No Icon", () => {
  const id = "No Icon";

  test("only title", async({page}) => {
    const component = page.getByTestId(id);

    // TODO assertion
  });
});
