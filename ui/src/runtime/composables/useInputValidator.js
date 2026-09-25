import { useValidation, useLocalization } from "#imports";

export default function() {
  const { localize: lc } = useLocalization({ group: "useInputValidator" });
  const validation = useValidation();

  const mutable = validation.injectMutable();

  return {
    date: WithMutable(Date(lc), mutable),
    mailAddress: WithMutable(MailAddress(lc), mutable),
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

function Date(lc) {
  const message = lc("Invalid date");

  function validate(value) {
    if(value?.length != 8) { return false; }
    if(isNaN(value)) { return false; }

    const day = Number(value.slice(0, 2));
    const month = Number(value.slice(2, 4));
    const year = Number(value.slice(4, 8));

    if(year < 1000 || year > 9999) { return false; }
    if(month < 1 || month > 12) { return false; }
    if(day < 1 || day > 31) { return false; }

    // edge cases like Feb 30, Apr 31
    const date = new globalThis.Date(year, month - 1, day);

    return date.getFullYear() === year &&
      date.getMonth() === month - 1 &&
      date.getDate() === day;
  }

  return {
    message,
    validate
  };
}

function MailAddress(lc) {
  const message = lc("Invalid e-mail address");
  const regex = "/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9][a-zA-Z0-9.-]*[a-zA-Z0-9]\\.[a-zA-Z]{2,}$/";
  const parts = regex.match(/^\/(.*)\/([gimsuy]*)$/);

  function validate(value) {
    return new RegExp(parts[1], parts[2]).test(value);
  }

  return {
    message,
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
