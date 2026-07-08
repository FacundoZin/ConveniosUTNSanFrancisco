<script setup lang="ts">
import EmpresaService from '@/modules/empresas/services/EmpresaService';
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { EmpresaDto } from '@/Types/ViewModels/ViewModels'
import CreateEmpresaModal from '@/modules/empresas/components/modals/CreateEmpresaModal.vue'
import EditEmpresaModal from '@/modules/empresas/components/modals/EditEmpresaModal.vue'
import AppPagination from '@/modules/shared/components/AppPagination.vue'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useEmpresaStore } from '../stores/empresaStore'
import { storeToRefs } from 'pinia'
import { POSITION, useToast } from 'vue-toastification'

const router = useRouter()
const toast = useToast()
const store = useEmpresaStore()
const { dashboardEmpresas: empresas, dashboardPagination: pagination } = storeToRefs(store)

const isLoading = ref(false)
const error = ref<string | null>(null)
const showCreateModal = ref(false)
const editEmpresaData = ref<EmpresaDto | null>(null)
const showEditModal = ref(false)
const loadingEditId = ref<number | null>(null)

const fetchEmpresas = async (page: number = 1, forceLoad = false) => {
  if (!forceLoad && empresas.value.length > 0 && pagination.value.currentPage === page) {
    return
  }

  isLoading.value = true
  error.value = null
  try {
    const response = await EmpresaService.GetEmpresasPaginado(page, pagination.value.pageSize)
    if (response.exit) {
      store.setDashboardState(response.data, {
        currentPage: response.currentPage,
        totalPages: response.totalPages,
        totalItems: response.totalItems,
        pageSize: pagination.value.pageSize
      })
    } else {
      error.value = response.errormessage || 'Error al cargar las empresas.'
    }
  } catch (e) {
    console.error(e)
    error.value = 'Error al cargar las empresas.'
  } finally {
    isLoading.value = false
  }
}

const handlePageChange = (page: number) => {
  fetchEmpresas(page, true)
}

const navigateToConvenios = (idEmpresa: number) => {
  router.push({ name: 'EmpresaConvenios', params: { id: idEmpresa } })
}

const handleEditClick = async (empresa: ComboBoxEmpresasDto) => {
  loadingEditId.value = empresa.idEmpresa
  const result = await EmpresaService.GetEmpresaById(empresa.idEmpresa)
  loadingEditId.value = null

  if (result.isSuccess && result.value) {
    editEmpresaData.value = result.value
    showEditModal.value = true
  } else {
    toast.error('No se pudo cargar la información de la empresa', { position: POSITION.BOTTOM_CENTER })
  }
}

const handleEditSuccess = () => {
  showEditModal.value = false
  editEmpresaData.value = null
  fetchEmpresas(pagination.value.currentPage, true)
}

onMounted(() => {
  fetchEmpresas(pagination.value.currentPage)
})
</script>

