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
  return slot.value[0].children.default()[0].children[0].children;
});

const types = {
  "!NOTE": { class: "info", icon: "fa-circle-info" },
  "!WARNING": { class: "warning", icon: "fa-warning" },
  "!TIP": { class: "tip", icon: "fa-lightbulb" },
  "!CAUTION": { class: "danger", icon: "fa-circle-xmark" },
  default: { class: "default", icon: "fa-angle-right" }
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
  padding: 0;
  border-radius: 10px;
  margin: 1em 0;
  max-width: $width-content;

  i {
    position: absolute;
    margin-top: 1em;
    line-height: $line-height;
  }

  div {
    width: 100%;
    padding-left: 3ch;
  }

  @mixin box($color) {
    i {
      color: $color;
    }
  }

  &.info {
    @include box($color-box-info);
  }
  &.warning {
    @include box($color-box-warning);
  }
  &.tip {
    @include box($color-box-tip);
  }
  &.danger {
    @include box($color-box-danger);
  }
  &.default {
    @include box($color-box-default);
  }
}

@media(min-width: $width-page-xl) {
  blockquote i {
    line-height: calc($line-height * $line-height-xl-coef);
  }
}
</style>
