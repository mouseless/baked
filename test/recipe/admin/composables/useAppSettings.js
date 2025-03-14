import fs from "node:fs";
import path from "node:path";

export default function() {
  const settingsJson = path.resolve(".baked/app.settings.json");

  return JSON.parse(fs.readFileSync(settingsJson, "utf8"));
}