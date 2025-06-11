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
        {{ l("Admin_Recipe__Test_login") }}
      </strong>
      <div class="text-gray-600 dark:text-gray-400">
        {{ l("Enter_any_username_to_login") }}
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
      :label="l('Login')"
    />
  </form>
  <Divider />
  <Button
    icon="pi pi-home"
    :label="l('Back_to_Home_Page')"
    severity="secondary"
    variant="outlined"
    as="router-link"
    to="/"
  />
</template>
<script setup>
import { onUpdated, ref } from "vue";
import { RouterLink } from "vue-router";
import { useRuntimeConfig } from "#app";
import { useToken, useLocalization } from "#imports";
import { Button, Divider, InputText } from "primevue";

defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});
defineModel({ type: null, default: null });

const { localize: l } = useLocalization("Login");
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
