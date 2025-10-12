<script lang="ts" setup>
import { KeyFilters } from '@/Common/KeyFilter';
import ConvenioList from '@/Components/ConvenioList.vue';
import FilterPanel from '@/Components/FilterPanel.vue';
import SearchByAntiguedad from '@/Components/SearchComponents/SearchByAntiguedad.vue';
import SearchByCarreras from '@/Components/SearchComponents/SearchByCarreras.vue';
import SearchByEmpresa from '@/Components/SearchComponents/SearchByEmpresa.vue';
import SearchByEstado from '@/Components/SearchComponents/SearchByEstado.vue';
import SearchByFechaFin from '@/Components/SearchComponents/SearchByFechaFin.vue';
import SearchByFechaFirma from '@/Components/SearchComponents/SearchByFechaFirma.vue';
import SearchByNumeroConvenio from '@/Components/SearchComponents/SearchByNumeroConvenio.vue';
import SearchByNumeroResolucion from '@/Components/SearchComponents/SearchByNumeroResolucion.vue';
import SearchByTitle from '@/Components/SearchComponents/SearchByTitle.vue';
import { useConvenioQuery } from '@/Composables/CreateConvenioQueryObject';
import { CreateListConveniosDto } from '@/Factory/ConvenioFactory';
import { ApiService } from '@/Services/ApiService';
import '@/Styles/Dashboard.css';
import type { ListConveniosDto } from '@/Types/ViewModels/ViewModels';
import { ref } from 'vue';


const ListadoConvenios = ref<ListConveniosDto>(CreateListConveniosDto(null));
const errorMensaje = ref<string | null>(null);
const isloading = ref(false);
const QueryComposable = useConvenioQuery();
const TypeofConvenioToSearch = ref<'marco' | 'especifico' | ''>('marco');
const FilterPanelOpen = ref(false);

const activeFilterComponent = ref<string | null>(null);


const obtenerConvenios = async () => {
  errorMensaje.value = null;
  isloading.value = true;

  const result = await ApiService.GetConvenios(QueryComposable.queryObject);

  if (!result.isSuccess) {
    errorMensaje.value = result.error.message;
  } else {
    ListadoConvenios.value = CreateListConveniosDto(result.value);
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
  <div class="d-flex gap-2 p-4">
    <div v-if="errorMensaje" class="alert alert-danger alert-dismissible fade show" role="alert">
      <strong>Error:</strong> {{ errorMensaje }}
      <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"
        @click="errorMensaje = null"></button>
    </div>
    <button type="button" class="btn btn-info text-white" @click="handleOpenofFilterPanel('marco')">
      Buscar Convenios Marcos
      <i class="bi bi-arrow-right"></i>
    </button>

    <button type="button" class="btn btn-info text-white" @click="handleOpenofFilterPanel('especifico')">
      Buscar Convenios Especificos
      <i class="bi bi-arrow-right"></i>
    </button>
  </div>

  <FilterPanel :is-panel-open="FilterPanelOpen" :type-of-convenio="TypeofConvenioToSearch"
    :-query-object="QueryComposable.queryObject" @close-panel="FilterPanelOpen = false"
    @filter-selected="handleFilterSelected" @DirectSearch="obtenerConvenios" />

  <SearchByTitle v-if="activeFilterComponent === KeyFilters.ByTitulo" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByTitle>

  <SearchByEmpresa v-if="activeFilterComponent === KeyFilters.ByEmpresa" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByEmpresa>

  <SearchByNumeroConvenio v-if="activeFilterComponent === KeyFilters.ByNumeroConvenio" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByNumeroConvenio>

  <SearchByNumeroResolucion v-if="activeFilterComponent === KeyFilters.ByNumeroResolucion"
    @SearchDone="obtenerConvenios" :QueryObject="QueryComposable.queryObject"
    :type-of-convenio="TypeofConvenioToSearch"></SearchByNumeroResolucion>

  <SearchByFechaFin v-if="activeFilterComponent === KeyFilters.ByFechaFin" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByFechaFin>

  <SearchByFechaFirma v-if="activeFilterComponent === KeyFilters.ByFechaFirma" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByFechaFirma>

  <SearchByCarreras v-if="activeFilterComponent === KeyFilters.ByCarrera" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByCarreras>

  <SearchByEstado v-if="activeFilterComponent === KeyFilters.ByEstado" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByEstado>

  <SearchByAntiguedad v-if="activeFilterComponent === KeyFilters.ByAntiguedadDto" @SearchDone="obtenerConvenios"
    :QueryObject="QueryComposable.queryObject" :type-of-convenio="TypeofConvenioToSearch"></SearchByAntiguedad>

  <ConvenioList :convenios="ListadoConvenios" :isloading="isloading" />
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