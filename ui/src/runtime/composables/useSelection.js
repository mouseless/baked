import { ref, watch } from "vue";

export default function useSelection({
  data,
  model,
  path,
  stateStore,
  stateful,
  targetProp,
  mode = "single"
}) {
  const isMulti = mode === "multi";
  const selected = ref(isMulti ? [] : undefined);

  function getModel() {
    if(isMulti) {
      return targetProp
        ? model.value?.map(selection => selection[targetProp])
        : model.value;
    }

    return targetProp
      ? model.value?.[targetProp]
      : model.value;
  }

  function setModel(selected) {
    let value = selected;

    if(isMulti && !selected?.length) {
      value = undefined;
    }

    if(stateful) {
      stateStore[path] = value;
    }

    if(isMulti) {
      if(arrayEquals(value, getModel())) { return; }

      model.value = value
        ? targetProp
          ? value.map(item => ({ [targetProp]: item }))
          : value
        : undefined;

      return;
    }

    model.value = value
      ? targetProp
        ? { [targetProp]: value }
        : value
      : undefined;
  }

  watch(
    [() => data, getModel],
    ([_data, _model]) => {
      if(!_data) { return; }

      selected.value = stateful ? (stateStore[path] ?? _model) : _model ?? null;

      if(stateful && !hasChanges(selected.value)) {
        setModel(selected.value);
      }
    },
    { immediate: true }
  );

  watch(selected, setModel);

  function arrayEquals(a, b) {
    return a?.length === b?.length && a?.every((value, index) => value === b[index]);
  }

  function hasChanges(value) {
    return isMulti ? arrayEquals(value, getModel()) : value === getModel();
  }

  return {
    selected
  };
}
