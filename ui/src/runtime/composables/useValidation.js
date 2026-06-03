import { computed } from "vue";
import { useContext, useComposableResolver } from "#imports";

export default function() {
  const context = useContext();
  const composableResolver = useComposableResolver();

  function validate({ model, inputs, composables = [] }) {
    const validationComposables = ["useDefaultValidation", ...composables.map(c => c.name)].map(vc => {
      try {
        return composableResolver.resolve(vc).default;
      } catch {
        console.error(`${vc} not loaded`);

        return;
      }
    }).filter(v => v);

    const validations = computed(() =>
      validationComposables.reduce((acc, useComposable) => ({
        ...acc,
        ...useComposable({
          inputs,
          model: model.value
        })
      }), {})
    );
    const mutableValidations = {};

    context.provideValidations(validations);
    context.provideMutableValidations(mutableValidations);

    const isValid = computed(() =>
      Object.values(validations.value).every(v => v.valid) &&
      Object.values(mutableValidations.value).every(v => v.valid)
    );

    const messages = computed(() =>
      [...Object.values(validations.value), ...Object.values(mutableValidations).map(v => v.value)]
        .filter(v => v.message)
        .map((v, i) => `${i > 0 ? "\n" : ""} - ${v.message}`)
        .join("")
    );

    return { isValid, messages, validations };
  }

  function injectMutable() {
    const ref = context.injectMutableValidation();
    if(!ref) { return null; }

    return MutableValidation(ref);
  }

  return {
    validate,
    injectMutable
  };
}

function MutableValidation(ref) {
  function clear() {
    ref.valid = true;
    delete ref.message;
    delete ref.severity;
  }

  function setError(message) {
    ref.valid = false;
    ref.message = message;
    ref.severity = "error";
  }

  return {
    clear,
    setError
  };
}