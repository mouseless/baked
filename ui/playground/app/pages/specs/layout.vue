<template>
  <UiSpec test-id="layout">
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if below button displays `Report` page with `Default` layout ⬆️"
    />
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="default"
        type="button"
        :label="l('Spec: Page with Default Layout')"
        class="m-4"
        @click="$router.push('/report-page-sample')"
      />
    </div>
    <Divider />
    <Message
      :schema="{ size: 'large' }"
      data="⬇️  Check if below button displays `Login` page with `Modal` layout ⬆️"
    />
    <div
      class="border-4 border-gray-500 rounded"
      data-testid="component"
    >
      <Button
        data-testid="modal"
        type="button"
        :label="l('Spec: Page with Modal Layout')"
        class="m-4"
        @click="$router.push(loginPath)"
      />
    </div>
  </UiSpec>
</template>
<script setup>
import { Message } from "#components";
import { Button, Divider } from "primevue";
import { useLocalization, useLoginRedirect } from "#imports";

const { localize: l } = useLocalization();
const loginRedirect = useLoginRedirect();

const loginPath = loginRedirect.compute();
</script>