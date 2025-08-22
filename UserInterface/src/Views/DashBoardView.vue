<template>
  <div class="bg-gray-100 min-h-screen p-4">
    <div class="max-w-7xl mx-auto">
      <h1 class="text-3xl font-bold text-gray-800 mb-6">Panel de Convenios</h1>

      <SearchBar @update="onSearchUpdate" />
      <OrderOptions :query="Query" @update:query="onOrderUpdate" />

      <div class="mt-6 flex justify-end">
        <button @click="ObtenerConvenios" class="bg-blue-600 text-white px-6 py-2 rounded-lg shadow hover:bg-blue-700 transition-colors">
          Buscar
        </button>
      </div>

      <ConvenioList :convenios="ListadoConvenios" />
      <ThePagination :query="Query" @pagina-cambiada="ObtenerConvenios" />
    </div>
  </div>
</template>

<script lang="ts" setup>
import ConvenioList from '@/Components/ConvenioList.vue';
import OrderOptions from '@/Components/OrderOptions.vue';
import ThePagination from '@/Components/ThePagination.vue';
import SearchBar from '@/Components/SearchBar.vue'
import ApiService from '@/Services/ApiService'
import type { ConvenioQueryObject } from '@/Types/Api.Interface'
import type { Convenioview } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { ref } from 'vue'

const ListadoConvenios = ref<(Convenioview[])>([]);
const errorMensaje = ref('');

const Query = ref<ConvenioQueryObject>({
  TituloConvenio: '',
  Nombre_empresa: '',
  ProximosAterminar: false,
  AntiguedadAscendente: false,
  AntiguedadDescendente: false,
  PaginaActual: 1,
  CantidadResultados: 10
})

// Cada vez que el usuario escribe o cambia tipo, actualizamos filtros
function onSearchUpdate({ Busqueda, Parametro } : { Busqueda: string, Parametro: 'titulo' | 'empresa' }){
  if (Parametro === 'titulo') Query.value.TituloConvenio = Busqueda
  if (Parametro === 'empresa') Query.value.Nombre_empresa = Busqueda
}

function onOrderUpdate(newQuery: Partial<ConvenioQueryObject>) {
  Query.value = { ...Query.value, ...newQuery };
}


const ObtenerConvenios = async () => {
  errorMensaje.value = '';
  try{  
    const response = await ApiService.GetConvenios(Query.value);
    ListadoConvenios.value = response.data;
  }catch(error){
    if(isAxiosError(error) && error.response){
      errorMensaje.value = ` ${error.response.data}`;
    }else{
      errorMensaje.value = "Lo sentimos, algo a salido mal"
    }
  }
}
</script>