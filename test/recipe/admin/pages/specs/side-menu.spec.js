import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";

test.beforeEach(async({goto}) => {
  await goto("/specs/side-menu", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("default logo source", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("img")).toHaveAttribute("src", "/logo.svg");
  });

  test("item url", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("a")).toHaveAttribute("href", "/menu");
  });

  test("item icon", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.icon)).toHaveClass(/pi pi-heart/);
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Highlight", () => {
  const id = "Highlight";

  test("button color", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("a:first-of-type button")).toHaveClass(/p-button-primary/);
    await expect(component.locator("a:last-of-type button")).toHaveClass(/p-button-secondary/);
  });
});

test.describe("Custom Logo", () => {
  const id = "Custom Logo";

  test("logo source", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("img")).toHaveAttribute("src", "/e5c4p3.png");
  });
});

test.describe("Disabled Item", () => {
  const id = "Disabled Item";

  test("button color", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.base)).toHaveAttribute("disabled");
  });
});
