# Unreleased

## Improvements

- `@nuxtjs/tailwindcss` package is no longer required by the module
  - Configurations are no longer needed
- Tailwind 4 support is added
  - Tailwind `screens` should now be configured via Tailwind's `@theme`
  - Tailwind 4 is automatically installed
- PrimeVue layer order is updated, new order is
  `theme, base, primevue, utilities`
- `override.scss` is renamed to `component.css`

## Library Upgrades

| npm Package         | Old Version | New Version |
| ---                 | ---         | ---         |
| @nuxtjs/tailwindcss | 6.14.0      | removed     |
| @tailwindcss/vite   | new         | 4.3.1       |
| tailwindcss         | new         | 4.3.1       |