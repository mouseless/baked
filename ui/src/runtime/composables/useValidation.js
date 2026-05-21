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

    context.provideValidations(validations);

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

  return {
    validate
  };
}