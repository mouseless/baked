import { useState } from "#imports";

export default function() {
  return useState("uiStates", () => ({
    panelStates: { },
    selectStates: { }
  }));
}

