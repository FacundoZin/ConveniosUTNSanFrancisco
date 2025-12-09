<script setup lang="ts">
import { ref } from 'vue'
import ApiService from '@/Services/ApiService'
import type { InvolucradosDto } from '@/Types/Involucrados/InvolucradosByCarrera'
import { useToast } from 'vue-toastification'

const toast = useToast()

const loading = ref(false)
const carreraSeleccionada = ref<number | null>(null)
const involucrados = ref<InvolucradosDto[]>([])
const cantidadTotal = ref<number | null>(null)
const busquedaRealizada = ref(false)

const carreras = [
  { id: 1, nombre: 'Ingeniería Química' },
  { id: 2, nombre: 'Ingeniería en Sistemas' },
  { id: 3, nombre: 'Ingeniería Electrónica' },
  { id: 4, nombre: 'Ingeniería Electromecánica' },
  { id: 5, nombre: 'Tecnicatura en Programación' },
  { id: 6, nombre: 'Materias Basicas' },
  { id: 7, nombre: 'SEU' },
  { id: 8, nombre: 'Vinculación Tecnológica' },
]

const handleSearch = async () => {
  if (!carreraSeleccionada.value) return

  loading.value = true
  busquedaRealizada.value = false
  involucrados.value = []
  cantidadTotal.value = null

  const result = await ApiService.GetInvolucradosByCarrera(carreraSeleccionada.value)

  loading.value = false
  busquedaRealizada.value = true

  if (result.isSuccess) {
    involucrados.value = result.value.involucrados
    cantidadTotal.value = result.value.cantidad
  } else {
    toast.error(result.error?.message || 'Error al obtener los involucrados')
  }
}
</script>

<template>
  <div class="container-fluid px-4 py-4">
    <div class="row mb-4">
      <div class="col-12">
        <h2 class="text-primary fw-bold mb-3">
          <i class="bi bi-people-fill me-2"></i>Involucrados por Carrera
        </h2>
        <p class="text-muted">
          Seleccione una carrera para ver el listado de personas involucradas.
        </p>
      </div>
    </div>

    <!-- Buscador -->
    <div class="card border-0 shadow-sm mb-4">
      <div class="card-body p-4">
        <div class="row align-items-end g-3">
          <div class="col-md-6 col-lg-4">
            <label class="form-label fw-bold">Carrera</label>
            <select class="form-select" v-model="carreraSeleccionada">
              <option :value="null" disabled>Seleccione una carrera...</option>
              <option v-for="carrera in carreras" :key="carrera.id" :value="carrera.id">
                {{ carrera.nombre }}
              </option>
            </select>
          </div>
          <div class="col-md-auto">
            <button
              class="btn btn-primary w-100"
              @click="handleSearch"
              :disabled="!carreraSeleccionada || loading"
            >
              <span
                v-if="loading"
                class="spinner-border spinner-border-sm me-2"
                role="status"
                aria-hidden="true"
              ></span>
              <i v-else class="bi bi-search me-2"></i>
              Buscar
            </button>
          </div>

          <!-- Resultado Cantidad -->
          <div class="col-md-auto ms-auto" v-if="cantidadTotal !== null">
            <div class="d-flex align-items-center bg-light px-4 py-2 rounded-3 border">
              <div class="me-3 text-primary">
                <i class="bi bi-person-lines-fill fs-3"></i>
              </div>
              <div>
                <h6 class="mb-0 text-muted small text-uppercase">Total Involucrados</h6>
                <h4 class="mb-0 fw-bold text-dark">{{ cantidadTotal }}</h4>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Resultados Tabla -->
    <div v-if="busquedaRealizada">
      <div v-if="involucrados.length > 0" class="card border-0 shadow-sm">
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
                    <span class="badge bg-primary-subtle text-primary border border-primary-subtle">
                      {{ involucrado.rolInvolucrado }}
                    </span>
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
        <p>No hay involucrados registrados para la carrera seleccionada.</p>
      </div>
    </div>
  </div>
</template>
