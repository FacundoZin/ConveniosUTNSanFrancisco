<script lang="ts" setup>
import { KeyFilters } from '@/Common/KeyFilter'
import ConvenioList from '@/modules/convenios/components/ConvenioList.vue'
import FilterPanel from '@/modules/convenios/components/FilterPanel.vue'
import SearchByAntiguedad from '@/modules/convenios/components/search/SearchByAntiguedad.vue'
import SearchByAnio from '@/modules/convenios/components/search/SearchByAnio.vue'
import SearchByAreas from '@/modules/convenios/components/search/SearchByAreas.vue'
import SearchByDesdeHasta from '@/modules/convenios/components/search/SearchByDesdeHasta.vue'
import SearchByEmpresa from '@/modules/convenios/components/search/SearchByEmpresa.vue'
import SearchByEstado from '@/modules/convenios/components/search/SearchByEstado.vue'
import SearchByFechaFin from '@/modules/convenios/components/search/SearchByFechaFin.vue'
import SearchByFechaFirma from '@/modules/convenios/components/search/SearchByFechaFirma.vue'
import SearchByMes from '@/modules/convenios/components/search/SearchByMes.vue'
import SearchByNumeroConvenio from '@/modules/convenios/components/search/SearchByNumeroConvenio.vue'
import SearchByNumeroResolucion from '@/modules/convenios/components/search/SearchByNumeroResolucion.vue'
import SearchByTitle from '@/modules/convenios/components/search/SearchByTitle.vue'
import SearchCountByMes from '@/modules/convenios/components/search/SearchCountByMes.vue'
import SearchCountByRango from '@/modules/convenios/components/search/SearchCountByRango.vue'
import CountConveniosResult from '@/modules/convenios/components/CountConveniosResult.vue'
import { CreateListConveniosDto } from '@/Factory/ConvenioFactory'
import ConvenioService from '@/modules/convenios/services/ConvenioService';
import AppPagination from '@/modules/shared/components/AppPagination.vue'
import type { CantidadConveniosDto } from '@/Types/Convenios/CantidadConveniosDto'
import { ref } from 'vue'
import { useConvenioStore } from '@/modules/convenios/stores/convenioStore'
import { storeToRefs } from 'pinia'

const store = useConvenioStore()
const {
  listadoConvenios,
  typeofConvenioToSearch,
  filterPanelOpen,
  activeFilterComponent,
  showNoResultsMode,
  pagination,
  countResult,
  countResultBoth,
  countSearchType,
  countMonth,
  countYear,
  countFechaDesde,
  countFechaHasta,
  queryObject
} = storeToRefs(store)

const errorMensaje = ref<string | null>(null)
const isloading = ref(false)

const obtenerConvenios = async () => {
  errorMensaje.value = null
  isloading.value = true

  const result = await ConvenioService.GetConvenios(queryObject.value)

  if (!result.isSuccess) {
    errorMensaje.value = result.error.message
  } else {
    if (
      queryObject.value.CountFirmadosByMesDto ||
      queryObject.value.countFirmadosByRangoDto
    ) {
      // Determinar si es un número (un tipo) o CantidadConveniosDto (ambos tipos)
      if (typeof result.value === 'number') {
        countResult.value = result.value
        countResultBoth.value = null
      } else if (typeof result.value === 'object' && 'cantidadMarcos' in result.value) {
        countResultBoth.value = result.value as CantidadConveniosDto
        countResult.value = null
      }

      if (queryObject.value.CountFirmadosByMesDto) {
        countSearchType.value = 'mes'
        countMonth.value = queryObject.value.CountFirmadosByMesDto.month
        countYear.value = queryObject.value.CountFirmadosByMesDto.year
      } else if (queryObject.value.countFirmadosByRangoDto) {
        countSearchType.value = 'rango'
        countFechaDesde.value = queryObject.value.countFirmadosByRangoDto.desde
        countFechaHasta.value = queryObject.value.countFirmadosByRangoDto.hasta
      }

      showNoResultsMode.value = false
    } else {
      let dataToProcess = result.value
      
      // Manejar respuesta paginada
      if (result.value && typeof result.value === 'object' && 'totalPages' in result.value) {
        const pagedData = result.value as any
        dataToProcess = pagedData.data
        pagination.value = {
          currentPage: pagedData.currentPage,
          totalPages: pagedData.totalPages,
          totalItems: pagedData.totalItems
        }
      } else {
        pagination.value = { currentPage: 1, totalPages: 1, totalItems: 0 }
      }

      listadoConvenios.value = CreateListConveniosDto(dataToProcess, typeofConvenioToSearch.value)

      if (listadoConvenios.value.Type === 'ambos') {
        const ambos = listadoConvenios.value as any
        if (ambos.conveniosMarcos.length === 0 && ambos.conveniosEspecificos.length === 0) {
          showNoResultsMode.value = true
        } else {
          showNoResultsMode.value = false
        }
      } else {
        if (listadoConvenios.value.data.length === 0) {
          showNoResultsMode.value = true
        } else {
          showNoResultsMode.value = false
        }
      }

      console.log('Lista de convenios actualizada:', listadoConvenios.value)

      countResult.value = null
      countResultBoth.value = null
    }
  }

  isloading.value = false
}

const handleOpenofFilterPanel = (type: 'marco' | 'especifico' | 'ambos') => {
  typeofConvenioToSearch.value = type
  filterPanelOpen.value = true
}

