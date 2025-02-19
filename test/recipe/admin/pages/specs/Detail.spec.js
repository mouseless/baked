import { expect, test } from "@nuxt/test-utils/playwright";
import { primeVue } from "~/utils/locators.js";

const id = "Basic";

test.describe("basic", () => {
  test.beforeEach(async({goto}) => {
    await goto("/specs/Detail.spec", { waitUntil: "hydration" });
  });

  test("panel title", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator(primeVue.panel.title)).toHaveText("TITLE TEXT");
  });

  test("header", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("header")).toHaveText("HEADER TEXT");
  });
});

