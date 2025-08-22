<script lang="ts" setup>
import type { ConvenioQueryObject } from '@/Types/Api.Interface';
import { computed } from 'vue';

const props = defineProps<{ query: ConvenioQueryObject }>();

const emit = defineEmits(["paginaCambiada"]);

const paginaActual = computed(() => props.query?.PaginaActual ?? 1);

function irPagina(nuevaPagina: number) {
  if (props.query) {
    props.query.PaginaActual = nuevaPagina;
    emit("paginaCambiada", nuevaPagina);
  }
}
</script>

<template>
  <div class="mt-6 flex justify-center items-center gap-2">
    <button
      @click="irPagina(paginaActual - 1)"
      :disabled="paginaActual <= 1"
      class="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-100 disabled:opacity-50 disabled:cursor-not-allowed"
    >
      Anterior
    </button>

    <div class="flex items-center gap-2">
      <button
        v-for="pagina in 5"
        :key="pagina"
        @click="irPagina(pagina)"
        :class="[
          'px-4 py-2 text-sm font-medium border rounded-lg',
          paginaActual === pagina
            ? 'text-white bg-blue-600 border-blue-600'
            : 'text-gray-700 bg-white border-gray-300 hover:bg-gray-100'
        ]"
      >
        {{ pagina }}
      </button>
    </div>

    <button
      @click="irPagina(paginaActual + 1)"
      class="px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-100"
    >
      Siguiente
    </button>
  </div>
</template>
