export default function() {
  const route = useRoute();
  const { public: { menu } } = useRuntimeConfig();

  try {
    const parentPath = `/${route.path.split("/")[1]}`;

    return menu[route.path] ||
    {
      ...menu[parentPath].submenu[route.path],
      parent: menu[parentPath]
    };
  } catch {
    return {
      title: "Error",
      icon: "pi pi-minus-circle",
      route: route.path
    };
  }
};

