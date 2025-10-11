<script lang="ts" setup>
import FilterPanel from '@/Components/FilterPanel.vue'; 
import { useConvenioQuery } from '@/Composables/CreateConvenioQueryObject';
import { ApiService } from '@/Services/ApiService';
import '@/Styles/Dashboard.css';
import type { ConvenioEspecificoDto, ConvenioMarcoDto } from '@/Types/ViewModels/ViewModels';
import { ref } from 'vue';


const ListadoConvenios = ref<ConvenioEspecificoDto|ConvenioMarcoDto|null>(null);
const errorMensaje = ref<string|null>(null);
const isloading = ref(false);
const QueryComposable = useConvenioQuery();
const TypeofConvenioToSearch = ref<'marco' | 'especifico' | ''>('marco'); 
const FilterPanelOpen = ref(false);

const activeFilterComponent = ref<string | null>(null); 


const obtenerConvenios = async () =>{
  errorMensaje.value = null;
  isloading.value = true;

  const result = await ApiService.GetConvenios(QueryComposable.queryObject);

  if(!result.isSuccess){
    errorMensaje.value = result.error.message;
  }else{
    ListadoConvenios.value = result.value;
  }

  isloading.value = false
};

const handleOpenofFilterPanel = (type: 'marco' | 'especifico') => {
  TypeofConvenioToSearch.value = type; 
  FilterPanelOpen.value = true; 
};

const handleFilterSelected = (filterKey: string) => {
  activeFilterComponent.value = filterKey; 
};
</script>

<template>
  <div class="dashboard-container">
    <div class="filters-container">
      </div>

    <button @click="obtenerConvenios" class="buscar-btn">
      Buscar
    </button>
  </div>

  <div class="d-flex gap-2 p-4">
    <button 
      type="button" 
      class="btn btn-info text-white" 
      @click="handleOpenofFilterPanel('marco')"
    >
      Buscar Convenios Marcos
      <i class="bi bi-arrow-right"></i>
    </button>

    <button 
      type="button" 
      class="btn btn-info text-white" 
      @click="handleOpenofFilterPanel('especifico')"
    >
      Buscar Convenios Especificos
      <i class="bi bi-arrow-right"></i>
    </button>
  </div>
  
  <FilterPanel :is-panel-open="FilterPanelOpen" 
    :type-of-convenio="TypeofConvenioToSearch"                       
    @close-panel="FilterPanelOpen = false" 
     @filter-selected="handleFilterSelected"  />
                                
  />

  <!-- aca iria un v-if que compare la  clave del filtro seleccionado y muestre el componente que corresponda -->
</template>

<style scoped>
.btn i.bi {
  margin-left: 0.5rem;
}
/* Asegura que el texto blanco se vea bien sobre el celeste claro (info) */
.btn-info {
  --bs-btn-color: #fff;
}
</style>