import { defineStore } from 'pinia'
import type { InfoConvenioMarcoDto, ListConveniosDto } from '@/Types/ViewModels/ViewModels'
import { ref } from 'vue'
import { CreateListConveniosDto } from '@/Factory/ConvenioFactory'
import { useConvenioQuery } from '@/modules/convenios/composables/CreateConvenioQueryObject'
import type { CantidadConveniosDto } from '@/Types/Convenios/CantidadConveniosDto'

export const useConvenioStore = defineStore('convenio', () => {
  // Estado para un convenio individual (detalle)
  const currentConvenioMarco = ref<InfoConvenioMarcoDto | null>(null)
  const lastMarcoId = ref<number | null>(null)

  // --- Estado para el Dashboard (Persistencia) ---
  const listadoConvenios = ref<ListConveniosDto>(CreateListConveniosDto(null))
  const typeofConvenioToSearch = ref<'marco' | 'especifico' | 'ambos' | ''>('marco')
  const filterPanelOpen = ref(true)
  const activeFilterComponent = ref<string | null>(null)
  const showNoResultsMode = ref(false)
  const pagination = ref({ currentPage: 1, totalPages: 1, totalItems: 0 })

  // Estado para resultados de conteo
  const countResult = ref<number | null>(null)
  const countResultBoth = ref<CantidadConveniosDto | null>(null)
  const countSearchType = ref<'mes' | 'rango' | null>(null)
  const countMonth = ref<number | undefined>(undefined)
  const countYear = ref<number | undefined>(undefined)
  const countFechaDesde = ref<string | undefined>(undefined)
  const countFechaHasta = ref<string | undefined>(undefined)

  // Query Composable (mantiene la lógica de filtros)
  const queryComposable = useConvenioQuery()

  function setConvenioMarco(data: InfoConvenioMarcoDto) {
    currentConvenioMarco.value = data
    lastMarcoId.value = data.id
  }

  function clearConvenioMarco() {
    currentConvenioMarco.value = null
    lastMarcoId.value = null
  }

  function invalidateMarcoCache() {
    currentConvenioMarco.value = null
    lastMarcoId.value = null
  }

  function invalidateListadoCache() {
    listadoConvenios.value = CreateListConveniosDto(null)
    pagination.value = { currentPage: 1, totalPages: 1, totalItems: 0 }
    showNoResultsMode.value = false
    countResult.value = null
    countResultBoth.value = null
  }

  function removeFromListado(id: number) {
    const current = listadoConvenios.value as any
    if (!current) return
    if (current.Type === 'ambos') {
      if (Array.isArray(current.conveniosMarcos))
        current.conveniosMarcos = current.conveniosMarcos.filter((c: any) => c.id !== id)
      if (Array.isArray(current.conveniosEspecificos))
        current.conveniosEspecificos = current.conveniosEspecificos.filter((c: any) => c.id !== id)
      current.data = [...(current.conveniosMarcos || []), ...(current.conveniosEspecificos || [])]
      if (pagination.value.totalItems > 0) pagination.value.totalItems -= 1
    } else if (Array.isArray(current.data)) {
      const before = current.data.length
      current.data = current.data.filter((c: any) => c.id !== id)
      if (current.data.length !== before && pagination.value.totalItems > 0)
        pagination.value.totalItems -= 1
    }
  }

  return {
    // Individual
    currentConvenioMarco,
    lastMarcoId,
    setConvenioMarco,
    clearConvenioMarco,
    invalidateMarcoCache,
    invalidateListadoCache,
    removeFromListado,

    // Dashboard state
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
    queryObject: queryComposable.queryObject,
    clearAllFilters: queryComposable.clearAllFilters
  }
})
