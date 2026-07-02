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

## `@mouseless/baked` Migration

```md
- [ ] Update `@mouseless/baked` packages to the latest version.
- [ ] Remove the `@nuxtjs/tailwindcss` package.
  - Remove it from your dependencies.
  - If you have any related configuration in `nuxt.config.ts`, it can also be removed.
- [ ] Move your global style overrides from `.scss` files into `components.css`.
  - Import Tailwind at the top of the file:
    ```css
    @import "tailwindcss";
    ```
  - Wrap all overrides inside:
    ```css
    @layer utilities {
      /* overrides */
    }
    ```
  - Group overrides by component and separate them using uppercase section comments as recommended by MDN:
    ```css
    /* BUTTON */

    /* MODAL */

    /* INPUT */
    ```
  - Don't forget to update the CSS import path in `nuxt.config.ts`.
- [ ] Replace custom CSS with Tailwind utilities using `@apply` wherever possible.
- [ ] Rename your variables file to `theme.css`, or move your theme variables into a new `theme.css` file.
  - If you have repeated calculated values, extract them into CSS variables and use them with arbitrary values:
    ```css
    .title {
      font-size: var(--text-size);
    }
    ```
    in Tailwind:
    ```html
    <div class="text-(--text-size)">
    ```
  - You can also expose CSS variables through Tailwind's theme to get shorter utility classes:
    ```css
    :root {
      --color-custom: #xxxxx;
    }

    @theme {
      --color-custom: var(--color-custom);
    }
    ```
    Then use them directly:
    ```html
    <div class="text-custom bg-custom"></div>
    ```
- [ ] Move all styles from component `<style>` blocks into `components.css`.
- [ ] Review your styles after the migration.
  - Since component styles are no longer scoped, some selectors may have different specificity.
  - You may need to add `!important` (or Tailwind's `!` modifier) in a few places to preserve the expected behavior.
- [ ] Remove the `sass` package.
  - Remove any related configuration from `nuxt.config.ts`.
  - Remove the package from your dependencies.
- [ ] If you previously customized Tailwind breakpoints or `useBreakpoints`, migrate them accordingly.
  - `useBreakpoints` can continue to be configured as before.
  - Tailwind breakpoints must now be defined in `theme.css` using `@theme`:
    ```css
    @theme {
      --breakpoint-sm: 640px;
      --breakpoint-md: 768px;
      --breakpoint-lg: 1024px;
    }
    ```
```

## Library Upgrades

| npm Package         | Old Version | New Version |
| ---                 | ---         | ---         |
| @nuxtjs/tailwindcss | 6.14.0      | removed     |
| @tailwindcss/vite   | new         | 4.3.1       |
| tailwindcss         | new         | 4.3.1       |