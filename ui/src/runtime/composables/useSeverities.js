import * as severities from "@@/.baked/severities.json";

export default function() {
  function compute({ name } = {}) {
    return severities.default[name];
  }

  return {
    compute
  };
}