<template>
  <div>
    <SearchBar @update="onSearchUpdate" />
    <OrderOptions :query="Query"/>
    <pre>{{ Query }}</pre>
    <ConvenioList :convenios="ListadoConvenios" />

    <!-- Botón que realmente llama a la API -->
    <button @click="ObtenerConvenios" class="bg-green-500 text-white px-4 py-2 rounded mt-3">
      Buscar
    </button>
  </div>
</template>

<script lang="ts" setup>
import ConvenioList from '@/Components/ConvenioList.vue';
import Filters from '@/Components/Filters.vue';
import OrderOptions from '@/Components/OrderOptions.vue';
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