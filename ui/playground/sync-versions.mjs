import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const rootDir = __dirname;

const [,, resourcePkgArg, targetPkgArg] = process.argv;

if(!resourcePkgArg || !targetPkgArg) {
  process.exit(1);
}

const resourcePath = path.resolve(rootDir, resourcePkgArg);
const targetPath = path.resolve(rootDir, targetPkgArg);

const resourcePkg = JSON.parse(fs.readFileSync(resourcePath, "utf-8"));
const targetPkg = JSON.parse(fs.readFileSync(targetPath, "utf-8"));

const versionMap = {
  ...(resourcePkg.dependencies || {}),
  ...(resourcePkg.devDependencies || {})
};

for(const depType of ["dependencies", "devDependencies"]) {
  const deps = targetPkg[depType] || {};
  for(const [pkg, ver] of Object.entries(deps)) {
    if(ver.startsWith("$")) {
      const realPkgName = ver.slice(1);
      if(versionMap[realPkgName]) {
        deps[pkg] = versionMap[realPkgName];
      }
    }
  }
}

fs.writeFileSync(targetPath, JSON.stringify(targetPkg, null, 2) + "\n");
