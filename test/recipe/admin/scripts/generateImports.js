import { writeFileSync } from "fs";
import { resolve } from "path";
import os from "os";
import * as PrimeVue from "primevue";

function main() {
  let content = `<template>${os.EOL}`;
  for(const component in PrimeVue) {
    if(!startsWithCapital(component)) { continue; }

    content += `  <${component} />${os.EOL}`;
  }
  content += "</template>";

  writeFileSync(resolve("components/.importPrimeVue.vue"), content);
}

function startsWithCapital(str) {
  return /^[A-Z]/.test(str);
}

main();
