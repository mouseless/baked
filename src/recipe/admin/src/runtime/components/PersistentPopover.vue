<!--
  Origin: https://github.com/primefaces/primevue/blob/master/packages/primevue/src/popover/Popover.vue

  Note: We've changed `v-if` to `v-show` to make the popover content persistent in the dom
-->
<template>
  <Portal :append-to="appendTo">
    <transition
      name="p-popover"
      v-bind="ptm('transition')"
      @enter="onEnter"
      @leave="onLeave"
      @after-leave="onAfterLeave"
    >
      <div
        v-show="visible"
        :ref="containerRef"
        v-focustrap
        role="dialog"
        :aria-modal="visible"
        :class="cx('root')"
        v-bind="ptmi('root')"
        @click="onOverlayClick"
      >
        <slot
          v-if="$slots.container"
          name="container"
          :close-callback="hide"
          :keydown-callback="(event) => onButtonKeydown(event)"
        />
        <template v-else>
          <div
            :class="cx('content')"
            v-bind="ptm('content')"
            @click="onContentClick"
            @mousedown="onContentClick"
            @keydown="onContentKeydown"
          >
            <slot />
          </div>
        </template>
      </div>
    </transition>
  </Portal>
</template>

<script>
import { $dt } from "@primeuix/styled";
import { absolutePosition, addClass, addStyle, focus, getOffset, isClient, isTouchDevice, setAttribute } from "@primeuix/utils/dom";
import { ZIndex } from "@primeuix/utils/zindex";
import { ConnectedOverlayScrollHandler } from "@primevue/core/utils";
import FocusTrap from "primevue/focustrap";
import OverlayEventBus from "primevue/overlayeventbus";
import Portal from "primevue/portal";
import Ripple from "primevue/ripple";
import { BasePersistentPopover } from "#components";

