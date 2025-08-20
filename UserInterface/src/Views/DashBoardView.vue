<template>
  <div>
    <SearchBar @update="onSearchUpdate" />

    <!-- Botón que realmente llama a la API -->
    <button @click="buscarApi" class="bg-green-500 text-white px-4 py-2 rounded mt-3">
      Buscar
    </button>
  </div>
</template>

<script lang="ts" setup>
import SearchBar from '@/components/SearchBar.vue'
import { getConvenios } from '@/services/conveniosApi'
import type { convenioFilters } from '@/Types'
import { ref } from 'vue'

const filtros = ref<convenioFilters>({
  TituloConvenio: '',
  Nombre_empresa: '',
  ProximosAterminar: false,
  AntiguedadAscendente: false,
  AntiguedadDescendente: false,
  PaginaActual: 1,
  CantidadResultados: 10
})

// Cada vez que el usuario escribe o cambia tipo, actualizamos filtros
function onSearchUpdate({ tipo, valor }) {
  if (tipo === 'titulo') filtros.value.TituloConvenio = valor
  if (tipo === 'empresa') filtros.value.Nombre_empresa = valor
}


const Result = getConvenios(filtros.value)
// Llama a la API con los filtros actuales
async function buscarApi() {
  try {
    const { data } = await getConvenios(filtros.value)
    console.log('Resultados:', data)
    // acá actualizás tu lista de convenios
  } catch (e) {
    console.error(e)
  }
}
</script>
