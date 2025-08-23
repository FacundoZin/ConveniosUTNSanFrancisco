<template>
  <div class="dashboard-container">
    <div class="filters-container">
      <SearchBar @update="onSearchUpdate" />
      <OrderOptions :query="Query"/>
    </div>

    <ConvenioList :convenios="ListadoConvenios" />
    <Pagination :query="Query" @pagina-cambiada="ObtenerConvenios"/>

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
function onSearchUpdate({ Busqueda, Parametro } : { Busqueda: string, Parametro: 'titulo' | 'empresa' }){
  if (Parametro === 'titulo') Query.value.TituloConvenio = Busqueda
  if (Parametro === 'empresa') Query.value.Nombre_empresa = Busqueda
}


const ObtenerConvenios = async () => {
  errorMensaje.value = '';
  try{  
    const response = await ApiService.GetConvenios(Query.value);
    ListadoConvenios.value = [
      ...response.data.conveniosMarco,
      ...response.data.conveniosEspecificos
    ];
  }catch(error){
    if(isAxiosError(error) && error.response){
      errorMensaje.value = ` ${error.response.data}`;
    }else{
      errorMensaje.value = "Lo sentimos, algo a salido mal"
    }
  }
}
</script>

<style scoped>
.dashboard-container {
  background-color: #f5f5f5; /* gris claro */
  padding: 20px;
  min-height: 100vh;
    padding-top: 80px; /* si tu header mide 80px */
}

.filters-container {
  background-color: #1a1a1a; /* negro para contraste */
  padding: 20px;
  border-radius: 10px;
  display: flex;
  gap: 15px;
  flex-wrap: wrap;
  margin-bottom: 20px;
}

.buscar-btn {
  background-color: #0077c8; /* azul UTN */
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background 0.3s;
  margin-top: 10px;
}

.buscar-btn:hover {
  background-color: #005ea3;
}
</style>
