import { expect, test } from "@nuxt/test-utils/playwright";
import primevue from "../utils/locators/primevue";

test.beforeEach(async({ goto, page }) => {
  await page.route("*/**/route-parameters-samples/*", async route => {
    await route.fulfill({ body: "fake-response" });
  });
  await page.route("*/**/rich-transient-with-datas/12/method", async route => {
    await route.fulfill({ body: "fake-response" });
  });
  await page.route("*/**/method-samples/async", async route => {
    await route.fulfill({ body: "fake-response" });
  });
  await goto("/specs/bake?val=2", { waitUntil: "hydration" });
});

test.describe("Base", () => {
  const id = "Base";

  test("component render", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component).toHaveText("TEST");
  });

  test("component has marker class for bake path", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b--variants.b--base")).toBeAttached();
  });

  test("component has marker class for component type", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.locator(".b-component--Expected")).toBeAttached();
  });
});

test.describe("Parent Data", () => {
  const id = "Parent Data";

  test("reads data from parent", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("child-root"))
      .toHaveText(`
        {
          "child": "CHILD VALUE"
        }`
      );
  });

  test("reads data from parent using given prop", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("child-prop")).toHaveText("CHILD VALUE");
  });
});

test.describe("Data Descriptor", () => {
  const id = "Data Descriptor";

  test("provides data parameters to child", async({ page }) => {
    const component = page.getByTestId(id);

    await expect(component.getByTestId("test")).toHaveText(/computed/);
    await expect(component.getByTestId("test")).toHaveText(/RequiredWithDefault1/);
    await expect(component.getByTestId("test")).toHaveText(/Required1/);
  });

  test("builds path with given params data", async({ page }) => {
    const request = await page.waitForRequest(req => req.url().includes("/route-parameters-samples"));
    expect(request.url()).toContain("/route-parameters-samples/7b6b67bb-30b5-423e-81b4-a2a0cd59b7f9");
  });
});

test.describe("Model", () => {
  const id = "Model";

  test("model is passed when a component defines model", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputeText.base);

    await expect(input).toHaveValue("Model Data");
  });

  test("model value can be updated", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.locator(primevue.inputeText.base);
    const model = page.getByTestId(`${id}:model`);

    await input.fill("Test");

    await expect(model).toHaveText("Test");
  });
});

test.describe("Action", () =>{
  const id = "Action";

  test("Execute given composite action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base)).toBeVisible();
    await expect(page.locator(primevue.toast.summary)).toHaveText("Execute Action");

    const request = await page.waitForRequest(req => req.url().includes("rich-transient-with-datas"));
    expect(request.method()).toBe("POST");
    expect(request.headers()["authorization"]).toContain("token-admin-ui");
    expect(request.url()).toContain("/rich-transient-with-datas/12/method");
    expect(request.url()).toContain("?val=2");
    expect(request.postDataJSON()).toEqual({ text: "text" });
  });

  test("Execute given remote post action", async({ page }) => {
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await expect(page.locator(primevue.toast.base).last()).toBeVisible();
    await expect(page.locator(primevue.toast.summary).last()).toHaveText("Execute Post Action");
  });
});

test.describe("Reaction", () =>{
  const id = "Reaction";

  // Initial data load completes before this text execution because page is
  // waited till hydration, request count is expected to be one. To catch if
  // reload occured below code is used
  // ```js
  // let reloaded = false;
  // page.on("request", req => {
  //   if(req.url().includes("method-samples/async")) {
  //     reloaded = true;
  //   }
  // });
  // ```

  test("Reload reaction with composite and emit triggers", async({ page }) => {
    let reloaded = false;
    page.on("request", req => {
      if(req.url().includes("method-samples/async")) {
        reloaded = true;
      }
    });
    const component = page.getByTestId(id);
    const button = component.locator(primevue.button.base);

    await button.click();

    await page.waitForLoadState("networkidle");
    expect(reloaded).toBe(true);
  });

  test("Reaction is filtered out when emitted value doesn't match constraint", async({ page }) => {
    let reloaded = false;
    page.on("request", req => {
      if(req.url().includes("method-samples/async")) {
        reloaded = true;
      }
    });
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");

    await input.fill("something else");

    await page.waitForLoadState("networkidle");
    expect(reloaded).toBe(false);
  });

  test("Reaction occurs when emitted value matches constraint", async({ page }) => {
    let reloaded = false;
    page.on("request", req => {
      if(req.url().includes("method-samples/async")) {
        reloaded = true;
      }
    });
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");

    await input.fill("emit");

    await page.waitForLoadState("networkidle");
    expect(reloaded).toBe(true);
  });

  test("Page context action and trigger", async({ page }) => {
    let reloaded = false;
    page.on("request", req => {
      if(req.url().includes("method-samples/async")) {
        reloaded = true;
      }
    });
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");

    await input.fill("page-context");

    await page.waitForLoadState("networkidle");
    expect(reloaded).toBe(true);
  });

  test("Composable constraint", async({ page }) => {
    let reloaded = false;
    page.on("request", req => {
      if(req.url().includes("method-samples/async")) {
        reloaded = true;
      }
    });
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");

    await input.fill("validate");

    await page.waitForLoadState("networkidle");
    expect(reloaded).toBe(true);
  });

  test("Show/hide reaction with isNot constraint", async({ page }) => {
    const component = page.getByTestId(id);
    const input = component.getByTestId("input");
    await expect(component.getByTestId("output")).toBeAttached();

    await input.fill("hide");

    await page.waitForLoadState("networkidle");
    await expect(component.getByTestId("output")).not.toBeAttached();
  });
});
