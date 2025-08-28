import { ref, onMounted, onBeforeUnmount } from "vue";
import { useRuntimeConfig } from "#app";

export default function() {
  const { public: { composables } } = useRuntimeConfig();

  const sizes = composables.useBreakpoint.screens;

  const is2xs = ref(false);
  const isXs = ref(false);
  const isSm = ref(false);
  const isMd = ref(false);
  const isLg = ref(false);
  const isXl = ref(false);
  const is2xl = ref(false);
  const is3xl = ref(false);
  const isMax2xs = ref(false);
  const isMaxXs = ref(false);
  const isMaxSm = ref(false);
  const isMaxMd = ref(false);
  const isMaxLg = ref(false);
  const isMaxXl = ref(false);
  const isMax2xl = ref(false);
  const isMax3xl = ref(false);
  const removePx = size => typeof size === "string" ? parseInt(size.replace("px", ""), 10) : size;

  const update = () => {
    const width = window.innerWidth;
    Object.keys(sizes).forEach(key => {
      sizes[key] = removePx(sizes[key]);
    });

    is2xs.value = width >= sizes["2xs"];
    isXs.value = width >= sizes.xs;
    isSm.value = width >= sizes.sm;
    isMd.value = width >= sizes.md;
    isLg.value = width >= sizes.lg;
    isXl.value = width >= sizes.xl;
    is2xl.value = width >= sizes["2xl"];
    is3xl.value = width >= sizes["3xl"];
    isMax2xs.value = width < sizes["2xs"];
    isMaxXs.value = width < sizes.xs;
    isMaxSm.value = width < sizes.sm;
    isMaxMd.value = width < sizes.md;
    isMaxLg.value = width < sizes.lg;
    isMaxXl.value = width < sizes.xl;
    isMax2xl.value = width < sizes["2xl"];
    isMax3xl.value = width < sizes["3xl"];
  };

  onMounted(() => {
    update();
    window.addEventListener("resize", update);
  });

  onBeforeUnmount(() => {
    window.removeEventListener("resize", update);
  });

  return {
    is2xs,
    isXs,
    isSm,
    isMd,
    isLg,
    isXl,
    is2xl,
    is3xl,
    isMax2xs,
    isMaxXs,
    isMaxSm,
    isMaxMd,
    isMaxLg,
    isMaxXl,
    isMax2xl,
    isMax3xl
  };
}
