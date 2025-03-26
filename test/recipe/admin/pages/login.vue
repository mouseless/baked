<template>
  <div class="h-screen">
    <div class="flex flex-col gap-8 justify-center items-center h-3/4">
      <form class="w-96 flex justify-center flex-col gap-4 px-4" @submit.prevent="submit">
        <InputText
          v-model="username"
          :invalid="submitted && !username"
          type="text"
          placeholder="Username"
          :class="{ 'animate-shake': submitted && !username }"
        />
        <Password
          v-model="password"
          :invalid="submitted && !password"
          name="password"
          placeholder="Password"
          :feedback="false"
          :class="{ 'animate-shake': submitted && !password }"
          fluid
        />
        <Button type="submit" class="mt-4" label="Login" />
      </form>
    </div>
  </div>
  <Toast />
</template>
<script setup>
import { onUpdated, ref } from "vue";
import { definePageMeta, useToken } from "#imports";
import { Button, InputText, Password, Toast } from "primevue";

definePageMeta({
  layout: false
});

const token = useToken();

const username = ref();
const password = ref();
const submitted = ref(false);

onUpdated(() =>{
  submitted.value = false;
});

async function submit() {
  submitted.value = true;

  const result = await $fetch("authentication-samples/login", {
    baseURL: "http://localhost:5151",
    method: "POST",
    body: { username: username.value, password: password.value }
  });

  if(!result) {
    password.value = null;

    return;
  }

  token.setCurrent(result);
}
</script>
