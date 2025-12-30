<template>
  <div class="flex flex-col gap-8">
    <PageTitle :schema="title">
      <template
        v-if="inputs.length > 0"
        #actions
      >
        <Button
          :schema="submit"
          :ready
          @submit="onSubmit"
        />
      </template>
    </PageTitle>
    <div class="flex justify-center">
      <Contents>
        <Inputs
          v-if="inputs"
          :inputs="inputs"
          :input-class="`w-full lg:col-span-2`"
          @ready="onReady"
          @changed="onChanged"
        />
      </Contents>
    </div>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Button, Contents, Inputs, PageTitle } from "#components";

const { schema } = defineProps({
  schema: { type: null, required: true }
});
const emit = defineEmits(["submit"]);

const { title, submit, inputs } = schema;

const formData = ref({});
const ready = ref(inputs.length === 0);

function onReady(value) {
  ready.value = value;
}

function onChanged({ values }) {
  formData.value = values;
}

function onSubmit() {
  emit("submit", formData.value);
}
</script>
