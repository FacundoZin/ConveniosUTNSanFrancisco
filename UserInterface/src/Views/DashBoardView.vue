<template>
  <div class="dashboard-container">
    <div class="filters-container">
      <SearchBar @update="onSearchUpdate" />
      <OrderOptions :query="Query" />
    </div>

    <ConvenioList :convenios="ListadoConvenios" />
    <Pagination :query="Query" @pagina-cambiada="ObtenerConvenios" />

    <button @click="ObtenerConvenios" class="buscar-btn">
      Buscar
    </button>
  </div>
</template>


<script lang="ts" setup>
import ConvenioList from '@/Components/ConvenioList.vue';
import OrderOptions from '@/Components/OrderOptions.vue';
import Pagination from '@/Components/Pagination.vue';
import SearchBar from '@/Components/SearchBar.vue';
import ApiService from '@/Services/ApiService';
import '@/Styles/Dashboard.css';
import type { ConvenioQueryObject } from '@/Types/Api.Interface';
import type { Convenioview } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { ref } from 'vue';


const ListadoConvenios = ref<Convenioview[]>([]);
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
function onSearchUpdate({ Busqueda, Parametro }: { Busqueda: string, Parametro: 'titulo' | 'empresa' }) {
  if (Parametro === 'titulo') Query.value.TituloConvenio = Busqueda
  if (Parametro === 'empresa') Query.value.Nombre_empresa = Busqueda
}


const ObtenerConvenios = async () => {
  errorMensaje.value = '';
  try {
    const response = await ApiService.GetConvenios(Query.value);
    ListadoConvenios.value = [
      ...response.data.conveniosMarco,
      ...response.data.conveniosEspecificos
    ];
  } catch (error) {
    if (isAxiosError(error) && error.response) {
      errorMensaje.value = ` ${error.response.data}`;
    } else {
      errorMensaje.value = "Lo sentimos, algo a salido mal"
    }
  }
}
</script>
