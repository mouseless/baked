import { fileURLToPath } from "url";
import { join, dirname } from "path";
import { run } from "@mouseless/prebuild";

const configPath = join(dirname(fileURLToPath(import.meta.url)), "config.yml");

run(configPath);
