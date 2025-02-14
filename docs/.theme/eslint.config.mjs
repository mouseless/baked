import withNuxt from "./.nuxt/eslint.config.mjs";

export default withNuxt([
  {
    ignores: [
      "**/.nuxt/",
      "**/.output/",
      "**/.public/",
      "**/.temp/",
      "**/content/",
      "**/dist/",
      "**/.vscode/",
      "**/.prebuild/"
    ]
  },
  {
    rules: {
      "arrow-parens": ["error", "as-needed"],
      "comma-dangle": ["error", "never"],
      indent: ["error", 2],

      "keyword-spacing": [
        "error",
        {
          overrides: {
            if: {
              after: false
            },

            for: {
              after: false
            },

            while: {
              after: false
            },

            static: {
              after: false
            },

            as: {
              after: false
            }
          }
        }
      ],

      "no-multi-spaces": "error",
      "no-multiple-empty-lines": "error",
      "no-return-assign": "off",
      "no-trailing-spaces": "error",
      "no-var": "error",
      "prefer-const": "error",
      quotes: ["error", "double"],
      semi: ["error", "always"],
      "space-before-function-paren": ["error", "never"],
      "@typescript-eslint/no-dynamic-delete": "off",
      "vue/multi-word-component-names": "off",
      "vue/html-quotes": ["error", "double"],
      "vue/no-multiple-template-root": "off"
    }
  }
]);
