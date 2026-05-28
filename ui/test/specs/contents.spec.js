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
    await expect(component.getByTestId("content-4")).toHaveText("CONTENT_4");
    await expect(component.getByTestId("content-5")).toHaveText("CONTENT_5");
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

  test("main contents are listed under another div with a fluid width", async({ page }) => {
    const component = page.getByTestId(id);

    const div = component.locator(".b-Contents .w-full");

    await expect(div.getByTestId("content-1")).toBeAttached();
    await expect(div.getByTestId("content-2")).toBeAttached();
    await expect(div.getByTestId("content-3")).toBeAttached();
    await expect(div.getByTestId("content-4")).not.toBeAttached();
    await expect(div.getByTestId("content-5")).not.toBeAttached();
  });

  test("side contents are listed under another div with a fixed width", async({ page }) => {
    const component = page.getByTestId(id);

    const div = component.locator(".b-Contents .w-\\[30rem\\]");

    await expect(div.getByTestId("content-1")).not.toBeAttached();
    await expect(div.getByTestId("content-2")).not.toBeAttached();
    await expect(div.getByTestId("content-3")).not.toBeAttached();
    await expect(div.getByTestId("content-4")).toBeAttached();
    await expect(div.getByTestId("content-5")).toBeAttached();
  });

  test("side contents are split using a divider", async({ page }) => {
    const component = page.getByTestId(id);

    const div = component.locator(".b-Contents .w-\\[30rem\\]");

    await expect(div.locator("> *").nth(1)).toHaveClass(/.p-divider/);
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