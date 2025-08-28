<template>
  <nav
    class="
      p-4 bg-slate-100 dark:bg-zinc-900
      flex flex-col justify-start gap-2
      max-md:p-2 max-md:h-16
      max-md:flex-row max-md:justify-between
      max-md:items-center
      2xl:min-w-64
    "
  >
    <RouterLink
      to="/"
      class="
        flex mt-4 mb-8 w-full min-w-[3.25rem]
        max-2xs:hidden
        max-md:my-0 max-md:w-10
      "
    >
      <Logo
        :src="logo"
        class="2xl:hidden"
      />
      <Logo
        :src="largeLogo"
        class="hidden 2xl:block"
      />
    </RouterLink>
    <div
      class="
        flex flex-col gap-2 h-full
        max-md:flex-row max-md:overflow-x-auto
        max-md:snap-x
      "
    >
      <template v-if="loading">
        <Skeleton class="py-6 min-w-[3.25rem] max-md:snap-start" />
        <Skeleton class="py-6 min-w-[3.25rem] max-md:snap-start" />
      </template>
      <template v-else-if="data">
        <SideMenuItem
          v-for="item in menu"
          :key="item.title"
          :item="item"
          :path="data.path"
          class="max-md:snap-start"
        />
      </template>
    </div>
    <div v-if="$slots.footer || footer">
      <div class="max-md:hidden">
        <Bake
          v-if="footer"
          name="footer"
          :descriptor="footer"
        />
        <slot
          v-else
          name="footer"
        />
      </div>
      <Button
        severity="secondary"
        icon="pi pi-cog"
        class="w-[3.25rem] h-[3.25rem] md:hidden"
        variant="text"
        rounded
        @click="toggleFooterPopover"
      />
      <Popover
        ref="footerPopover"
        class="md:hidden w-1/2 min-w-fit"
      >
        <Bake
          v-if="footer"
          name="footer"
          :descriptor="footer"
        />
        <slot
          v-else
          name="footer"
        />
      </Popover>
    </div>
  </nav>
</template>
<script setup>
import { ref } from "vue";
import { RouterLink } from "vue-router";
import { Button, Popover, Skeleton } from "primevue";
import { Bake, Logo, SideMenuItem } from "#components";
import { useContext } from "#imports";

const context = useContext();

const { schema, data } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, required: true }
});

const { logo, largeLogo, menu, footer } = schema;

const loading = context.loading();
const footerPopover = ref();

function toggleFooterPopover(event) {
  footerPopover.value.toggle(event);
}
</script>
