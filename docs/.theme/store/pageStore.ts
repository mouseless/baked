import { defineStore } from "pinia";

export const usePageStore = defineStore("pageStore", {
  state: () => ({
    pages: {}
  }),
  actions: {
    setPages(pages: {}) {
      this.pages = pages;
    }
  }
});
