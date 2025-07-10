import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../../utils/locators/primevue.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/side-menu", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("default logo source", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("img")).toHaveAttribute("src", "/logo.svg");
  });

  test("logo links to home", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("a").nth(0)).toHaveAttribute("href", "/");
  });

  test("item url", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("a").nth(1)).toHaveAttribute("href", "/menu");
  });

  test("item icon", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.button.icon)).toHaveClass(/pi pi-heart/);
  });

  test("footer", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("footer")).toHaveText("FT");
  });

  test("localized tooltip", async({page}) => {
    const component = page.getByTestId(id);

    await component.hover();

    await expect(page.locator(primevue.tooltip.right)).toBeAttached();
    await expect(page.locator(primevue.tooltip.right)).toBeVisible();
    await expect(page.locator(primevue.tooltip.right)).toHaveText("Title");
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

    await expect(component.locator("button").nth(0)).toHaveClass(/p-button-primary/);
    await expect(component.locator("button").nth(1)).toHaveClass(/p-button-secondary/);
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
