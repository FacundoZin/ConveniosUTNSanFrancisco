<script lang="ts" setup>
import { KeyFilters } from '@/Common/KeyFilter'
import ConvenioList from '@/Components/ConvenioList.vue'
import FilterPanel from '@/Components/FilterPanel.vue'
import SearchByAntiguedad from '@/Components/SearchComponents/SearchByAntiguedad.vue'
import SearchByCarreras from '@/Components/SearchComponents/SearchByCarreras.vue'
import SearchByEmpresa from '@/Components/SearchComponents/SearchByEmpresa.vue'
import SearchByEstado from '@/Components/SearchComponents/SearchByEstado.vue'
import SearchByFechaFin from '@/Components/SearchComponents/SearchByFechaFin.vue'
import SearchByFechaFirma from '@/Components/SearchComponents/SearchByFechaFirma.vue'
import SearchByNumeroConvenio from '@/Components/SearchComponents/SearchByNumeroConvenio.vue'
import SearchByNumeroResolucion from '@/Components/SearchComponents/SearchByNumeroResolucion.vue'
import SearchByTitle from '@/Components/SearchComponents/SearchByTitle.vue'
import { useConvenioQuery } from '@/Composables/CreateConvenioQueryObject'
import { CreateListConveniosDto } from '@/Factory/ConvenioFactory'
import ApiService from '@/Services/ApiService'
import type { ListConveniosDto } from '@/Types/ViewModels/ViewModels'
import { ref } from 'vue'

const ListadoConvenios = ref<ListConveniosDto>(CreateListConveniosDto(null))
const errorMensaje = ref<string | null>(null)
const isloading = ref(false)
const QueryComposable = useConvenioQuery()
const TypeofConvenioToSearch = ref<'marco' | 'especifico' | ''>('marco')
const FilterPanelOpen = ref(true)

const activeFilterComponent = ref<string | null>(null)

const obtenerConvenios = async () => {
  errorMensaje.value = null
  isloading.value = true

  const result = await ApiService.GetConvenios(QueryComposable.queryObject)

  if (!result.isSuccess) {
    errorMensaje.value = result.error.message
    console.log('hay un error')
  } else {

    console.log("esta es la respuesta de la api", result.value)
    ListadoConvenios.value = CreateListConveniosDto(result.value)
    const listaCreada = CreateListConveniosDto(result.value)
    console.log("esta la lista de convenios creada:", listaCreada.data, listaCreada.Type)
  }

  isloading.value = false
}

const handleOpenofFilterPanel = (type: 'marco' | 'especifico') => {
  TypeofConvenioToSearch.value = type
  FilterPanelOpen.value = true
}

const handleFilterSelected = (filterKey: string) => {
  activeFilterComponent.value = filterKey
}
</script>

<template>
  <div class="d-flex gap-2 p-4">
    <div v-if="errorMensaje" class="alert alert-danger alert-dismissible fade show" role="alert">
      <strong>Error:</strong> {{ errorMensaje }}
      <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"
        @click="errorMensaje = null"></button>
    </div>
  </div>

  <div class="d-flex justify-content-center my-3">
    <div class="toggle-switch-convenios">
      <input type="radio" name="tipoConvenio" id="btn-marco" value="marco" :checked="TypeofConvenioToSearch === 'marco'"
        @change="
          () => {
            TypeofConvenioToSearch = 'marco'
            FilterPanelOpen = true
          }
        " />
      <label for="btn-marco"> <i class="bi bi-folder-fill me-2"></i> Convenios Marcos </label>

      <input type="radio" name="tipoConvenio" id="btn-especifico" value="especifico"
        :checked="TypeofConvenioToSearch === 'especifico'" @change="
          () => {
            TypeofConvenioToSearch = 'especifico'
            FilterPanelOpen = true
          }
        " />
      <label for="btn-especifico">
        <i class="bi bi-file-earmark-text-fill me-2"></i> Convenios Específicos
      </label>

      <span class="toggle-slider"></span>
    </div>
  </div>

  <FilterPanel :isPanelOpen="FilterPanelOpen" :typeOfConvenio="TypeofConvenioToSearch"
    :QueryObject="QueryComposable.queryObject" @close-panel="FilterPanelOpen = false"
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

<style scoped src="@/styles/toggle-convenios.css"></style>
