<template>
  <form
    class="w-96 flex justify-center flex-col gap-4 px-4"
    @submit.prevent="submit"
  >
    <div class="w-full text-center mb-6">
        <RouterLink to="/"><img src="/logo.svg" class="mx-auto w-10"></RouterLink>
    </div>
    <div class="text-center">
      <strong class="block text-2xl mb-2">
        Admin Recipe - Test Login
      </strong>
      <div class="text-gray-600 dark:text-gray-400">
        Enter any username to login.
      </div>
    </div>
    <InputText
      v-model="username"
      :invalid="submitted && !username"
      type="text"
      placeholder="Username"
      :class="{ 'animate-shake': submitted && !username }"
    />
    <Button
      type="submit"
      class="mt-4"
      label="Login"
    />
  </form>
</template>
<script setup>
import { onUpdated, ref } from "vue";
import { RouterLink } from "vue-router";
import { useRuntimeConfig } from "#app";
import { useToken } from "#imports";
import { Button, InputText } from "primevue";

const { public: { composables } } = useRuntimeConfig();
const token = useToken();

const username = ref();
const submitted = ref(false);

onUpdated(() => submitted.value = false);

async function submit() {
  submitted.value = true;

  const result = await $fetch("/authentication-samples/login",
    {
      baseURL: composables.useDataFetcher.baseURL,
      method: "POST",
      body: { username: username.value }
    });

  if(!result) {
    return;
  }

  token.setCurrent(result);
}
</script>
