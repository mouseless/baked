import { ref, onMounted, onBeforeUnmount } from "vue";
import { useRuntimeConfig } from "#app";

export default function() {
  const { public: { composables: { useBreakpoints: { screens } } } } = useRuntimeConfig();

  const sizes = Object
    .keys(screens)
    .reduce((result, key) => ({ [key]: removePx(screens[key]), ...result }), {});

  const refs = {};
  Object.keys(sizes).forEach(key => {
    refs[`is${capitalize(key)}`] = ref(false);
    refs[`isMax${capitalize(key)}`] = ref(false);
  });

  const update = () => {
    const width = window.innerWidth;

    Object.keys(sizes).forEach(key => {
      refs[`is${capitalize(key)}`].value = width >= sizes[key];
      refs[`isMax${capitalize(key)}`].value = width < sizes[key];
    });
  };

  onMounted(() => {
    update();
    window.addEventListener("resize", update);
  });

  onBeforeUnmount(() => {
    window.removeEventListener("resize", update);
  });

  function removePx(size) {
    return typeof size === "string"
      ? parseInt(size.replace("px", ""), 10)
      : size;
  }

  function capitalize(str) {
    if(str.length === 0) return str;

    return str.charAt(0).toUpperCase() + str.slice(1);
  }

  return refs;
}
