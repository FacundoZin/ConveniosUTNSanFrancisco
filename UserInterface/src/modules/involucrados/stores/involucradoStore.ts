import { defineStore } from 'pinia'
import type { InvolucradosWithConveniosDto } from '@/Types/Involucrados/InvolucradosWithConveniosDto'
import type { InvolucradosDto } from '@/Types/Involucrados/InvolucradosByArea'
import { ref } from 'vue'

export const useInvolucradoStore = defineStore('involucrado', () => {
  // Detail state
  const currentInvolucrado = ref<InvolucradosWithConveniosDto | null>(null)
  const lastInvolucradoId = ref<number | null>(null)

  // Dashboard state
  const dashboardInvolucrados = ref<InvolucradosDto[]>([])
  const dashboardPagination = ref({
    currentPage: 1,
    totalPages: 1,
    totalItems: 0,
    pageSize: 12
  })

  function setInvolucrado(data: InvolucradosWithConveniosDto) {
    currentInvolucrado.value = data
    lastInvolucradoId.value = data.id 
  }

  function setDashboardState(data: InvolucradosDto[], pagination: any) {
    dashboardInvolucrados.value = data
    dashboardPagination.value = { ...pagination }
  }

  function clearInvolucrado() {
    currentInvolucrado.value = null
    lastInvolucradoId.value = null
  }

  return {
    currentInvolucrado,
    lastInvolucradoId,
    dashboardInvolucrados,
    dashboardPagination,
    setInvolucrado,
    setDashboardState,
    clearInvolucrado
  }
})
