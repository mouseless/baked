import { ref, onMounted, onBeforeUnmount } from "vue";
import { useRuntimeConfig } from "#app";

export function useBreakpoint() {
  const { public: { composables } } = useRuntimeConfig();

  const sizes = composables.useBreakpoint.screens;

  const isXs = ref(false);
  const isSm = ref(false);
  const isMd = ref(false);
  const isLg = ref(false);
  const isXl = ref(false);
  const is2xl = ref(false);
  const isMaxSm = ref(false);
  const isMaxMd = ref(false);
  const isMaxLg = ref(false);
  const isMaxXl = ref(false);

  const removePx = size => typeof size === "string" ? parseInt(size.replace("px", ""), 10) : size;

  const update = () => {
    const width = window.innerWidth;
    Object.keys(sizes).forEach(key => {
      sizes[key] = removePx(sizes[key]);
    });

    isXs.value = width < sizes.md;
    isSm.value = width >= sizes.sm;
    isMd.value = width >= sizes.md;
    isLg.value = width >= sizes.lg;
    isXl.value = width >= sizes.xl;
    is2xl.value = width >= sizes["2xl"];
    isMaxSm.value = width <= sizes.md;
    isMaxMd.value = width <= sizes.lg;
    isMaxLg.value = width <= sizes.xl;
    isMaxXl.value = width <= sizes["2xl"];
  };

  onMounted(() => {
    update();
    window.addEventListener("resize", update);
  });

  onBeforeUnmount(() => {
    window.removeEventListener("resize", update);
  });

  return {
    isXs,
    isSm,
    isMd,
    isLg,
    isXl,
    is2xl,
    isMaxSm,
    isMaxMd,
    isMaxLg,
    isMaxXl
  };
}
