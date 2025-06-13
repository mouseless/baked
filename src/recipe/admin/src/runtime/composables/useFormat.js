import { useRuntimeConfig } from "#app";

export default function() {
  const { public: { composables } } = useRuntimeConfig();

  const locale = composables?.useFormat?.locale || "en-US";
  const currency = composables?.useFormat?.currency || "USD";
  const suffix = composables?.useFormat?.suffix || { billions: "B", millions: "M", thousands: "K" };

  const STAGES = [
    { threshold: 1_000_000_000, divisor: 1_000_000_000, suffix: suffix.billions, fraction: true },
    { threshold: 1_000_000, divisor: 1_000_000, suffix: suffix.millions, fraction: true },
    { threshold: 1_000, divisor: 1_000, suffix: suffix.thousands, fraction: true },
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
      ? STAGES.find(s => (shortenThousands || s.threshold !== 1_000) && value >= s.threshold) ?? STAGES[STAGES.length - 1]
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
    truncate
  };
};
