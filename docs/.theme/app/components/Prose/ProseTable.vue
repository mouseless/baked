<template>
  <div class="rounded-xs overflow-x-auto mt-(--space-sm) mb-(--space-md)">
    <table
      class="
        border-spacing-0 w-full
        [&_tr_td]:p-(--space-sm)
        [&_tr_th]:p-(--space-sm)
        [&_tr_th]:bg-(--color-bg-table-nth-child)
        [&_tr:nth-child(even)_td]:bg-(--color-bg-table-nth-child)
        [&_*]:text-left
      "
    >
      <slot />
    </table>
  </div>
</template>