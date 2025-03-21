<template>
  <!--
    [!NOTE]

    unlike usual model passing, `.model` is not enough here in below. for
    some reason vue rewraps the model which is already a ref, causing a
    double ref. that's why `.model.value` is passed instead of `.model`
  -->
  <Bake
    v-for="queryParameter in queryParameters"
    :key="queryParameter.name"
    v-model="parameters[queryParameter.name].model.value"
    :name="`query-parameters/${queryParameter.name}`"
    :descriptor="queryParameter.component"
  />
</template>
<script setup>
import { computed, ref, onMounted, watch } from "vue";
import { useRoute, useRouter } from "#app";
import Bake from "./Bake.vue";

const { queryParameters } = defineProps({
  queryParameters: { type: Array, required: true }
});

const route = useRoute();
const router = useRouter();

const parameters = {};
for(const queryParameter of queryParameters) {
  const query = computed(() => route.query[queryParameter.name]);
  const model = ref(query.value);

  parameters[queryParameter.name] = { query, model };

  // binds query to model, needed when query parameters chang due to a
  // navigation from side menu or header etc.
  watch(query, newQuery => model.value = newQuery);
}

onMounted(() => {
  if(queryParameters
    .filter(qp => qp.required)
    .map(qp => parameters[qp.name].query)
    .some(q => !q.value)
  ) {
    router.replace({
      path: route.path,
      query: queryParameters.reduce((result, qp) => {
        result[qp.name] = parameters[qp.name].query.value || qp.default;

        return result;
      }, {})
    });
  }
});

watch(Object.values(parameters).map(p => p.model), newValues => {
  const action = queryParameters
    .filter(qp => qp.required)
    .map(qp => parameters[qp.name].query)
    .every(q => q.value)
    ? "push"
    : "replace"
  ;

  router[action]({
    path: route.path,
    query: Object.keys(parameters).reduce((result, name, i) => {
      if(newValues[i]) {
        result[name] = newValues[i];
      }

      return result;
    }, {})
  });
});
</script>
