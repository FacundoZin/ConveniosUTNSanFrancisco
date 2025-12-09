<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const empresas = ref<ComboBoxEmpresasDto[]>([])
const isLoading = ref(false)
const error = ref<string | null>(null)

const fetchEmpresas = async () => {
  isLoading.value = true
  error.value = null
  try {
    const response = await ApiService.GetEmpresas()
    empresas.value = response
  } catch (e) {
    console.error(e)
    error.value = 'Error al cargar las empresas.'
  } finally {
    isLoading.value = false
  }
}

const navigateToConvenios = (idEmpresa: number) => {
  router.push({ name: 'EmpresaConvenios', params: { id: idEmpresa } })
}

onMounted(() => {
  fetchEmpresas()
})
</script>

<template>
  <div class="container-fluid px-4 py-5">
    <div class="d-flex justify-content-between align-items-center mb-5">
      <h2 class="fw-bold text-primary mb-0">Empresas Registradas</h2>
      <span class="badge bg-primary rounded-pill px-3 py-2 fs-6" v-if="!isLoading">
        {{ empresas.length }} Empresas
      </span>
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
        <div class="card h-100 border-0 shadow-sm rounded-4 hover-card transition-all">
          <div class="card-body p-4 d-flex flex-column align-items-center text-center">
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
</style>
