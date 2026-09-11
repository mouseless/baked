import { useValidation, useLocalization } from "#imports";

export default function() {
  const { localize: lc } = useLocalization({ group: "useValidator" });
  const validation = useValidation();

  const mutable = validation.injectMutable();

  return {
    url: WithMutable(Url(lc), mutable)
  };
}

function WithMutable(validator, mutable) {
  function validate(value, { silent = false } = {}) {
    const valid = validator.validate(value);
    if(silent) { return valid; }

    if(valid || !value) {
      mutable.clear();
    } else {
      mutable.setError(validator.message);
    }

    return valid;
  }

  return {
    message: validator.message,
    validate
  };
}

function Url(lc) {
  const message = lc("Invalid URL");

  function validate(value) {
    // backend adds trailing "/" to absolute url when no query string -> account for that extra char
    const effectiveLength = value?.length + (!value?.includes("?") && !value?.endsWith("/") ? 1 : 0);
    if(effectiveLength > 255) { return false; }

    try {
      const url = new URL(value);

      // don't accept whitespace on host part
      if(url.host.includes("%20")) { return false; }

      // "test.com." -> "test.com" -> ["test", "com"]
      const parts = url.hostname.replace(/\.$/, "").split(".");

      // "www.xxx" likely missing TLD -> require 3 parts when starts with www
      const minParts = parts[0] === "www" ? 3 : 2;

      // protocol must be http/https/ftp, TLD must be bigger than one character
      return (url.protocol === "http:" || url.protocol === "https:" || url.protocol === "ftp:") &&
        parts.length >= minParts &&
        parts[parts.length - 1].length >= 2;
    } catch { return false; }
  }

  return {
    message,
    validate
  };
}
