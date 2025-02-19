<template>
  <Panel header="Dashboard">
    <Menubar :model="menus"/>
  </Panel>
</template>
<script setup>
import { Menubar } from "primevue";

const menus = ref([{
  label: "Rich Transients",
  icon: "pi pi-palette",
  items: [
    {
      label: "Rich Transient w/ Datas 1",
      url: "/rich-transient-with-datas/test1"
    },
    {
      label: "Rich Transient w/ Datas 2",
      url: "/rich-transient-with-datas/test2"
    },
    {
      label: "Rich Transient w/ Datas 3",
      url: "/rich-transient-with-datas/test3"
    }
  ]
}]);
</script>