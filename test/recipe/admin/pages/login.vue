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
</template>
<script setup>
import { onUpdated, ref } from "vue";
import { useAuth, useToken } from "#imports";
import { Button, InputText, Password } from "primevue";

const auth = useAuth();
const token = useToken();

const username = ref();
const password = ref();
const submitted = ref(false);

onUpdated(() =>{
  submitted.value = false;
});

async function submit() {
  submitted.value = true;

  const result = await auth.login(username.value, password.value);

  if(!result) {
    password.value = null;

    return;
  }

  token.setCurrent(result);
}
</script>
