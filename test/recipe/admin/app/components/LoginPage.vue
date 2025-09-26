<template>
  <form
    class="w-96 flex justify-center flex-col gap-4 px-4 max-2xs:w-64"
    @submit.prevent="submit"
  >
    <div class="w-full text-center mb-6">
      <RouterLink to="/" class="inline-block">
        <Logo src="logo-full.svg"/>
      </RouterLink>
    </div>
    <div class="text-center">
      <strong class="block text-2xl mb-2">
        {{ lc("Admin Recipe - Test login") }}
      </strong>
      <div class="text-gray-600 dark:text-gray-400">
        {{ lc("Enter any username to login") }}
      </div>
    </div>
    <InputText
      v-model="username"
      :invalid="submitted && !username"
      type="text"
      :placeholder="lc('Username')"
      :class="{ 'animate-shake': submitted && !username }"
    />
    <Button
      type="submit"
      class="mt-4"
      :label="lc('Login')"
    />
  </form>
  <Divider />
  <div>
    <Button
      icon="pi pi-home"
      :label="lc('Back to Home Page')"
      severity="secondary"
      variant="outlined"
      as="router-link"
      to="/"
    />
  </div>
</template>
<script setup>
import { onUpdated, ref } from "vue";
import { RouterLink } from "vue-router";
import { Button, Divider, InputText } from "primevue";
import { useRuntimeConfig } from "#app";
import { Logo } from "#components";
import { useToken, useLocalization } from "#imports";

defineProps({
  schema: { type: null, default: null },
  data: { type: null, default: null }
});
defineModel({ type: null, default: null });

const { localize: lc } = useLocalization({ group: "Login" });
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
