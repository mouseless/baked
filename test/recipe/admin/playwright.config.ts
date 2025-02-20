import { fileURLToPath } from "node:url";
import { defineConfig } from "@playwright/test";
import type { ConfigOptions } from "@nuxt/test-utils/playwright";

export default defineConfig<ConfigOptions>({
  testDir: "./pages/specs",
  reporter: "list",
  use: {
    nuxt: {
      rootDir: fileURLToPath(new URL(".", import.meta.url))
    }
  }
});
