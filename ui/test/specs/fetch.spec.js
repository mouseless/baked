import { expect, test } from "@nuxt/test-utils/playwright";
import mockMe from "../utils/mockMe";
import giveMe from "../utils/giveMe";

test.beforeEach(async({ goto, page }) => {
  await goto("/specs", { waitUntil: "hydration" });
  await page.route("*/**/time-provider-samples/now", async route => {
    const json = Date.now();
    await route.fulfill({ json });
  });
});

test("refresh is executed for requests sent with an expired token", async({ goto, page }) => {
  const requestPromise = page.waitForRequest(req => req.url().includes("time-provider-samples/now"));
  const token = giveMe.aToken();
  await mockMe.theSession(page, token);
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("request").click();

  const request = await requestPromise;
  expect(request.headers()["authorization"]).toBe("NOT IMPLEMENTED YET");
});
