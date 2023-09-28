<template>
  <blockquote :class="type.class">
    <i
      v-if="type.icon"
      :class="[ 'fa', type.icon ]"
    />
    <div>
      <component :is="() => body" />
    </div>
  </blockquote>
</template>
<script setup>
import { computed, useSlots } from "#imports";

const slots = useSlots();

const slot = computed(() => {
  return slots.default();
});

const firstLine = computed(() => {
  return slot.value[0].children.default()[0].children;
});

const types = {
  ":information_source:": { class: "info", icon: "fa-circle-info" },
  ":warning:": { class: "warning", icon: "fa-warning" },
  ":bulb:": { class: "tip", icon: "fa-lightbulb" },
  ":x:": { class: "danger", icon: "fa-circle-xmark" },
  default: { class: "default" }
};

const type = computed(() => {
  return types[firstLine.value] ?? types.default;
});

const body = computed(() => {
  let result = slot.value;

  if(types[firstLine.value]) {
    result = result.slice(1);
  }

  return result;
});
</script>
<style lang="scss" scoped>
blockquote {
  display: flex;
  flex-direction: row;
  align-items: flex-start;
  justify-content: flex-start;
  padding: 0 1em;
  border: solid 2px;
  border-radius: 10px;
  margin: 15px 0px;

  i {
    padding-top: 1.2em;
    padding-right: 1em;
  }

  div {
    width: 100%;
  }

  @mixin box($type) {
    @include theme("border-color", $theme-color-bd-box + "-" + $type);
    @include theme("background-color", $theme-color-bg-box + "-" + $type);
    @include theme("color", $theme-color-box + "-" + $type);

    i {
      @include theme("color", $theme-color-i-box + "-" + $type);
    }
  }

  &.info {
    @include box("info");
  }
  &.warning {
    @include box("warning");
  }
  &.tip {
    @include box("tip");
  }
  &.danger {
    @include box("danger");
  }
  &.default {
    @include box("default");
  }
}
</style>
