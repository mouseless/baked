# Unreleased

## Improvements

- `ReportPage` now offers a better full-screen rendering
  - `DefaultLayout` provides article overflow option in UI context
  - `overflow` is added to `ReportPageTab` to automatically enable article
    overflow when tab is selected
  - `fullScreen` is moved from `ReportPageTabContent` to `ReportPageTab` to
    manage both `fullScreen` and `overflow` together
  - `ReportPage` was rendering only first tab content when a tab is full-screen,
    now it renders all contents
- `DefaultLayout` overflow was not hidden causing unintended scrolls, fixed
- `DataTable` now has `virtualScrollerOptions` property for increasing
  performance when handling large amount of data
- `null` parameters now supported.
