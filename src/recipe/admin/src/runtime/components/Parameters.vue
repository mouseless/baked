<template>
  <!--
    [!NOTE]

    unlike the usual way to pass model, `.model` is not enough here in below.
    for some reason vue rewraps the model which is already a ref, causing a
    double ref. that's why `.model.value` is passed instead of `.model`
  -->
  <div class="flex gap-2">
    <Bake
      v-for="parameter in parameters"
      :key="parameter.name"
      v-model="values[parameter.name].value"
      :name="`parameters/${parameter.name}`"
      :descriptor="parameter.component"
    />
  </div>
</template>
<script setup>
import { defineEmits, onMounted, ref, watch } from "vue";
import Bake from "./Bake.vue";

const { parameters } = defineProps({
  parameters: { type: Array, required: true }
});

const emit = defineEmits(["ready", "changed"]);

const values = {};
for(const parameter of parameters) {
  const model = ref(parameter.default);

  values[parameter.name] = model;
}

// initial emit in case it is already ready using default parameters
emitReady();
emitChanged();

// when any of the parameter values changed from input components, it emits
// ready and changed
watch(Object.values(values), async() => {
  emitReady();
  emitChanged();
});

function emitReady() {
  emit("ready",
    parameters
      .filter(p => p.required)
      .reduce((result, p) => result && values[p.name].value?.length > 0, true)
  );
}

function emitChanged() {
  emit("changed", {
    uniqueKey: Object
      .values(values)
      .map(v => v.value)
      .filter(v => v?.length > 0)
      .join("-"),
    values
  });
}
</script>
