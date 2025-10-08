<template>
  <div class="dashboard-container">
    <div class="filters-container">
      <SearchBar @update="onSearchUpdate" />
      <OrderOptions :query="Query" />
    </div>

    <ConvenioList :convenios="ListadoConvenios" :isloading="isloading" />
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
import { useConvenioQuery } from '@/Composables/CreateConvenioQueryObject';
import { ApiService } from '@/Services/ApiService';
import '@/Styles/Dashboard.css';
import type { ConvenioEspecificoDto, ConvenioMarcoDto } from '@/Types/ViewModels/ViewModels';
import { ref } from 'vue';


const ListadoConvenios = ref<ConvenioEspecificoDto|ConvenioMarcoDto|null>(null);
const errorMensaje = ref<string|null>(null);
const isloading = ref(false);
const QueryComposable = useConvenioQuery();

const obtenerConvenios = async () =>{
  errorMensaje.value = null,
  isloading.value = true;

  const result = await ApiService.GetConvenios(QueryComposable.queryObject);

  if(!result.isSuccess){
    errorMensaje.value = result.error.message;
  }else{
    ListadoConvenios.value = result.value;
  }

  isloading.value = false
}

</script>


<template>  
  
</template>