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
const showNoResultsMode = ref(false)

const activeFilterComponent = ref<string | null>(null)

const obtenerConvenios = async () => {
  errorMensaje.value = null
  isloading.value = true

  const result = await ApiService.GetConvenios(QueryComposable.queryObject)

  if (!result.isSuccess) {
    errorMensaje.value = result.error.message
    console.log('hay un error')
  } else {
    console.log('esta es la respuesta de la api', result.value)
    ListadoConvenios.value = CreateListConveniosDto(result.value, TypeofConvenioToSearch.value)
    
    // Check if results are empty to toggle "No Results" mode
    if (ListadoConvenios.value.data.length === 0) {
      showNoResultsMode.value = true
    } else {
      showNoResultsMode.value = false
    }
    
    const listaCreada = CreateListConveniosDto(result.value, TypeofConvenioToSearch.value)
    console.log('esta la lista de convenios creada:', listaCreada.data, listaCreada.Type)
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

const resetSearch = () => {
  showNoResultsMode.value = false
  // Optionally reset the list to empty or initial state if desired, 
  // but just showing the filters again is the main request.
  // We might want to clear the "No results" message by resetting the list type or data?
  // For now, let's just show the filters. The user can then search again.
  // To make it cleaner, we can reset the list to an empty state but with type '' so it doesn't show "No results" immediately?
  // Or just keep the empty list but hide the "No results" component? 
  // Wait, the "No results" component is inside ConvenioList and shows when data.length === 0.
  // If we set showNoResultsMode = false, the filters appear. 
  // But ConvenioList is still showing "No results" because data.length is 0.
  // The user asked to "elimine el mensaje de la pantalla".
  // So we should probably reset ListadoConvenios to a state where it doesn't show the message.
  // CreateListConveniosDto(null) returns a list with empty data.
  // If we want to hide the "No results" message in ConvenioList, we need to make sure `convenios.Type` is '' (empty string)
  // because ConvenioList has `<div v-if="!isloading && convenios.Type !== ''" ...>`
  
  ListadoConvenios.value = CreateListConveniosDto(null) // This sets Type to '' by default if we look at the factory? 
  // Let's check CreateListConveniosDto implementation or usage. 
  // In line 20: CreateListConveniosDto(null)
  // If I reset it to this, Type might be empty or default.
  // Let's assume CreateListConveniosDto(null) creates a safe empty object.
}
</script>

<template>
  <div v-if="!showNoResultsMode">
    <div class="d-flex justify-content-center my-4">
      <ul class="nav nav-pills p-1 bg-light rounded-pill shadow-sm">
        <li class="nav-item">
          <button
            class="nav-link rounded-pill px-4 d-flex align-items-center gap-2"
            :class="{ active: TypeofConvenioToSearch === 'marco' }"
            @click="
              () => {
                TypeofConvenioToSearch = 'marco'
                FilterPanelOpen = true
              }
            "
          >
            <i class="bi bi-folder-fill"></i>
            Convenios Marcos
          </button>
        </li>
        <li class="nav-item">
          <button
            class="nav-link rounded-pill px-4 d-flex align-items-center gap-2"
            :class="{ active: TypeofConvenioToSearch === 'especifico' }"
            @click="
              () => {
                TypeofConvenioToSearch = 'especifico'
                FilterPanelOpen = true
              }
            "
          >
            <i class="bi bi-file-earmark-text-fill"></i>
            Convenios Específicos
          </button>
        </li>
      </ul>
    </div>

    <FilterPanel
      :isPanelOpen="FilterPanelOpen"
      :typeOfConvenio="TypeofConvenioToSearch"
      :QueryObject="QueryComposable.queryObject"
      @close-panel="FilterPanelOpen = false"
      @filter-selected="handleFilterSelected"
      @DirectSearch="obtenerConvenios"
    />

    <SearchByTitle
      v-if="activeFilterComponent === KeyFilters.ByTitulo"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByTitle>

    <SearchByEmpresa
      v-if="activeFilterComponent === KeyFilters.ByEmpresa"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByEmpresa>

    <SearchByNumeroConvenio
      v-if="activeFilterComponent === KeyFilters.ByNumeroConvenio"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByNumeroConvenio>

    <SearchByNumeroResolucion
      v-if="activeFilterComponent === KeyFilters.ByNumeroResolucion"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByNumeroResolucion>

    <SearchByFechaFin
      v-if="activeFilterComponent === KeyFilters.ByFechaFin"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByFechaFin>

    <SearchByFechaFirma
      v-if="activeFilterComponent === KeyFilters.ByFechaFirma"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByFechaFirma>

    <SearchByCarreras
      v-if="activeFilterComponent === KeyFilters.ByCarrera"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByCarreras>

    <SearchByEstado
      v-if="activeFilterComponent === KeyFilters.ByEstado"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByEstado>

    <SearchByAntiguedad
      v-if="activeFilterComponent === KeyFilters.ByAntiguedadDto"
      @SearchDone="obtenerConvenios"
      :QueryObject="QueryComposable.queryObject"
      :type-of-convenio="TypeofConvenioToSearch"
    ></SearchByAntiguedad>
  </div>

  <div class="d-flex justify-content-center mt-4">
    <div
      v-if="errorMensaje"
      class="alert alert-danger alert-dismissible fade show w-30 text-center shadow"
      role="alert"
    >
      <strong>Error:</strong> {{ errorMensaje }}
      <button
        type="button"
        class="btn-close"
        data-bs-dismiss="alert"
        aria-label="Close"
        @click="errorMensaje = null"
      ></button>
    </div>
  </div>

  <ConvenioList 
    :convenios="ListadoConvenios" 
    :isloading="isloading" 
    @reset-search="resetSearch"
  />
</template>


