import { fileURLToPath } from "url";
import { join, dirname } from "path";
import { run } from "./.prebuild/index.js";

const configPath = join(dirname(fileURLToPath(import.meta.url)), "config.yml");

run(configPath);
