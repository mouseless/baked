<template>
  <UiSpec>
    <FakeLoading title="Base">
      <AwaitLoading>NONE</AwaitLoading>
    </FakeLoading>
    <FakeLoading title="Customized">
      <AwaitLoading :skeleton="{ width: '5rem', height: '5rem' }" />
    </FakeLoading>
    <FakeLoading title="Overridden">
      <AwaitLoading>
        <template #loading>
          loading...
        </template>
      </AwaitLoading>
    </FakeLoading>
    <i>CASE: verify props forwarded to component</i>
    <FakeLoading
      title="Error"
      :error
    >
      <AwaitLoading />
    </FakeLoading>
    <FakeLoading
      title="Error"
      :error
    >
      <AwaitLoading :skeleton="{ width: '5rem', height: '5rem' }" />
    </FakeLoading>
    <FakeLoading
      title="Error"
      :error
    >
      <AwaitLoading>
        <template #error="{ error: e }">
          ! {{ e.summary }} - {{ e.detail }} !
        </template>
      </AwaitLoading>
    </FakeLoading>
    <i>CASE: verify error claimed</i>
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