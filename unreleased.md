# Unreleased

## `tailwindcss` Upgrade

```markdown
- [ ] Update `@mouseless/baked` packages to the latest version
- [ ] Remove the `@nuxtjs/tailwindcss` package
  - Remove it from your dependencies
  - If you have any related configuration in `nuxt.config.ts`, it can also be removed
- [ ] Move your global style overrides from `.scss` files into `components.css`
  - Import Tailwind at the top of  `components.css`
  - Wrap all overrides in the `utilities` layer
  - Group overrides by component using uppercase section comments (MDN style)
  - All custom css should be their `.b-component-...` class
  - Update the CSS import in `nuxt.config.ts`
- [ ] Replace custom CSS with Tailwind utilities using `@apply` wherever possible
- [ ] Move theme variables to `theme.css`
  - Rename your existing variables file or create a new `theme.css`
  - Extract repeated calculated values into CSS variables
  - Expose reusable variables through Tailwind's `@theme` when appropriate
- [ ] Move all styles from component `<style>` blocks into `components.css`
- [ ] Review your styles after the migration
  - Since component styles are no longer scoped, some selectors may have different specificity
  - You may need to add tailwind's `!` modifier in a few places to preserve the expected behavior
- [ ] Remove the `sass` package
  - Remove any related configuration from `nuxt.config.ts`
  - Remove the package from your dependencies
- [ ] If you previously customized Tailwind breakpoints or `useBreakpoints`, migrate them accordingly
  - `useBreakpoints` can continue to be configured as before
  - Tailwind breakpoints must now be defined in `theme.css` using `@theme`
```

## Improvements

- `@nuxtjs/tailwindcss` package is no longer required by the module
  - Configurations are no longer needed
- Tailwind 4 is now supported and automatically installed

## Breaking Changes

- Tailwind `screens` must now be configured via Tailwind's `@theme`
- PrimeVue layer order is changed, new order is
  `theme, base, primevue, utilities`
- `override.scss` is renamed to `component.css`
- Existing styles may break after Tailwind 4 upgrade
  - Follow the `tailwindcss` Upgrade guide above to migrate

## Library Upgrades

| npm Package         | Old Version | New Version |
| ---                 | ---         | ---         |
| @nuxtjs/tailwindcss | 6.14.0      | removed     |
| @tailwindcss/vite   | new         | 4.3.1       |
| tailwindcss         | new         | 4.3.1       |