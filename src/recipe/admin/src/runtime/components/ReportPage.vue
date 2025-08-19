<template>
  <div class="space-y-4">
    <PageTitle :schema="title">
      <template #actions>
        <QueryParameters
          v-if="queryParameters?.length > 0"
          :parameters="queryParameters"
          @ready="onReady"
          @changed="onChanged"
        />
      </template>
      <template #extra>
        <Tabs
          v-if="ready && tabs.length > 1"
          v-model:value="currentTab"
          class="!-mb-4"
        >
          <TabList :pt="{ tabList: { class: '!bg-transparent' } }">
            <Tab
              v-for="tab in shownTabs"
              :key="tab.id"
              :value="tab.id"
              class="space-x-2"
            >
              <Bake
                v-if="tab.icon"
                :name="`tabs/${tab.id}/icon`"
                :descriptor="tab.icon"
              />
              <span v-if="tab.title">{{ l(tab.title) }}</span>
            </Tab>
          </TabList>
        </Tabs>
      </template>
    </PageTitle>
    <Message
      v-if="showRequiredMessage"
      severity="info"
    >
      <i class="pi pi-info-circle" />
      <span class="ml-3">{{ lc("Select required values to view this report") }}</span>
    </Message>
    <div
      v-if="ready"
      class="py-4 flex flex-col gap-4 items-center"
    >
      <DeferredTabContent
        v-for="tab in shownTabs"
        :key="`${uniqueKey}-${tab.id}`"
        v-model="currentTab"
        :when="tab.id"
        class="w-full"
        :class="{ 'max-w-screen-xl': !tab.fullScreen }"
      >
        <template v-if="tab.fullScreen">
          <Bake
            v-for="(content, i) in tab.contents.filter(content => content.showWhen ? page[content.showWhen] : true)"
            :key="`content-${content.key || i}`"
            :name="`tabs/${tab.id}/contents/${content.key || i}`"
            :descriptor="content.component"
          />
        </template>
        <div
          v-else
          class="grid grid-cols-1 lg:grid-cols-2 gap-4"
        >
          <div
            v-for="(content, i) in tab.contents.filter(content => content.showWhen ? page[content.showWhen] : true)"
            :key="`content-${content.key || i}`"
            :class="{ 'lg:col-span-2': !content.narrow }"
          >
            <Bake
              :name="`tabs/${tab.id}/contents/${content.key || i}`"
              :descriptor="content.component"
            />
          </div>
        </div>
      </DeferredTabContent>
    </div>
  </div>
</template>
<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { Message, Tab, TabList, Tabs } from "primevue";
import { useContext, useLocalization } from "#imports";
import { Bake, DeferredTabContent, PageTitle, QueryParameters } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization("ReportPage");

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { title, queryParameters, tabs } = schema;

const page = context.page();
const articleOverflow = context.articleOverflow();
const ready = ref(queryParameters.length === 0);
const uniqueKey = ref();
const currentTab = ref(tabs.length > 0 ? tabs[0].id : "");
const shownTabs = computed(() => tabs.filter(tab => tab.showWhen ? page[tab.showWhen] : true));
const lastTab = ref();
// this could be computed(() => !ready.value), but message gets duplicated when
// page is refreshed so this variable is not handled separately. this is
// probably because `Message` is a component that fades in and added to dom in
// an unusual way
const showRequiredMessage = ref(false);

onMounted(() => {
  showRequiredMessage.value = !ready.value;
});

if(tabs.length > 0) {
  articleOverflow.value = tabs[0].overflow || false;
  watch(
    currentTab,
    (newTabId, oldTabId) => {
      if(newTabId === oldTabId) { return; }

      const newTab = tabs.find(tab => tab.id === newTabId);
      if(!newTab) { return; }

      articleOverflow.value = newTab.overflow || false;
    },
    { immediate: true }
  );
}

for(const tab of tabs) {
  if(!tab.showWhen) { continue; }

  // switch to a shown tab when current tab gets hidden upon page context
  // change
  watch(
    () => page[tab.showWhen],
    (show, previousShow) => {
      if(show === previousShow) { return; }

      if(currentTab.value === tab.id && !show) {
        // current tab will be hidden, switch to first shown tab
        lastTab.value = currentTab.value;
        const firstAvailableTab = shownTabs.value[0];
        if(firstAvailableTab) {
          currentTab.value = firstAvailableTab.id;
        }
      } else if(lastTab.value === tab.id && show) {
        // last tab becomes available, switch back to it
        currentTab.value = lastTab.value;
        lastTab.value = null;
      }
    },
    { immediate: true }
  );
}

function onReady(value) {
  ready.value = value;
  showRequiredMessage.value = !value;
}

function onChanged(value) {
  uniqueKey.value = value;
}
</script>
