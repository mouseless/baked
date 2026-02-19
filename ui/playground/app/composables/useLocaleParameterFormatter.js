import { useLocalization } from "#imports";

export default function() {
  const { localize: l } = useLocalization();

  function format(parameterValue) {
    if(!isNaN(Number(parameterValue))) {
      return parameterValue;
    }

    return l(parameterValue);
  }

  return {
    format
  };
}
