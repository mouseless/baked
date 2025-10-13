export default function() {

  function find(conditional, data) {
    const conditions = conditional.conditions.filter(component => data[component.prop] === component.value);
    if(conditions.length > 0) {
      return conditions[0].component;
    }

    return conditional.fallback;
  }

  return {
    find
  };
}
