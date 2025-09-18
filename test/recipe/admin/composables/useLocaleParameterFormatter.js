import { useLocalization } from "#imports";

export default function() {
  const { localize: l } = useLocalization();

  function format(parameterValue) {
    return l(parameterValue);
  }

  return {
    format
  };
}
