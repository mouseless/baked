import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/nav-link", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("address", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveAttribute("href", "/test-path/test-id");
  });

  test("text", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("TEST");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});
