<script setup lang="ts">
import ConvenioService from '@/modules/convenios/services/ConvenioService'
import type { InvolucradosWithConveniosDto } from '@/Types/Involucrados/InvolucradosWithConveniosDto'
import { EstadoConvenio, EstadoConvenioTexto } from '@/Types/Enums/Enums'
import { onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useInvolucradoStore } from '../stores/involucradoStore'
import { storeToRefs } from 'pinia'

const route = useRoute()
const router = useRouter()
const involucradoId = Number(route.params.id)

const store = useInvolucradoStore()
const { currentInvolucrado: involucradoData } = storeToRefs(store)

const isLoading = ref(false)
const error = ref<string | null>(null)

const fetchConvenios = async (forceLoad = false) => {
  if (!forceLoad && store.lastInvolucradoId === involucradoId && store.currentInvolucrado) {
    return
  }

  isLoading.value = true
  error.value = null
  try {
    const response = await ConvenioService.GetConveniosPorInvolucrado(involucradoId)
    if (response.isSuccess) {
      store.setInvolucrado(response.value)
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

const goToEspecifico = (idConvenio: number) => {
  router.push({ name: 'VistaConvenioEspecifico', params: { id: idConvenio } })
}

onMounted(() => {
  fetchConvenios()
})
</script>

<template>
  <div class="container-fluid px-4 py-5" v-if="!isLoading && involucradoData">
    <!-- Back Button -->
    <div class="mb-4">
      <button 
        class="btn btn-link text-decoration-none p-0 d-flex align-items-center gap-2 text-primary fw-semibold transition-all hover-translate-x"
        @click="router.push({ name: 'InvolucradosPorArea' })"
      >
        <i class="bi bi-arrow-left-circle-fill fs-4"></i>
        <span>Volver a Involucrados</span>
      </button>
    </div>

    <!-- Header -->
    <div class="mb-5 border-bottom pb-4">
      <h6 class="text-uppercase text-muted fw-bold mb-1">Involucrado</h6>
      <h2 class="fw-normal text-primary mb-3">
        <i class="bi bi-person-badge me-3"></i>{{ involucradoData.nombre }} {{ involucradoData.apellido }}
      </h2>
    </div>

    <!-- Error State -->
    <div v-if="error" class="alert alert-danger shadow-sm rounded-4 mb-4" role="alert">
      <i class="bi bi-exclamation-triangle-fill me-2"></i> {{ error }}
    </div>

    <!-- Empty State -->
    <div
      v-if="involucradoData.conveniosEspecificos.length === 0"
      class="text-center py-5"
    >
      <div class="mb-3 text-muted">
        <i class="bi bi-folder-x" style="font-size: 3rem"></i>
      </div>
      <h4 class="text-muted">No existen convenios registrados para esta persona.</h4>
    </div>

    <div v-else>
      <!-- Convenios Específicos Section -->
      <section>
        <div class="d-flex justify-content-between align-items-center mb-4">
          <h5 class="mb-0 text-dark d-flex align-items-center gap-2">
            <i class="bi bi-file-earmark-text-fill text-secondary"></i> Convenios en los que participa
          </h5>
          <span class="badge bg-secondary rounded-pill">{{
            involucradoData.conveniosEspecificos.length
          }}</span>
        </div>

        <div v-if="involucradoData.conveniosEspecificos.length > 0" class="row g-4">
          <div
            v-for="especifico in involucradoData.conveniosEspecificos"
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
      <button class="btn btn-outline-danger rounded-pill mt-3" @click="router.push('/involucrados-por-area')">
        Volver a Involucrados
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

.hover-translate-x {
  transition: transform 0.2s ease;
}

.hover-translate-x:hover {
  transform: translateX(-5px);
}
</style>
