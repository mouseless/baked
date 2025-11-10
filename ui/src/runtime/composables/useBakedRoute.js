import { useRoute } from "#app";

export default function() {
  const route = useRoute();

  if(!route.params?.baked && !route.params.routeParts) {
    return { };
  }

  if(route.params.baked) {
    return {
      path: [route.params.baked].join("/")
    };
  }

  if(route.params.routeParts) {
    return {
      path: [route.params.routeParts].join("/"),
      ...route.params
    };
  }
}
