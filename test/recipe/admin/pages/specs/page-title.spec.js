import { expect, test } from "@nuxt/test-utils/playwright";
import giveMe from "~/utils/giveMe";

test.beforeEach(async({goto}) => {
  await goto("/specs/page-title", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("title", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.locator("h1")).toHaveText("Title");
  });

  test("description visibility based on screen size", async({page}) => {
    const component = page.getByTestId(id);
    const description = component.getByTestId("description");
    const infoIcon = component.locator(".pi-info-circle");

    // Check desktop view (2xl screen)
    const desktop = giveMe.aScreenSize({name: "2xl"});
    await page.setViewportSize({ ...desktop });
    await expect(description).toBeVisible();
    await expect(infoIcon).toBeHidden();

    // Check tablet view (lg screen)
    const tablet = giveMe.aScreenSize({name: "lg"});
    await page.setViewportSize({ ...tablet });
    await expect(description).toBeHidden();
    await expect(infoIcon).toBeVisible();

    // Verify tooltip appears on hover
    await infoIcon.hover();
    await expect(component.locator(".b-tooltip")).toBeVisible();
    await expect(component.locator("p")).toContainText("Description");

    // Check mobile view (sm screen)
    const mobile = giveMe.aScreenSize({name: "sm"});
    await page.setViewportSize({ ...mobile });
    await expect(description).toBeHidden();
    await expect(infoIcon).toBeVisible();
  });
});

test.describe("Actions", () => {
  const id = "Actions";

  test("actions", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("ACTION_1")).toHaveText("VALUE_1");
    await expect(component.getByTestId("ACTION_2")).toHaveText("VALUE_2");
  });

  test("visual", { tag: "@visual" }, async({page}) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveScreenshot();
  });
});

test.describe("No Description", () => {
  const id = "No Description";

  test("description still available with nbsp", async({page}) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("description")).toHaveText(" ");
  });
});
