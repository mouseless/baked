<template>
  <div class="space-y-4">
    <PageTitle :schema="title">
      <template
        v-if="inputs?.length > 0"
        #actions
      >
        <Inputs
          :inputs
          @ready="onReady"
          @changed="onChanged"
        />
      </template>
      <template #extra>
        <Tabs
          v-if="ready && tabs.length > 1"
          v-model:value="currentTab"
          class="!-mb-4 overflow-x-auto overflow-y-hidden"
        >
          <TabList :pt="{ root: '!bg-transparent' }">
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
      class="pt-4 flex flex-col gap-4 items-center"
    >
      <DeferredTabContent
        v-for="tab in shownTabs"
        :key="`${uniqueKey}-${tab.id}`"
        v-model="currentTab"
        :when="tab.id"
        :class="{ 'max-w-screen-xl 3xl:max-w-screen-2xl': !tab.fullScreen }"
        class="w-full"
      >
        <template v-if="tab.fullScreen">
          <template
            v-for="content in tab.contents"
            :key="`content-${content.key}`"
          >
            <Bake
              :name="`tabs/${tab.id}/contents/${content.key}`"
              :descriptor="content.component"
            />
          </template>
        </template>
        <div
          v-else
          class="b-ReportPage--grid grid grid-cols-1 lg:grid-cols-2 gap-4"
        >
          <template
            v-for="content in tab.contents"
            :key="`content-${content.key}`"
          >
            <Bake
              :name="`tabs/${tab.id}/contents/${content.key}`"
              :descriptor="content.component"
              :class="{ 'lg:col-span-2': !content.narrow }"
            />
          </template>
        </div>
      </DeferredTabContent>
    </div>
  </div>
</template>
<script setup>
import { computed, onBeforeUnmount, onMounted, reactive, ref } from "vue";
import { Message, Tab, TabList, Tabs } from "primevue";
import { useContext, useLocalization, useReactionHandler } from "#imports";
import { Bake, DeferredTabContent, Inputs, PageTitle } from "#components";

const context = useContext();
const { localize: l } = useLocalization();
const { localize: lc } = useLocalization({ group: "ReportPage" });
const reactionHandler = useReactionHandler();

const { schema } = defineProps({
  schema: { type: null, required: true },
  data: { type: null, default: null }
});

const { title, inputs, tabs } = schema;

const path = context.injectPath();
const ready = ref(inputs.length === 0);
const uniqueKey = ref();
const currentTab = ref(tabs.length > 0 ? tabs[0].id : "");
const tabVisible = reactive(tabs.reduce((prev, cur) => ({ ...prev, [cur.id]: true }), {}));
const shownTabs = computed(() => tabs.filter(tab => tabVisible[tab.id]));
const lastTab = ref();
const tabReactions = [];
// this could be computed(() => !ready.value), but message gets duplicated when
// page is refreshed so this variable is not handled separately. this is
// probably because `Message` is a component that fades in and added to dom in
// an unusual way
const showRequiredMessage = ref(false);

for(const tab of tabs) {
  if(!tab.reactions) { continue; }

  const tabReaction = reactionHandler.create(`${path}/tabs/${tab.id}`, {
    reload() { },
    show(visible) {
      tabVisible[tab.id] = visible;

      // switch to a shown tab when current tab gets hidden upon page context
      // change
      if(currentTab.value === tab.id && !visible) {
        // current tab will be hidden, switch to first shown tab
        lastTab.value = currentTab.value;
        const firstAvailableTab = shownTabs.value[0];
        if(firstAvailableTab) {
          currentTab.value = firstAvailableTab.id;
        }
      } else if(lastTab.value === tab.id && visible) {
        // last tab becomes available, switch back to it
        currentTab.value = lastTab.value;
        lastTab.value = null;
      }
    }
  });
  tabReaction.bind(tab.reactions);
  tabReactions.push(tabReaction);
}

onMounted(() => {
  showRequiredMessage.value = !ready.value;
});

onBeforeUnmount(() => {
  for(const tabReaction of tabReactions) {
    tabReaction.unbind();
  }
});

function onReady(value) {
  ready.value = value;
  showRequiredMessage.value = !value;
}

function onChanged(event) {
  uniqueKey.value = event.uniqueKey;
}
</script>
