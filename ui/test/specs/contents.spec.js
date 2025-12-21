import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "../utils/giveMe";

test.beforeEach(async({ goto }) => {
  await goto("/specs/contents", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("contents", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content-1")).toHaveText("CONTENT_1");
    await expect(component.getByTestId("content-2")).toHaveText("CONTENT_2");
    await expect(component.getByTestId("content-3")).toHaveText("CONTENT_3");
  });

  test("width is restricted", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".max-w-screen-xl")).toBeAttached();
  });

  test("narrow contents don't span to columns on large screen", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("content-1")).not.toHaveClass(/lg:col-span-2/);
    await expect(component.getByTestId("content-2")).not.toHaveClass(/lg:col-span-2/);
    await expect(component.getByTestId("content-3")).toHaveClass(/lg:col-span-2/);
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Full Screen", () => {
  const id = "Full Screen";

  test("width is not restricted", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".max-w-screen-xl")).not.toBeAttached();
  });

  test("visual", { tag: "@visual" }, async({ page }) => {
    const component = page.getByTestId(id);
    const screen = giveMe.aScreenSize({ name: "3xl" });

    await page.setViewportSize({ ...screen });

    await expect(component).toHaveScreenshot();
  });
});
