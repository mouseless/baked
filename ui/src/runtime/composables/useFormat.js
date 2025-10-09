import { useRuntimeConfig } from "#app";
import { useLocalization } from "#imports";

export default function() {
  const { locale: currentLocale, localize: lc } = useLocalization({ group: "useFormat" });
  const { public: { composables } } = useRuntimeConfig();

  const locale = currentLocale.value || "en";
  const currency = composables?.useFormat?.currency || "USD";
  const B = lc("suffix.B"); // billions
  const M = lc("suffix.M"); // millions
  const K = lc("suffix.K"); // thousands

  const STAGES = [
    { threshold: 1_000_000_000, divisor: 1_000_000_000, suffix: B, fraction: true },
    { threshold: 1_000_000, divisor: 1_000_000, suffix: M, fraction: true },
    { threshold: 1_000, divisor: 1_000, suffix: K, fraction: true },
    { threshold: 999.9999, divisor: 1, suffix: "", fraction: false },
    { threshold: Number.MIN_VALUE, divisor: 1, suffix: "", fraction: true }
  ];

  function asClasses(path,
    prefix = "b--"
  ) {
    return path.split("/")
      .filter(item => item.length > 0)
      .map(item => `${prefix}${item}`);
  }

  function asCurrency(value,
    { shorten, shortenThousands } = { }
  ) {
    return asNumber(value, {
      shorten,
      shortenThousands,
      formatOptions: {
        style: "currency",
        currency
      }
    }) ;
  }

  function asDecimal(value) {
    value ||= "-";

    return value.toLocaleString(locale, { style: "decimal", maximumFractionDigits: 2 });
  }

  function asMonth(date) {
    return date.toLocaleString(locale, { month: "long" });
  }

  function asNumber(value,
    { shorten, shortenThousands, formatOptions } = { }
  ) {
    if(!value) { return "-"; }

    shorten ??= true;
    shortenThousands ??= false;
    formatOptions ??= { };

    const stage = shorten
      ? STAGES.find(s => (shortenThousands || s.threshold !== 1_000) && Math.abs(value) >= s.threshold) ?? STAGES[STAGES.length - 1]
      : STAGES[STAGES.length - 1];
    const shownValue = value / stage.divisor;

    const fractionDigitCount = stage.fraction ? 2 : 0;
    const formattedResult = shownValue.toLocaleString(locale, {
      maximumFractionDigits: fractionDigitCount,
      trailingZeroDisplay: "stripIfInteger",
      ...formatOptions
    });

    return {
      shortened: stage.suffix !== "",
      toString: () => formattedResult + stage.suffix
    };
  }

  function asPercentage(value) {
    value ||= "-";

    return value.toLocaleString(locale, { style: "percent", maximumFractionDigits: 2 });
  }

  function format(formatString, args) {
    // TODO: this format call is temporary, final design should handle path
    // variables using name, not index, e.g., /test/{0} -> /test/{id}
    return formatString.replace(/(\{\{\d\}\}|\{\d\})/g, part => {
      if(part.substring(0, 2) === "{{") { return part; } // escape

      const index = parseInt(part.match(/\d/)[0]);

      return args[index];
    });
  }

  function truncate(value, length) {
    if(!value || !length || value.length <= length - 3) {
      return value;
    }

    return value.substring(0, length - 3).concat("...");
  }

  return {
    asClasses,
    asCurrency,
    asDecimal,
    asMonth,
    asNumber,
    asPercentage,
    format,
    truncate
  };
};
