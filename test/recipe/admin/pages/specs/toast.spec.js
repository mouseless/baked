import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "~/utils/locators/primevue.js";

test.beforeEach(async({goto}) => {
  await goto("/specs/toast", { waitUntil: "hydration" });
});

const id = primevue.toast.base;

test("toast is configured correctly", async({page}) => {
  const component = page.locator(id);

  await expect(component).toBeVisible();
});

test("visual", { tag: "@visual" }, async({page}) => {
  const component = page.locator(id);

  await expect(component).toHaveScreenshot();
});
