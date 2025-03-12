<template>
  <div class="p-error p-8">
    <div class="pt-8 space-y-4">
      <Tag
        severity="danger"
        :value="statusCode"
        class="text-4xl"
      />
      <h1 class="text-6xl">
        {{ errorInfo.title }}
      </h1>
      <div class="text-2xl">
        {{ errorInfo.message }}. Erişmek istediğiniz
        sayfaya aşağıdaki menü üzerinden ulaşmayı deneyiniz.
      </div>
    </div>
    <Divider
      type="dashed"
      class="my-8"
    />
    <Message severity="warn">
      <i class="pi pi-exclamation-circle mr-2" />
      İstediğiniz sayfaya ulaşamıyorsanız, lütfen sistem yöneticisi ile
      iletişime geçiniz.
    </Message>
  </div>
</template>
<script setup>
import { Tag, Divider, Message } from "primevue";
import { computed } from "vue";

const props = defineProps({
  error: {
    type: Error,
    default: {}
  }
});

const statusCode = props.error?.data?.status ?? props.error?.statusCode ?? 500;
const errorInfo = computed(() => {
  return errorInfos[statusCode] ?? errorInfos[500];
});

const errorInfos = {};
errorInfos[403] = { title: "Erişim Reddedildi", message: "Belirttiğiniz adresi veya veriyi görüntüleme yetkiniz bulunmamaktadır." };
errorInfos[404] = { title: "Sayfa Bulunamadı", message: "Belirttiğiniz adres yanlış ya da eskimiş olabilir." };
errorInfos[500] = { title: "Beklenmeyen Hata", message: "Lütfen sistem yöneticisi ile iletişime geçiniz" };
</script>
