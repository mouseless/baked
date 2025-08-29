import { fileURLToPath } from "node:url";
import { defineConfig, devices } from "@playwright/test";
import type { ConfigOptions } from "@nuxt/test-utils/playwright";

export default defineConfig<ConfigOptions>({
  testDir: "./pages/specs",
  timeout: 2 * 60 * 1000,
  reporter: "list",

  projects: [
    {
      name: "Desktop Chrome",
      use: {
        ...devices["Desktop Chrome"],
        nuxt: {
          rootDir: fileURLToPath(new URL(".", import.meta.url))
        }
      }
    }
  ]
});