<template>
  <div class="container-fluid px-4 py-5">
    <div class="d-flex justify-content-between align-items-center mb-5">
      <div>
        <h2 class="fw-bold text-primary mb-0">Empresas Registradas</h2>
        <span
          class="badge bg-primary-subtle text-primary border border-primary-subtle rounded-pill px-3 py-2 mt-2"
          v-if="!isLoading"
        >
          {{ pagination.totalItems }} Empresas en el sistema
        </span>
      </div>
      <button
        class="btn btn-primary btn-lg rounded-pill shadow-sm px-4 d-flex align-items-center gap-2 transition-all hover-lift"
        @click="showCreateModal = true"
      >
        <i class="bi bi-plus-circle-fill fs-5"></i>
        <span class="fw-semibold">Registrar Empresa</span>
      </button>
    </div>

    <!-- Loading State -->
    <div v-if="isLoading" class="d-flex justify-content-center align-items-center min-vh-50">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Error State -->
    <div v-else-if="error" class="alert alert-danger shadow-sm rounded-4" role="alert">
      <i class="bi bi-exclamation-triangle-fill me-2"></i>
      {{ error }}
    </div>

    <!-- Empty State -->
    <div v-else-if="empresas.length === 0" class="text-center py-5">
      <div class="mb-3 text-muted">
        <i class="bi bi-building-slash" style="font-size: 3rem"></i>
      </div>
      <h4 class="text-muted">No hay empresas registradas</h4>
    </div>

    <!-- Grid -->
    <div v-else class="row g-4">
      <div
        v-for="empresa in empresas"
        :key="empresa.idEmpresa"
        class="col-12 col-md-6 col-lg-4 col-xl-3"
      >
        <div class="card h-100 border-0 shadow-sm rounded-4 hover-card transition-all position-relative">
          <!-- Edit button - top right -->
          <button
            class="btn btn-sm btn-light rounded-circle position-absolute top-0 end-0 m-2 shadow-sm edit-btn d-flex align-items-center justify-content-center"
            style="width: 36px; height: 36px"
            :disabled="loadingEditId === empresa.idEmpresa"
            @click.stop="handleEditClick(empresa)"
            title="Editar empresa"
          >
            <span
              v-if="loadingEditId === empresa.idEmpresa"
              class="spinner-border spinner-border-sm"
              role="status"
              aria-hidden="true"
            ></span>
            <i v-else class="bi bi-pencil-fill text-secondary fs-6"></i>
          </button>

          <div class="card-body p-4 d-flex flex-column align-items-center text-center pt-5">
            <div
              class="icon-wrapper bg-light rounded-circle d-flex align-items-center justify-content-center mb-3 text-primary"
              style="width: 64px; height: 64px"
            >
              <i class="bi bi-building fs-3"></i>
            </div>

            <h5 class="card-title fw-bold text-dark mb-3 text-truncate w-100">
              {{ empresa.nombreEmpresa }}
            </h5>

            <div class="mt-auto pt-3 w-100">
              <button
                class="btn btn-outline-primary rounded-pill w-100 d-flex align-items-center justify-content-center gap-2"
                @click="navigateToConvenios(empresa.idEmpresa)"
              >
                <span>Ver Convenios</span>
                <i class="bi bi-arrow-right-circle-fill"></i>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Pagination -->
    <div v-if="!isLoading && pagination.totalPages > 1" class="d-flex justify-content-center mt-5">
      <AppPagination
        :current-page="pagination.currentPage"
        :total-pages="pagination.totalPages"
        @page-changed="handlePageChange"
      />
    </div>

    <!-- Create Modal -->
    <CreateEmpresaModal
      :show="showCreateModal"
      @close="showCreateModal = false"
      @success="fetchEmpresas(pagination.currentPage, true)"
    />

    <!-- Edit Modal -->
    <EditEmpresaModal
      v-if="editEmpresaData"
      :show="showEditModal"
      :empresa="editEmpresaData"
      @close="showEditModal = false; editEmpresaData = null"
      @success="handleEditSuccess"
    />
  </div>
</template>

<style scoped>
.min-vh-50 {
  min-height: 50vh;
}

.hover-card {
  transition:
    transform 0.2s ease-in-out,
    box-shadow 0.2s ease-in-out;
}

.hover-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1) !important;
}

.icon-wrapper {
  transition: background-color 0.2s;
}

.hover-card:hover .icon-wrapper {
  background-color: var(--bs-primary-bg-subtle) !important;
  color: var(--bs-primary) !important;
}

.hover-lift {
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.hover-lift:hover {
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(13, 110, 253, 0.3) !important;
}

.edit-btn {
  opacity: 0;
  transition: opacity 0.2s ease, background-color 0.2s ease;
  z-index: 2;
}

.hover-card:hover .edit-btn {
  opacity: 1;
}

.edit-btn:hover {
  background-color: var(--bs-primary) !important;
}

.edit-btn:hover i {
  color: white !important;
}
</style>
