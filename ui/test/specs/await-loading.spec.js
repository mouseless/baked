// import { expect, test } from "@nuxt/test-utils/playwright";
//
// test.beforeEach(async({ goto }) => {
//   await goto("/specs/await-loading", { waitUntil: "hydration" });
// });
//
// test.describe("Base", () => {
//   const id = "Base";
//
//   test.skip("hides content while loading", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("shows skeleton in place", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test("visual", { tag: "@visual" }, async({ page }) => {
//     const component = page.getByTestId(id);
//
//     await expect(component).toHaveScreenshot();
//   });
// });
//
// test.describe("Loaded", () => {
//   const id = "Loaded";
//
//   test.skip("shows content", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("hides skeleton", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("props are forwarded to component", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
// });
//
// test.describe("Overridden", () => {
//   const id = "Overridden";
//
//   test.skip("skeleton width and height", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("visual", { tag: "@visual" }, async({ page }) => {
//     const component = page.getByTestId(id);
//
//     await expect(component).toHaveScreenshot();
//   });
// });
//
// test.describe("Error", () => {
//   const id = "Error";
//
//   test.skip("error summary in body", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("summary and detail in tooltip", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("error claimed", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("visual", { tag: "@visual" }, async({ page }) => {
//     const component = page.getByTestId(id);
//
//     await expect(component).toHaveScreenshot();
//   });
//
//   test.skip("visual (tooltip)", { tag: "@visual" }, async({ page }) => {
//     const component = page.getByTestId(id);
//
//     await expect(component).toHaveScreenshot();
//   });
// });
//
// test.describe("Customized Error", () => {
//   const id = "Customized Error";
//
//   test.skip("skeleton styles in error element", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
//
//   test.skip("visual", { tag: "@visual" }, async({ page }) => {
//     const component = page.getByTestId(id);
//
//     await expect(component).toHaveScreenshot();
//   });
// });
//
// test.describe("Overridden Error", () => {
//   const id = "Overridden Error";
//
//   test.skip("uses template", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
// });
//
// test.describe("No Error", () => {
//   const id = "No Error";
//
//   test.skip("does not show error nor content", async({ page }) => {
//     const component = page.getByTestId(id);
//
//     console.error("not implemented");
//   });
// });