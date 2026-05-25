<template>
  <UiSpec>
    <ProvideLoading title="Base">
      <AwaitLoading>
        <span data-testid="output">HIDDEN</span>
      </AwaitLoading>
    </ProvideLoading>
    <ProvideLoading
      title="Loaded"
      loaded
    >
      <AwaitLoading data-testid="output">
        <span>SHOWN</span>
      </AwaitLoading>
    </ProvideLoading>
    <ProvideLoading
      title="Multi Children Slot"
      loaded
    >
      <AwaitLoading data-testid="output">
        <span data-testid="output-1">1</span>
        <span data-testid="output-2">2</span>
      </AwaitLoading>
    </ProvideLoading>
    <ProvideLoading title="Customized">
      <AwaitLoading
        :skeleton="{
          width: '5rem',
          height: '5rem'
        }"
      />
    </ProvideLoading>
    <ProvideLoading title="Overridden">
      <AwaitLoading data-testid="output">
        <template #loading>
          <span>loading...</span>
        </template>
      </AwaitLoading>
    </ProvideLoading>
    <ProvideError
      title="Error"
      :error
    >
      <template #default="{ handled }">
        <AwaitLoading data-testid="error" />
        <span data-testid="handled">{{ handled ? "handled" : "not-handled" }}</span>
      </template>
    </ProvideError>
    <ProvideError
      title="Customized Error"
      :error
    >
      <AwaitLoading
        data-testid="error"
        :skeleton="{
          width: '5rem',
          height: '5rem'
        }"
      />
    </ProvideError>
    <ProvideError
      title="Overridden Error"
      :error
    >
      <AwaitLoading>
        <template #error="{ error: { formatted: e } }">
          <span data-testid="error">! {{ e.summary }} - {{ e.detail }} !</span>
        </template>
      </AwaitLoading>
    </ProvideError>
    <ProvideError
      title="No Error"
      :error
    >
      <template #default="{ handled }">
        <AwaitLoading no-error>CONTENT</AwaitLoading>
        <span data-testid="handled">{{ handled ? "handled" : "not-handled" }}</span>
      </template>
    </ProvideError>
  </UiSpec>
</template>
<script setup>
import { AwaitLoading } from "#components";
import giveMe from "@utils/giveMe";

const error = giveMe.anError({
  title: "TEST ERROR",
  detail: "TEST DESCRIPTION"
});
</script>