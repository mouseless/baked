import { defineAsyncComponent } from "vue";

const Components = import.meta.glob("./components/*.vue");

const Baked = Object
  .keys(Components)
  .reduce((result, path) => {
      const name = path.slice(path.indexOf("components/") + "components/".length, path.lastIndexOf(".vue"))
      result[name] = defineAsyncComponent(Components[path]);
      return result;
  }, {});

module.exports = Baked;
