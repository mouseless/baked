import { computed } from "vue";
import { useComposableResolver } from "#imports";

export default function useValidate({ formData, inputData, validateComposable = [], includeDefault = false }) {
  const composableResolver = useComposableResolver();

  const composableKeys = includeDefault
    ? ["useValidateDefault", ...validateComposable]
    : [...validateComposable];

  const validators = composableKeys.map(vc => composableResolver.resolve(vc).default);

  const validations = computed(() =>
    validators.reduce((acc, validation) => ({
      ...acc,
      ...validation({
        inputData: inputData.value,
        formData: formData.value
      })
    }), {})
  );

  const isValid = computed(() =>
    Object.values(validations.value).every(v => v.valid)
  );

  const messages = computed(() =>
    Object.values(validations.value)
      .filter(v => v.message)
      .map((v, i) => `${i > 0 ? "\n" : ""} - ${v.message}`)
      .join("")
  );

  return { isValid, messages, validations };
}