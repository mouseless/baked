import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue";
import tailwindcss from "~/utils/locators/tailwindcss";

test.describe("Detail", () => {
  test.beforeEach(async({goto}) => {
    await goto("/specs/Detail.spec", { waitUntil: "hydration" });
  });

  test.describe("Basic", () => {
    const id = "Basic";

    test("panel title", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component.locator(primevue.panel.title)).toHaveText("TITLE TEXT");
    });

    test("header", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component.getByTestId("header")).toHaveText("HEADER TEXT");
    });

    test("props", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component.getByTestId("prop1")).toHaveText("PROP1 VALUE");
      await expect(component.getByTestId("prop2")).toHaveText("PROP2 VALUE");
    });

    test("visual", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component).toHaveScreenshot();
    });
  });

  test.describe("Null", () => {
    const id = "Null";

    test("panel title", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component.locator(primevue.panel.title)).not.toBeVisible();
    });

    test("header", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component.getByTestId("header")).not.toBeVisible();
    });

    test("props", async({page}) => {
      const component = page.getByTestId(id);

      await expect(component.locator(tailwindcss.grid)).not.toBeVisible();
    });
  });
});

