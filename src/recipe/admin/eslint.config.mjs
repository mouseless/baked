import pluginVue from "eslint-plugin-vue";

export default [
  ...pluginVue.configs["flat/recommended"],
  {
    ignores: [
      "**/.nuxt/",
      "**/.output/",
      "**/.public/",
      "**/.temp/",
      "**/content/",
      "**/dist/",
      "**/.vscode/",
      "**/.prebuild/",
      "**/package-lock.json",
      "**/tsconfig.json",
      "pages/*.json"
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
      "vue/multi-word-component-names": "off",
      "vue/html-quotes": ["error", "double"],
      "vue/no-multiple-template-root": "off"
    }
  }
];
