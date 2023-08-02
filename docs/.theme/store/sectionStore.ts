import { defineStore } from "pinia";

export const useSectionStore = defineStore("sectionStore", {
  state: () => ({
    sections: {}
  }),
  actions: {
    setSections(sections: {}) {
      this.sections = sections;
    }
  }
});
