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
    { threshold: Number.MIN_VALUE, divisor: 1, suffix: "", fraction: false }
  ];

  function asCurrency(value,
    { shorten, shortenThousands } = { }
  ) {
    if(!value) { return "-"; }

    shorten ??= true;
    shortenThousands ??= false;

    const stage = shorten
      ? STAGES.find(s => (shortenThousands || s.threshold !== 1_000) && value >= s.threshold) ?? STAGES[STAGES.length - 1]
      : STAGES[STAGES.length - 1];
    const shownValue = value / stage.divisor;

    let formattedResult = shownValue.toLocaleString(locale, {
      style: "currency",
      currency,
      maximumFractionDigits: stage.fraction ? 2 : 0
    });
    if(stage.fraction && formattedResult.endsWith("00")) {
      formattedResult = formattedResult.substring(0, formattedResult.length - 3);
    }

    return {
      shortened: stage.suffix !== "",
      toString: () => formattedResult + stage.suffix
    };
  }

  function asMonth(date) {
    return date.toLocaleString(locale, { month: "long" });
  }

  function asPercentage(value) {
    value ||= "-";

    return value.toLocaleString(locale, { style: "percent", maximumFractionDigits: 2 });
  }

  function asDecimal(value) {
    value ||= "-";

    return value.toLocaleString(locale, { style: "decimal", maximumFractionDigits: 2 });
  }

  return {
    asCurrency,
    asMonth,
    asPercentage,
    asDecimal
  };
};
