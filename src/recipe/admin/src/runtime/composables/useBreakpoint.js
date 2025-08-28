import { ref, onMounted, onBeforeUnmount } from "vue";

export function useBreakpoint() {
  const isXs = ref(false); // < 640px
  const isSm = ref(false); // >= 640px
  const isMd = ref(false); // >= 768px
  const isLg = ref(false); // >= 1024px
  const isXl = ref(false); // >= 1280px
  const is2xl = ref(false); // >= 1536px
  const isMaxSm = ref(false); // <= 768px
  const isMaxMd = ref(false); // <= 1024px
  const isMaxLg = ref(false); // <= 1280px
  const isMaxXl = ref(false); // <= 1536px

  // Dont use hard-coded sizes, fetch from common place
  const update = () => {
    const width = window.innerWidth;
    isXs.value = width < 640;
    isSm.value = width >= 640;
    isMd.value = width >= 768;
    isLg.value = width >= 1024;
    isXl.value = width >= 1280;
    is2xl.value = width >= 1536;
    isMaxSm.value = width <= 768;
    isMaxMd.value = width <= 1024;
    isMaxLg.value = width <= 1280;
    isMaxXl.value = width <= 1536;
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
