<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { EmpresaWithConveniosDto } from '@/Types/Empresa/EmpresaWithConveniosDto'
import { EstadoConvenio, EstadoConvenioTexto } from '@/Types/Enums/Enums'
import { onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()
const empresaId = Number(route.params.id)

const empresaData = ref<EmpresaWithConveniosDto | null>(null)
const isLoading = ref(false)
const error = ref<string | null>(null)

const fetchConvenios = async () => {
  isLoading.value = true
  error.value = null
  try {
    const response = await ApiService.GetConveniosPorEmpresa(empresaId)
    if (response.isSuccess) {
      empresaData.value = response.value as EmpresaWithConveniosDto
    } else {
      error.value = response.error ? response.error.message : 'Error desconocido al cargar datos.'
    }
  } catch (e) {
    console.error(e)
    error.value = 'Ocurrió un error inesperado.'
  } finally {
    isLoading.value = false
  }
}

const formatDate = (dateString?: string | null) => {
  if (!dateString) return 'N/A'
  return new Date(dateString).toLocaleDateString()
}

const getEstadoTexto = (estado: EstadoConvenio) => {
  return EstadoConvenioTexto[estado] || 'Desconocido'
}

const goToMarco = (idConveino: number) => {
  router.push({ name: 'VistaConvenioMarco', params: { id: idConveino } })
}

const goToEspecifico = (idConvenio: number) => {
  router.push({ name: 'VistaConvenioEspecifico', params: { id: idConvenio } })
}

onMounted(() => {
  fetchConvenios()
})
</script>

<template>
  <div class="container-fluid px-4 py-5" v-if="!isLoading && empresaData">
    <!-- Header -->
    <div class="mb-5 border-bottom pb-4">
      <h6 class="text-uppercase text-muted fw-bold mb-1">Empresa</h6>
      <h2 class="fw-normal text-primary mb-3">
        <i class="bi bi-building me-3"></i>{{ empresaData.nombreEmpresa }}
      </h2>
    </div>

    <!-- Error State -->
    <div v-if="error" class="alert alert-danger shadow-sm rounded-4 mb-4" role="alert">
      <i class="bi bi-exclamation-triangle-fill me-2"></i> {{ error }}
    </div>

    <!-- Empty State -->
    <div
      v-if="!empresaData.convenioMarco && empresaData.conveniosEspecificos.length === 0"
      class="text-center py-5"
    >
      <div class="mb-3 text-muted">
        <i class="bi bi-folder-x" style="font-size: 3rem"></i>
      </div>
      <h4 class="text-muted">No existen convenios registrados para esta empresa.</h4>
    </div>

    <div v-else>
      <!-- Convenio Marco Section -->
      <section class="mb-5">
        <h5 class="mb-4 text-dark d-flex align-items-center gap-2">
          <i class="bi bi-folder-fill text-secondary"></i> Convenio Marco
        </h5>

        <div
          v-if="empresaData.convenioMarco"
          class="card border-0 shadow-sm rounded-4 overflow-hidden hover-card-marco"
        >
          <div class="card-body p-4 d-flex flex-wrap align-items-center justify-content-between">
            <div>
              <h6 class="card-title fw-bold text-primary mb-1">
                {{ empresaData.convenioMarco.titulo || 'Sin Título' }}
              </h6>
              <p class="text-muted mb-0">
                <i class="bi bi-calendar-event me-1"></i>
                Vigencia: {{ formatDate(empresaData.convenioMarco.fechaFirmaConvenio) }} -
                {{ formatDate(empresaData.convenioMarco.fechaFin) }}
              </p>
              <span
                :class="[
                  'badge rounded-pill mt-2',
                  empresaData.convenioMarco.estado === EstadoConvenio.Vigente
                    ? 'bg-success'
                    : 'bg-secondary',
                ]"
              >
                {{ getEstadoTexto(empresaData.convenioMarco.estado) }}
              </span>
            </div>
            <button
              class="btn btn-primary rounded-circle shadow-sm btn-lg d-flex align-items-center justify-content-center"
              style="width: 50px; height: 50px"
              @click="goToMarco(empresaData.convenioMarco.id)"
              data-bs-toggle="tooltip"
              data-bs-placement="top"
              title="Ver Detalle"
            >
              <i class="bi bi-arrow-right fs-5"></i>
            </button>
          </div>
          <div class="card-footer bg-light border-0 p-3">
            <small class="text-muted"
              >Convenio N°: {{ empresaData.convenioMarco.numeroconvenio || 'N/A' }}</small
            >
          </div>
        </div>
        <div v-else class="alert alert-light border shadow-sm rounded-4 text-center text-muted p-4">
          <i class="bi bi-info-circle me-2"></i> No se encontró un Convenio Marco activo para esta
          empresa.
        </div>
      </section>

      <!-- Convenios Específicos Section -->
      <section>
        <div class="d-flex justify-content-between align-items-center mb-4">
          <h5 class="mb-0 text-dark d-flex align-items-center gap-2">
            <i class="bi bi-file-earmark-text-fill text-secondary"></i> Convenios Específicos
          </h5>
          <span class="badge bg-secondary rounded-pill">{{
            empresaData.conveniosEspecificos.length
          }}</span>
        </div>

        <div v-if="empresaData.conveniosEspecificos.length > 0" class="row g-4">
          <div
            v-for="especifico in empresaData.conveniosEspecificos"
            :key="especifico.id"
            class="col-12 col-md-6 col-lg-4"
          >
            <div class="card h-100 border-0 shadow-sm rounded-4 hover-card transition-all">
              <div class="card-body p-4 d-flex flex-column">
                <div class="d-flex justify-content-between align-items-start mb-3">
                  <span
                    class="badge bg-light text-primary border border-primary-subtle rounded-pill"
                    >Específico</span
                  >
                  <button
                    class="btn btn-outline-primary btn-sm rounded-circle d-flex align-items-center justify-content-center border-0 bg-primary-subtle text-primary"
                    style="width: 32px; height: 32px"
                    @click="goToEspecifico(especifico.id)"
                  >
                    <i class="bi bi-arrow-up-right"></i>
                  </button>
                </div>

                <h6 class="card-title fw-bold text-dark mb-3 text-truncate-2">
                  {{ especifico.titulo || 'Sin Título' }}
                </h6>

                <div class="mt-auto">
                  <p class="card-text text-muted small mb-2">
                    <i class="bi bi-calendar3 me-1"></i>
                    {{ formatDate(especifico.fechaInicioActividades) }} -
                    {{ formatDate(especifico.fechaFin) }}
                  </p>
                  <p class="card-text text-muted small mb-0">
                    <i class="bi bi-check-circle me-1"></i>
                    Estado:
                    <span
                      :class="
                        especifico.estado === EstadoConvenio.Vigente
                          ? 'text-success fw-bold'
                          : 'text-secondary'
                      "
                      >{{ getEstadoTexto(especifico.estado) }}</span
                    >
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div v-else class="alert alert-light border shadow-sm rounded-4 text-center text-muted p-4">
          <i class="bi bi-files me-2"></i> No hay Convenios Específicos registrados asociados a esta
          empresa.
        </div>
      </section>
    </div>
  </div>

  <!-- Loading State -->
  <div v-else-if="isLoading" class="d-flex justify-content-center align-items-center min-vh-100">
    <div class="spinner-grow text-primary" role="status">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>

  <!-- Full Error State (initial load) -->
  <div v-else-if="error" class="container py-5 text-center">
    <div class="alert alert-danger shadow rounded-4 d-inline-block px-5 py-4">
      <i class="bi bi-exclamation-octagon display-4 d-block mb-3"></i>
      <h4>Algo salió mal</h4>
      <p>{{ error }}</p>
      <button class="btn btn-outline-danger rounded-pill mt-3" @click="router.push('/empresas')">
        Volver a Empresas
      </button>
    </div>
  </div>
</template>

<style scoped>
.hover-card {
  transition:
    transform 0.2s ease-in-out,
    box-shadow 0.2s ease-in-out;
}
.hover-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.08) !important;
}

.hover-card-marco {
  transition:
    transform 0.2s ease-in-out,
    box-shadow 0.2s ease-in-out;
}

.hover-card-marco:hover {
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1) !important;
}

.text-truncate-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.bg-primary-subtle {
  background-color: var(--bs-primary-bg-subtle);
}
</style>
