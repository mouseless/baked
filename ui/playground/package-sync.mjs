import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const rootDir = __dirname;

const [,, source, target] = process.argv;

if(!source || !target) {
  process.exit(1);
}

const sourcePath = path.resolve(rootDir, source);
const targetPath = path.resolve(rootDir, target);

const sourcePkg = JSON.parse(fs.readFileSync(sourcePath, "utf-8"));
const targetPkg = JSON.parse(fs.readFileSync(targetPath, "utf-8"));

const versionMap = {
  ...(sourcePkg.dependencies || {}),
  ...(sourcePkg.devDependencies || {})
};

copyVersions("dependencies");
copyVersions("devDependencies");

fs.writeFileSync(targetPath, JSON.stringify(targetPkg, null, 2));

function copyVersions(depType) {
  if(!targetPkg[depType]) { return; }

  for(const [pkg, ver] of Object.entries(targetPkg[depType])) {
    if(!ver.startsWith("$")) { continue; }
    if(!versionMap[pkg]) { continue; }

    targetPkg[depType][pkg] = versionMap[pkg];
  }
}