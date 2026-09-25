import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto }) => {
  await goto("/specs/text-link", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("renders url as link", async({ page }) => {
    const component = page.getByTestId(id);
    const anchor = component.locator(primevue.button.link);

    await expect(anchor).toHaveAttribute("href", "https://baked.mouseless.codes");
    await expect(anchor).toHaveText("baked.mouseless.codes");
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Truncated", () => {
  const id = "Truncated";

  test("truncates the url string and keep target value", async({ page }) => {
    const component = page.getByTestId(id);
    const anchor = component.locator(primevue.button.link);

    await expect(anchor).toHaveAttribute("href", "https://baked.mouseless.codes/very/long/path/for/demo/purpose");
    await expect(anchor).toHaveText(/baked\.mouseless\.codes\.\.\./);
  });
});