export default {
  name: "PersistentPopover",
  directives: {
    focustrap: FocusTrap,
    ripple: Ripple
  },
  components: {
    Portal
  },
  extends: BasePersistentPopover,
  inheritAttrs: false,
  props: {
    fixed: Boolean
  },
  emits: ["show", "hide"],
  data() {
    return {
      visible: false
    };
  },
  watch: {
    dismissable: {
      immediate: true,
      handler(newValue) {
        if(newValue) {
          this.bindOutsideClickListener();
        } else {
          this.unbindOutsideClickListener();
        }
      }
    }
  },
  selfClick: false,
  target: null,
  eventTarget: null,
  outsideClickListener: null,
  scrollHandler: null,
  resizeListener: null,
  container: null,
  styleElement: null,
  overlayEventListener: null,
  documentKeydownListener: null,
  beforeUnmount() {
    if(this.dismissable) {
      this.unbindOutsideClickListener();
    }

    if(this.scrollHandler) {
      this.scrollHandler.destroy();
      this.scrollHandler = null;
    }

    this.destroyStyle();
    this.unbindResizeListener();
    this.target = null;

    if(this.container && this.autoZIndex) {
      ZIndex.clear(this.container);
    }

    if(this.overlayEventListener) {
      OverlayEventBus.off("overlay-click", this.overlayEventListener);
      this.overlayEventListener = null;
    }

    this.container = null;
  },
  mounted() {
    if(this.breakpoints) {
      this.createStyle();
    }
  },
  methods: {
    toggle(event, target) {
      if(this.visible) this.hide();
      else this.show(event, target);
    },
    show(event, target) {
      this.visible = true;
      this.eventTarget = event.currentTarget;
      this.target = target || event.currentTarget;
    },
    hide() {
      this.visible = false;
    },
    onContentClick() {
      this.selfClick = true;
    },
    onEnter(el) {
      addStyle(el, { position: "absolute", top: "0" });
      this.alignOverlay();

      if(this.dismissable) {
        this.bindOutsideClickListener();
      }

      this.bindScrollListener();
      this.bindResizeListener();

      if(this.autoZIndex) {
        ZIndex.set("overlay", el, this.baseZIndex + this.$primevue.config.zIndex.overlay);
      }

      this.overlayEventListener = e => {
        if(this.container.contains(e.target)) {
          this.selfClick = true;
        }
      };

      this.focus();
      OverlayEventBus.on("overlay-click", this.overlayEventListener);
      this.$emit("show");

      if(this.closeOnEscape) {
        this.bindDocumentKeyDownListener();
      }
    },
    onLeave() {
      this.unbindOutsideClickListener();
      this.unbindScrollListener();
      this.unbindResizeListener();
      this.unbindDocumentKeyDownListener();
      OverlayEventBus.off("overlay-click", this.overlayEventListener);
      this.overlayEventListener = null;
      this.$emit("hide");
    },
    onAfterLeave(el) {
      if(this.autoZIndex) {
        ZIndex.clear(el);
      }
    },
    alignOverlay() {
      absolutePosition(this.container, this.target, false);
      if(this.fixed) {
        this.fixPopoverPosition(this.container, this.target);
      }

      const containerOffset = getOffset(this.container);
      const targetOffset = getOffset(this.target);
      let arrowLeft = 0;

      if(containerOffset.left < targetOffset.left) {
        arrowLeft = targetOffset.left - containerOffset.left;
      }

      this.container.style.setProperty($dt("popover.arrow.left").name, `${arrowLeft}px`);

      if(containerOffset.top < targetOffset.top) {
        this.container.setAttribute("data-p-popover-flipped", "true");
        !this.isUnstyled && addClass(this.container, "p-popover-flipped");
      }
    },
    fixPopoverPosition(container, target) {
      // this is a ChatGPT generated function. provided the `absolutePosition`
      // function body and asked it to fix the position for a fixed component.
      // ---
      // @cihandeniz
      const rect = target.getBoundingClientRect();
      const vw = window.innerWidth, vh = window.innerHeight;
      const w = container.offsetWidth, h = container.offsetHeight;

      let top = rect.bottom;
      let left = rect.left;

      if(rect.bottom + h > vh) { top = Math.max(0, rect.top - h); }
      if(rect.left + w > vw) { left = Math.max(0, rect.right - w); }

      Object.assign(container.style, {
        position: "fixed",
        top: `${top}px`,
        left: `${left}px`
      });
    },
    onContentKeydown(event) {
      if(event.code === "Escape" && this.closeOnEscape) {
        this.hide();
        focus(this.target);
      }
    },
    onButtonKeydown(event) {
      switch (event.code) {
      case "ArrowDown":
      case "ArrowUp":
      case "ArrowLeft":
      case "ArrowRight":
        event.preventDefault();

      default:
        break;
      }
    },
    focus() {
      const focusTarget = this.container.querySelector("[autofocus]");

      if(focusTarget) {
        focusTarget.focus();
      }
    },
    onKeyDown(event) {
      if(event.code === "Escape" && this.closeOnEscape) {
        this.visible = false;
      }
    },
    bindDocumentKeyDownListener() {
      if(!this.documentKeydownListener) {
        this.documentKeydownListener = this.onKeyDown.bind(this);
        window.document.addEventListener("keydown", this.documentKeydownListener);
      }
    },
    unbindDocumentKeyDownListener() {
      if(this.documentKeydownListener) {
        window.document.removeEventListener("keydown", this.documentKeydownListener);
        this.documentKeydownListener = null;
      }
    },
    bindOutsideClickListener() {
      if(!this.outsideClickListener && isClient()) {
        this.outsideClickListener = event => {
          if(this.visible && !this.selfClick && !this.isTargetClicked(event)) {
            this.visible = false;
          }

          this.selfClick = false;
        };

        document.addEventListener("click", this.outsideClickListener);
      }
    },
    unbindOutsideClickListener() {
      if(this.outsideClickListener) {
        document.removeEventListener("click", this.outsideClickListener);
        this.outsideClickListener = null;
        this.selfClick = false;
      }
    },
    bindScrollListener() {
      if(!this.scrollHandler) {
        this.scrollHandler = new ConnectedOverlayScrollHandler(this.target, () => {
          if(this.visible) {
            this.visible = false;
          }
        });
      }

      this.scrollHandler.bindScrollListener();
    },
    unbindScrollListener() {
      if(this.scrollHandler) {
        this.scrollHandler.unbindScrollListener();
      }
    },
    bindResizeListener() {
      if(!this.resizeListener) {
        this.resizeListener = () => {
          if(this.visible && !isTouchDevice()) {
            this.visible = false;
          }
        };

        window.addEventListener("resize", this.resizeListener);
      }
    },
    unbindResizeListener() {
      if(this.resizeListener) {
        window.removeEventListener("resize", this.resizeListener);
        this.resizeListener = null;
      }
    },
    isTargetClicked(event) {
      return this.eventTarget && (this.eventTarget === event.target || this.eventTarget.contains(event.target));
    },
    containerRef(el) {
      this.container = el;
    },
    createStyle() {
      if(!this.styleElement && !this.isUnstyled) {
        this.styleElement = document.createElement("style");
        this.styleElement.type = "text/css";
        setAttribute(this.styleElement, "nonce", this.$primevue?.config?.csp?.nonce);
        document.head.appendChild(this.styleElement);

        let innerHTML = "";

        for(const breakpoint in this.breakpoints) {
          innerHTML += `
            @media screen and (max-width: ${breakpoint}) {
              .p-popover[${this.$attrSelector}] {
                width: ${this.breakpoints[breakpoint]} !important;
              }
            }
          `;
        }

        this.styleElement.innerHTML = innerHTML;
      }
    },
    destroyStyle() {
      if(this.styleElement) {
        document.head.removeChild(this.styleElement);
        this.styleElement = null;
      }
    },
    onOverlayClick(event) {
      OverlayEventBus.emit("overlay-click", {
        originalEvent: event,
        target: this.target
      });
    }
  }
};
</script>

<style>
.p-popover-content {
  & > div {
    @apply max-sm:p-0;
  }
}
</style>