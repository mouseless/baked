<template>
  <UiSpec
    :variants
    no-loading-variant
  />
</template>
<script setup>
import giveMe from "@utils/giveMe";

const variants = [
  {
    name: "Base",
    descriptor: giveMe.aButton({
      action: giveMe.aLocalAction({
        redirect: "/page/with/route/pageWithRoute"
      })
    })
  },
  {
    name: "Conditional",
    descriptor: giveMe.anExpectedInput({
      action: giveMe.aLocalAction({
        redirect: "/page/with/route/pageWithRoute",
        options: giveMe.aCompositeData([
          giveMe.anInlineData({ expected: "redirect" }),
          giveMe.aContextData({ key: "model", targetProp: "actual" })
        ])
      })
    })
  },
  {
    name: "Dynamic",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aButton({
          label: "Test without query",
          action: giveMe.aLocalAction({
            redirect: "/page/with/route/[id]",
            options: giveMe.aCompositeData([
              giveMe.anInlineData({ route: "/page/with/route/[id]" }),
              giveMe.anInlineData({ id: "42" })
            ])
          })
        }),
        giveMe.aButton({
          label: "Test with query",
          action: giveMe.aLocalAction({
            redirect: "/page/with/route/[id]",
            options: giveMe.aCompositeData([
              giveMe.anInlineData({ route: "/page/with/route/[id]", query: true }),
              giveMe.anInlineData({ id: "42", queryTest: true })
            ])
          })
        })
      ]
    })
  },
  {
    name: "Query",
    descriptor: giveMe.aContainer({
      contents: [
        giveMe.aButton({
          label: "All query",
          action: giveMe.aLocalAction({
            redirect: "/page/with/route",
            options: giveMe.aCompositeData([
              giveMe.anInlineData({ route: "/page/with/route", query: true }),
              giveMe.anInlineData({ query1: "true", query2: "true" })
            ])
          })
        }),
        giveMe.aButton({
          label: "Included query",
          action: giveMe.aLocalAction({
            redirect: "/page/with/route",
            options: giveMe.aCompositeData([
              giveMe.anInlineData({ route: "/page/with/route", query: true, includeQuery: [ "included" ] }),
              giveMe.anInlineData({ included: "true", excluded: "true" })
            ])
          })
        }),
        giveMe.aButton({
          label: "Excluded query",
          action: giveMe.aLocalAction({
            redirect: "/page/with/route",
            options: giveMe.aCompositeData([
              giveMe.anInlineData({ route: "/page/with/route", query: true, excludeQuery: [ "included"] }),
              giveMe.anInlineData({ included: "true", excluded: "true" })
            ])
          })
        })
      ]
    })
  }
];
</script>