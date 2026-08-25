<script setup lang="ts">
import Header from '@/modules/shared/components/Header.vue';
import SimpleHeader from '@/modules/shared/components/SimpleHeader.vue';
import { computed } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const isSimpleHeaderRoute = computed(() => {
  return route.path === '/login' || route.path === '/admin';
});
</script>

<template>
  <SimpleHeader v-if="isSimpleHeaderRoute" />
  <Header v-else />
  <router-view />
</template>

<style scoped></style>
