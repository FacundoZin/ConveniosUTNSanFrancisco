<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import InvolucradoService from '@/modules/involucrados/services/InvolucradoService'
import type { InvolucradosDto } from '@/Types/Involucrados/InvolucradosByArea'
import { useToast } from 'vue-toastification'

const toast = useToast()
const router = useRouter()

const loading = ref(false)
const areaSeleccionada = ref<number | null>(null)
const involucrados = ref<InvolucradosDto[]>([])
const cantidadTotal = ref<number | null>(null)
const busquedaRealizada = ref(false)

const areas = [
  { id: 1, nombre: 'Ingeniería Química', icono: 'bi-flask' },
  { id: 2, nombre: 'Ingeniería en Sistemas', icono: 'bi-laptop' },
  { id: 3, nombre: 'Ingeniería Electrónica', icono: 'bi-cpu' },
  { id: 4, nombre: 'Ingeniería Electromecánica', icono: 'bi-gear' },
  { id: 5, nombre: 'Tecnicatura en Programación', icono: 'bi-code-slash' },
  { id: 6, nombre: 'Materias Básicas', icono: 'bi-book' },
  { id: 7, nombre: 'SEU', icono: 'bi-people' },
  { id: 8, nombre: 'Vinculación Tecnológica', icono: 'bi-briefcase' },
]

const seleccionarArea = (id: number) => {
  if (areaSeleccionada.value === id) {
    areaSeleccionada.value = null
    involucrados.value = []
    cantidadTotal.value = null
    busquedaRealizada.value = false
    return
  }
  areaSeleccionada.value = id
  handleSearch()
}

const handleSearch = async () => {
  if (!areaSeleccionada.value) return

  loading.value = true
  busquedaRealizada.value = false
  involucrados.value = []
  cantidadTotal.value = null

  const result = await InvolucradoService.GetInvolucradosByArea(areaSeleccionada.value)

  loading.value = false
  busquedaRealizada.value = true

  if (result.isSuccess) {
    involucrados.value = result.value.involucrados
    cantidadTotal.value = result.value.cantidad
  } else {
    toast.error(result.error?.message || 'Error al obtener los involucrados')
  }
}

const handleVerConvenios = (id: number) => {
  router.push({ name: 'InvolucradoConvenios', params: { id } })
}
</script>

<template>
  <div class="container-fluid px-4 py-4">
    <div class="row mb-4">
      <div class="col-12">
        <h2 class="text-primary fw-bold mb-2">
          <i class="bi bi-people-fill me-2"></i>Involucrados por área
        </h2>
        <p class="text-muted mb-0">
          Seleccione un área para ver el listado de personas involucradas en convenios.
        </p>
      </div>
    </div>

    <!-- Selector de Áreas -->
    <div class="card border-0 shadow-sm rounded-4 mb-4">
      <div class="card-body p-4">
        <label class="form-label fw-semibold text-muted small text-uppercase mb-3">
          <i class="bi bi-funnel me-1"></i>Filtrar por área
        </label>
        <div class="d-flex flex-wrap gap-2">
          <button
            v-for="area in areas"
            :key="area.id"
            class="area-pill btn rounded-pill px-3 py-2 d-flex align-items-center gap-2 transition-all"
            :class="areaSeleccionada === area.id ? 'active' : ''"
            @click="seleccionarArea(area.id)"
          >
            <i :class="`bi ${area.icono}`"></i>
            <span class="fw-medium small">{{ area.nombre }}</span>
            <span
              v-if="areaSeleccionada === area.id && loading"
              class="spinner-border spinner-border-sm ms-1"
              role="status"
              aria-hidden="true"
            ></span>
            <i
              v-else-if="areaSeleccionada === area.id"
              class="bi bi-check-circle-fill ms-1"
            ></i>
          </button>
        </div>
      </div>
    </div>

    <!-- Resultado Cantidad -->
    <div v-if="cantidadTotal !== null && !loading" class="d-flex justify-content-end mb-4">
      <div class="d-flex align-items-center bg-primary-subtle px-4 py-3 rounded-pill border border-primary-subtle shadow-sm">
        <div class="me-3 text-primary">
          <i class="bi bi-person-lines-fill fs-3"></i>
        </div>
        <div>
          <h6 class="mb-0 text-primary small text-uppercase fw-semibold">Total Involucrados</h6>
          <h4 class="mb-0 fw-bold text-dark">{{ cantidadTotal }}</h4>
        </div>
      </div>
    </div>

    <!-- Loading -->
    <div v-if="loading" class="d-flex justify-content-center my-5">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Resultados Tabla -->
    <div v-if="busquedaRealizada && !loading">
      <div v-if="involucrados.length > 0" class="card border-0 shadow-sm rounded-4">
        <div class="card-body p-0">
          <div class="table-responsive">
            <table class="table table-hover align-middle mb-0">
              <thead class="bg-light">
                <tr>
                  <th class="py-3 ps-4">Nombre Completo</th>
                  <th class="py-3">Email</th>
                  <th class="py-3">Teléfono</th>
                  <th class="py-3">Legajo</th>
                  <th class="py-3">Rol</th>
                  <th class="py-3 text-end pe-4">Acciones</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="involucrado in involucrados" :key="involucrado.id">
                  <td class="ps-4">
                    <div class="fw-bold text-dark">
                      {{ involucrado.nombre }} {{ involucrado.apellido }}
                    </div>
                  </td>
                  <td>
                    <span v-if="involucrado.email" class="text-muted">
                      {{ involucrado.email }}
                    </span>
                    <span v-else class="text-muted fst-italic">No especificado</span>
                  </td>
                  <td>
                    <span v-if="involucrado.telefono">
                      {{ involucrado.telefono }}
                    </span>
                    <span v-else class="text-muted fst-italic">-</span>
                  </td>
                  <td>
                    <span v-if="involucrado.legajo" class="badge bg-light text-dark border">
                      {{ involucrado.legajo }}
                    </span>
                    <span v-else class="text-muted fst-italic small">N/A</span>
                  </td>
                  <td>
                    <span class="badge bg-primary-subtle text-primary border border-primary-subtle rounded-pill">
                      {{ involucrado.rolInvolucrado }}
                    </span>
                  </td>
                  <td class="text-end pe-4">
                    <button
                      class="btn btn-sm btn-outline-primary rounded-pill px-3"
                      @click="handleVerConvenios(involucrado.id)"
                      title="Ver convenios"
                    >
                      <i class="bi bi-eye me-1"></i>
                      Ver convenios
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>

      <div v-else class="text-center py-5 text-muted">
        <i class="bi bi-inbox fs-1 d-block mb-3 opacity-50"></i>
        <h5>No se encontraron resultados</h5>
        <p>No hay involucrados registrados para el área seleccionada.</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.area-pill {
  color: #6c757d;
  background: #f8f9fa;
  border: 2px solid #e9ecef;
  transition: all 0.2s ease;
}

.area-pill:hover {
  color: var(--bs-primary);
  background: var(--bs-primary-bg-subtle);
  border-color: var(--bs-primary-border-subtle);
  transform: translateY(-1px);
}

.area-pill.active {
  color: #fff;
  background: var(--bs-primary);
  border-color: var(--bs-primary);
  box-shadow: 0 2px 8px rgba(13, 110, 253, 0.3);
}
</style>
