import { spawn } from "child_process";
import { defineNuxtModule } from "@nuxt/kit";
import { resolve } from "path";

export default defineNuxtModule({
  setup(_, nuxt) {
    nuxt.hook("vite:extendConfig", (config, { isClient }) => {
      if(isClient) { return; }

      config.plugins?.push({
        name: "prebuild",
        configureServer(server) {
          const { watcher } = server;
          const rootPath = resolve(nuxt.options.rootDir, "../");

          watcher.add(rootPath);
          watcher.on("change", file => {
            if(file.includes(".theme")) { return; }
            if(!file.endsWith(".md")) { return; }

            const child = spawn("node", ["-r", "dotenv/config", "prebuild", "dotenv_config_path=.env.local"]);

            child.stdout.on("data", data => {
              console.log(`${data}`);
            });

            child.stderr.on("data", data => {
              console.error(`${data}`);
            });
          });
        }
      });
    });
  }
});