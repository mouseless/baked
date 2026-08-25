export default function({ mutableValidation }) {
  function run({ hint, restrictedValue, newValue } = {}) {
    if(!mutableValidation) { return; }

    if(restrictedValue && newValue === restrictedValue) {
      mutableValidation.setError(`${restrictedValue} is restricted`);
    } else {
      if(hint) {
        mutableValidation.setMessage(hint, { severity: "secondary", icon: "pi pi-lightbulb" });
      } else {
        mutableValidation.clear();
      }
    }
  }

  return {
    run
  };
}