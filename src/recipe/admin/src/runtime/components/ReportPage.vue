<template>
  <div class="space-y-4">
    <PageTitle :schema="title">
      <template #actions>
        <!--
          query params here
        -->
      </template>
      <template #extra>
        <Tabs
          v-model:value="currentTab"
          class="!-mb-4"
        >
          <TabList :pt="{ tabList: { class: '!bg-transparent' } }">
            <Tab
              v-for="(tab, i) in tabs"
              :key="tab.id"
              :value="tab.id"
              class="space-x-2"
            >
              <Bake
                v-if="tab.icon"
                :name="`tabs/${i}/icon`"
                :descriptor="tab.icon"
              />
              <span>{{ tab.title }}</span>
            </Tab>
          </TabList>
        </Tabs>
      </template>
    </PageTitle>
    <div
      v-if="allQueryParametersSet"
      class="py-4 flex flex-col gap-4 items-center"
    >
      <DeferredTabContent
        v-for="(tab, i) in tabs"
        :key="`${queryParametersJoined}-${tab.id}`"
        v-model="currentTab"
        :when="tab.id"
        class="w-full"
        :class="{ 'max-w-screen-xl': !(tab.contents.length === 1 && tab.contents[0].fullScreen) }"
      >
        <Bake
          v-if="tab.contents.length === 1 && tab.contents[0].fullScreen"
          :name="`tabs/${i}/contents/full-screen`"
          :descriptor="tab.contents[0].component"
        />
        <div
          v-else
          class="grid grid-cols-1 lg:grid-cols-2 gap-4"
        >
          <div
            v-for="(content, j) in tab.contents"
            :key="`content-${j}`"
            :class="{ 'lg:col-span-2': !content.narrow }"
          >
            <Bake
              :name="`tabs/${i}/contents/${j}`"
              :descriptor="content.component"
            />
          </div>
        </div>
      </DeferredTabContent>
    </div>
  </div>
</template>
<script setup>
import { ref } from "vue";
import { Tab, TabList, Tabs } from "primevue";
import Bake from "./Bake.vue";
import DeferredTabContent from "./DeferredTabContent.vue";
import PageTitle from "./PageTitle.vue";

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null },
  loading: { type: Boolean, default: false }
});

const { title, tabs } = schema;

const currentTab = ref(tabs.length > 0 ? tabs[0].id : "");
const allQueryParametersSet = ref(true);
const queryParametersJoined = ref("--join-query-parameters-here--");
</script>
