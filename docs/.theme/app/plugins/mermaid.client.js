import mermaid from "mermaid";

const mermaidOptions = {
  theme: "base",
  flowchart: {
    useMaxWidth: false
  },
  quadrantChart: {
    xAxisPosition: "bottom"
  }
};

const getCSSVariable = variable => {
  if(typeof document !== "undefined") {
    return getComputedStyle(document.documentElement).getPropertyValue(variable).trim();
  }

  return "";
};

const themeVariables = {
  fontFamily: "system-ui",
  fontSize: "16px",
  primaryColor: getCSSVariable("--color-mermaid-primary"),
  primaryTextColor: getCSSVariable("--color-mermaid-primary-text"),
  primaryBorderColor: getCSSVariable("--color-mermaid-primary-border"),
  lineColor: getCSSVariable("--color-mermaid-primary-line"),
  secondaryColor: getCSSVariable("--color-mermaid-secondary"),
  secondaryTextColor: getCSSVariable("--color-mermaid-secondary-text"),
  secondaryBorderColor: getCSSVariable("--color-mermaid-secondary-border"),
  tertiaryColor: getCSSVariable("--color-mermaid-tertiary"),
  activationBkgColor: getCSSVariable("--color-mermaid-primary"),
  quadrant1Fill: getCSSVariable("--color-mermaid-fill1"),
  quadrant2Fill: getCSSVariable("--color-mermaid-tertiary"),
  quadrant3Fill: getCSSVariable("--color-dark-box-bg"),
  quadrant4Fill: getCSSVariable("--color-mermaid-tertiary"),
  quadrantInternalBorderStrokeFill: getCSSVariable("--color-mermaid-secondary"),
  quadrantExternalBorderStrokeFill: getCSSVariable("--color-mermaid-secondary")
};

export default defineNuxtPlugin(() => {
  mermaid.initialize({ startOnLoad: false, ...mermaidOptions, themeVariables });

  return {
    provide: {
      mermaid
    }
  };
});