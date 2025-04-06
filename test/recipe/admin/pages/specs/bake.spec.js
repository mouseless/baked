import { expect, test } from "@nuxt/test-utils/playwright";

test.beforeEach(async({goto}) => {
  await goto("/specs/card-link", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("route", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("a")).toHaveAttribute("href", "/some-route");
  });
});
