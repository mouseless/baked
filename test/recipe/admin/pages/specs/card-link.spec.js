import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../../utils/locators/primevue.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/card-link", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("route", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("a")).toHaveAttribute("href", "/some-route");
  });

  test("icon", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".pi")).toHaveClass(/pi-wave-pulse/);
  });

  test("title", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h2")).toHaveText("CARD TITLE");
  });

  test("description", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).toHaveText("CARD DESCRIPTION");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("Only Mandatory Fields", () => {
  const id = "Only Mandatory Fields";

  test("no icon", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("pi")).not.toBeAttached();
  });

  test("no description", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).not.toBeAttached();
  });
});

test.describe("Disabled", () => {
  const id = "Disabled";

  test("disabled button", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("button")).toHaveAttribute("data-p-disabled", "true");
  });

  test("disabled with a reason notice", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primevue.tag.base)).toHaveText("SOON");
  });
});
