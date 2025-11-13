<template>
  <Bake
    :name="name"
    :descriptor="descriptor"
    :class="classes"
  />
</template>
<script setup>
import { reactive } from "vue";
import { useRoute, useRuntimeConfig } from "#app";
import { useContext, useFormat, useHead, usePages } from "#imports";
import { Bake } from "#components";

const context = useContext();
const { asClasses } = useFormat();
const pages = usePages();
const route = useRoute();
const { public: { components } } = useRuntimeConfig();

useHead({ title: components?.Page?.title });

context.providePage(reactive({}));
// TODO - review this in form components
context.provideEvents(Events());

const name = route.matched[0].name;
const className = name.replace("[", "").replace("]", "");
context.provideData({ ... route.params }, "Params");

const descriptor = await pages.fetch(name);
const classes = [asClasses("page"), asClasses(className, "b-route--")];

// TODO - review this in form components
function Events() {
  const listeners = {};

  function on(name, id, callback) {
    listeners[name] ||= {};

    listeners[name][id] = callback;
  }

  function off(name, id) {
    // eslint-disable-next-line @typescript-eslint/no-dynamic-delete
    delete listeners[name][id];
  }

  async function emit(name) {
    if(!listeners[name]) { return; }

    for(const id in listeners[name]) {
      listeners[name][id]();
    }
  }

  return {
    on,
    off,
    emit
  };
}
</script>
