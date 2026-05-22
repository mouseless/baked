<template>
  <UiSpec>
    <ProvideLoading title="Base">
      <AwaitLoading>
        HIDDEN
      </AwaitLoading>
    </ProvideLoading>
    <ProvideLoading
      title="Loaded"
      loaded
    >
      <AwaitLoading class="test">
        <span>SHOWN</span>
      </AwaitLoading>
    </ProvideLoading>
    <ProvideLoading title="Customized">
      <AwaitLoading
        :skeleton="{ width: '5rem', height: '5rem' }"
      />
    </ProvideLoading>
    <ProvideLoading title="Overridden">
      <AwaitLoading>
        <template #loading>
          loading...
        </template>
      </AwaitLoading>
    </ProvideLoading>
    <ProvideError
      title="Error"
      :error
    >
      <AwaitLoading />
    </ProvideError>
    <ProvideError
      title="Customized Error"
      :error
    >
      <AwaitLoading
        :skeleton="{ width: '5rem', height: '5rem' }"
      />
    </ProvideError>
    <ProvideError
      title="Overridden Error"
      :error
    >
      <AwaitLoading>
        <template #error="{ error: e }">
          ! {{ e.summary }} - {{ e.detail }} !
        </template>
      </AwaitLoading>
    </ProvideError>
    <ProvideError
      title="No Error"
      :error
    >
      <AwaitLoading no-error>
        HIDDEN
      </AwaitLoading>
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