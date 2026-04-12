import { defineStore } from 'pinia'
import type { EmpresaWithConveniosDto } from '@/Types/Empresa/EmpresaWithConveniosDto'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import { ref } from 'vue'

export const useEmpresaStore = defineStore('empresa', () => {
  // Detail state
  const currentEmpresa = ref<EmpresaWithConveniosDto | null>(null)
  const lastEmpresaId = ref<number | null>(null)

  // Dashboard state
  const dashboardEmpresas = ref<ComboBoxEmpresasDto[]>([])
  const dashboardPagination = ref({
    currentPage: 1,
    totalPages: 1,
    totalItems: 0,
    pageSize: 12
  })

  function setEmpresa(data: EmpresaWithConveniosDto) {
    currentEmpresa.value = data
    lastEmpresaId.value = data.idEmpresa
  }

  function setDashboardState(data: ComboBoxEmpresasDto[], pagination: any) {
    dashboardEmpresas.value = data
    dashboardPagination.value = { ...pagination }
  }

  function clearEmpresa() {
    currentEmpresa.value = null
    lastEmpresaId.value = null
  }

  return {
    currentEmpresa,
    lastEmpresaId,
    dashboardEmpresas,
    dashboardPagination,
    setEmpresa,
    setDashboardState,
    clearEmpresa
  }
})
