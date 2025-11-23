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
  const requests = [];

  page.on("request", request => {
    if(request.url().includes("time-provider-samples/now")) {
      requests.push(request);
    }
  });

  const token = giveMe.aToken({ expiresInSeconds: 3 });

  await mockMe.theSession(page, token);
  await goto("/specs/auth", { waitUntil: "hydration" });

  await page.getByTestId("request").click();
  await page.waitForTimeout(3500);

  await page.getByTestId("request").click();
  await page.waitForTimeout(100);

  expect(requests.length).toBe(2);
  expect(requests[0].headers()["authorization"]).toBeDefined();
  expect(requests[1].headers()["authorization"]).toBeDefined();
  expect(requests[0].headers()["authorization"]).not.toBe(requests[1].headers()["authorization"]);
});