const handleFilterSelected = (filterKey: string) => {
  activeFilterComponent.value = filterKey
  // Limpiar filtros previos para evitar búsquedas cruzadas indeseadas
  store.clearAllFilters()
  // Limpiar resultados previos al cambiar de filtro
  queryObject.value.PaginaActual = 1
  queryObject.value.CantidadResultados = 10
  listadoConvenios.value = CreateListConveniosDto(null)
  countResult.value = null
  showNoResultsMode.value = false
}

const resetSearch = () => {
  showNoResultsMode.value = false
  store.clearAllFilters() // Asegurar limpieza total
  listadoConvenios.value = CreateListConveniosDto(null)
  countResult.value = null
  countResultBoth.value = null
}

const closeCountResult = () => {
  countResult.value = null
  countResultBoth.value = null
}

const changePage = (page: number) => {
  queryObject.value.PaginaActual = page
  obtenerConvenios()
}
</script>

<template>
  <div v-if="!showNoResultsMode">
    <div class="d-flex justify-content-center my-4">
      <ul class="nav nav-pills p-1 bg-light rounded-pill shadow-sm">
        <li class="nav-item">
          <button
            class="nav-link rounded-pill px-4 d-flex align-items-center gap-2"
            :class="{ active: typeofConvenioToSearch === 'marco' }"
            @click="
              () => {
                typeofConvenioToSearch = 'marco'
                filterPanelOpen = true
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
            :class="{ active: typeofConvenioToSearch === 'especifico' }"
            @click="
              () => {
                typeofConvenioToSearch = 'especifico'
                filterPanelOpen = true
              }
            "
          >
            <i class="bi bi-file-earmark-text-fill"></i>
            Convenios Específicos
          </button>
        </li>
        <li class="nav-item">
          <button
            class="nav-link rounded-pill px-4 d-flex align-items-center gap-2"
            :class="{ active: typeofConvenioToSearch === 'ambos' }"
            @click="
              () => {
                typeofConvenioToSearch = 'ambos'
                filterPanelOpen = true
              }
            "
          >
            <i class="bi bi-collection-fill"></i>
            Ambos
          </button>
        </li>
      </ul>
    </div>

    <FilterPanel
      :isPanelOpen="filterPanelOpen"
      :typeOfConvenio="typeofConvenioToSearch"
      :QueryObject="queryObject"
      @close-panel="filterPanelOpen = false"
      @filter-selected="handleFilterSelected"
      @DirectSearch="obtenerConvenios"
    />

    <SearchByTitle
      v-if="activeFilterComponent === KeyFilters.ByTitulo"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByTitle>

    <SearchByEmpresa
      v-if="activeFilterComponent === KeyFilters.ByEmpresa"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByEmpresa>

    <SearchByNumeroConvenio
      v-if="activeFilterComponent === KeyFilters.ByNumeroConvenio"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByNumeroConvenio>

    <SearchByNumeroResolucion
      v-if="activeFilterComponent === KeyFilters.ByNumeroResolucion"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByNumeroResolucion>

    <SearchByFechaFin
      v-if="activeFilterComponent === KeyFilters.ByFechaFin"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByFechaFin>

    <SearchByFechaFirma
      v-if="activeFilterComponent === KeyFilters.ByFechaFirma"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByFechaFirma>

    <SearchByAreas
      v-if="activeFilterComponent === KeyFilters.ByArea"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByAreas>

    <SearchByEstado
      v-if="activeFilterComponent === KeyFilters.ByEstado"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByEstado>

    <SearchByAntiguedad
      v-if="activeFilterComponent === KeyFilters.ByAntiguedadDto"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByAntiguedad>

    <SearchByMes
      v-if="activeFilterComponent === KeyFilters.ByMes"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByMes>

    <SearchByAnio
      v-if="activeFilterComponent === KeyFilters.ByAnio"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByAnio>

    <SearchByDesdeHasta
      v-if="activeFilterComponent === KeyFilters.ByDesdeHasta"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchByDesdeHasta>

    <SearchCountByMes
      v-if="activeFilterComponent === KeyFilters.CountFirmadosByMes"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchCountByMes>

    <SearchCountByRango
      v-if="activeFilterComponent === KeyFilters.CountFirmadosByRango"
      @SearchDone="obtenerConvenios"
      :QueryObject="queryObject"
      :type-of-convenio="typeofConvenioToSearch"
    ></SearchCountByRango>

    <CountConveniosResult
      v-if="(countResult !== null || countResultBoth !== null) && typeofConvenioToSearch !== ''"
      :count="countResult"
      :countBoth="countResultBoth"
      :typeOfConvenio="typeofConvenioToSearch as 'marco' | 'especifico' | 'ambos'"
      :searchType="countSearchType!"
      :month="countMonth"
      :year="countYear"
      :fechaDesde="countFechaDesde"
      :fechaHasta="countFechaHasta"
      @close="closeCountResult"
    />
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

  <ConvenioList :convenios="listadoConvenios" :isloading="isloading" @reset-search="resetSearch" />
  
  <div v-if="!isloading && !showNoResultsMode && (listadoConvenios.Type !== 'ambos' ? listadoConvenios.data.length > 0 : (listadoConvenios.conveniosMarcos?.length > 0 || listadoConvenios.conveniosEspecificos?.length > 0))" class="d-flex justify-content-center mb-5">
    <AppPagination 
      :current-page="pagination.currentPage" 
      :total-pages="pagination.totalPages" 
      @page-changed="changePage" 
    />
  </div>
</template>
