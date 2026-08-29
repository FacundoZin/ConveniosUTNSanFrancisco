<template>
  <div class="container mt-4 position-relative">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <button
        v-if="ConvenioCreado"
        @click="irAlConvenio"
        class="btn btn-outline-success d-flex align-items-center gap-2"
        title="Ver convenio creado"
      >
        <i class="bi bi-arrow-right-circle"></i>
        Ver Convenio
      </button>
    </div>

    <div v-if="errorMensaje" class="alert alert-danger" role="alert">
      {{ errorMensaje }}
    </div>

    <form @submit.prevent="guardarConvenio" class="row g-3">
      <!-- SECCIÓN: Datos del Convenio -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Cargar Información de Convenio Específico</h4>

        <div class="row g-3">
          <div class="col-md-6">
            <label class="form-label">Número de Convenio</label>
            <input
              v-model="ConvenioEspecificoRequest.insertConvenioDto.numeroConvenio"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Título</label>
            <input
              v-model="ConvenioEspecificoRequest.insertConvenioDto.titulo"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Fecha de Firma</label>
            <input
              v-model="ConvenioEspecificoRequest.insertConvenioDto.fechaFirmaConvenio"
              type="date"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Fecha de Inicio de Actividades</label>
            <input
              v-model="ConvenioEspecificoRequest.insertConvenioDto.fechaInicioActividades"
              type="date"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Fecha de Fin</label>
            <input
              v-model="ConvenioEspecificoRequest.insertConvenioDto.fechaFinConvenio"
              type="date"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Carreras</label>

            <div class="dropdown w-100">
              <button
                class="btn btn-light border w-100 text-start"
                type="button"
                data-bs-toggle="dropdown"
              >
                Seleccionar...
              </button>

              <ul class="dropdown-menu w-100" data-bs-auto-close="outside">
                <li v-for="carrera in Carreras" :key="carrera.id" @click.stop>
                  <label class="dropdown-item d-flex align-items-center gap-2">
                    <input
                      type="checkbox"
                      :value="carrera.id"
                      v-model="ConvenioEspecificoRequest.idCarreras"
                    />
                    {{ carrera.nombre }}
                  </label>
                </li>
              </ul>
            </div>
          </div>

          <div class="col-12">
            <label class="form-label">Comentario Opcional</label>
            <textarea
              v-model="ConvenioEspecificoRequest.insertConvenioDto.comentarioOpcional"
              class="form-control"
              rows="2"
            ></textarea>
          </div>

          <div class="col-md-3">
            <label class="form-label">Estado</label>
            <select
              v-model.number="ConvenioEspecificoRequest.insertConvenioDto.estado"
              class="form-select"
              required
            >
              <option value="" disabled>Seleccionar...</option>
              <option :value="0">Borrador</option>
              <option :value="1">Vigente</option>
              <option :value="2">Finalizado</option>
            </select>
          </div>

          <div class="col-md-3">
            <label class="form-label">Número de Resolución</label>
            <input
              v-model="ConvenioEspecificoRequest.insertConvenioDto.numeroResolucion"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-3">
            <label class="form-label">Refrendado</label>
            <select
              v-model="ConvenioEspecificoRequest.insertConvenioDto.refrendado"
              class="form-select"
              required
            >
              <option :value="true">Sí</option>
              <option :value="false">No</option>
            </select>
          </div>

          <div class="col-md-3">
            <label class="form-label">Es acta</label>
            <select
              v-model="ConvenioEspecificoRequest.insertConvenioDto.esActa"
              class="form-select"
              required
            >
              <option :value="true">Sí</option>
              <option :value="false">No</option>
            </select>
          </div>
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Empresa -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Convenio con Empresa</h4>

        <div class="col-12 mb-3">
          <div class="form-check form-switch">
            <input
              class="form-check-input"
              type="checkbox"
              id="switchNuevaEmpresa"
              v-model="cargarNuevaEmpresa"
            />
            <label class="form-check-label" for="switchNuevaEmpresa">Cargar nueva empresa</label>
          </div>
        </div>

        <div v-if="!cargarNuevaEmpresa" class="col-md-6">
          <label class="form-label">Seleccionar Empresa</label>
          <SearchableSelect
            v-model="empresaForm.id"
            :options="empresaOptions"
            placeholder="Buscar o seleccionar empresa..."
          />
        </div>

        <div v-else class="border rounded p-3 bg-white">
          <h5 class="mb-3">Nueva Empresa</h5>

          <div class="row g-3">
            <div class="col-md-6">
              <label class="form-label">Nombre</label>
              <input v-model="empresaForm.nombre" type="text" class="form-control" required />
            </div>

            <div class="col-md-6">
              <label class="form-label">Razón Social</label>
              <input v-model="empresaForm.razonSocial" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">CUIT</label>
              <input v-model="empresaForm.cuit" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">Dirección</label>
              <input v-model="empresaForm.direccion" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">Teléfono</label>
              <input v-model="empresaForm.telefono" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">Email</label>
              <input v-model="empresaForm.email" type="email" class="form-control" />
            </div>
          </div>
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Convenio Marco -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Convenio Marco</h4>
        <VincularConvMarco :request="ConvenioEspecificoRequest" />
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Involucrados -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Cargar Involucrados</h4>

        <InvolucradoForm :involucrados-existentes="involucradosForm" @agregar="agregarInvolucrado" />

        <div class="mt-4 d-flex flex-wrap gap-3">
          <InvolucradosCard
            v-for="(inv, idx) in involucradosForm"
            :key="idx"
            :involucrado="inv"
            @eliminar="eliminarInvolucrado(idx)"
          />
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Seleccionar Involucrados Existentes -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Involucrados Existentes</h4>
        <InvolucradosExistentesSelector
          v-model="ConvenioEspecificoRequest.idsInvolucradosExistentes"
          @agregar-area="agregarArea"
        />
      </div>

      <!-- BOTÓN -->
      <div class="col-12 text-end">
        <button type="submit" class="btn btn-primary">Cargar Convenio</button>
      </div>
    </form>

    <div v-if="IsLoading" class="loader-overlay d-flex justify-content-center align-items-center">
      <div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import InvolucradoForm from '@/modules/involucrados/components/InvolucradoForm.vue'
import InvolucradosCard from '@/modules/involucrados/components/InvolucradosCard.vue'
import InvolucradosExistentesSelector from '@/modules/involucrados/components/InvolucradosExistentesSelector.vue'
import VincularConvMarco from '@/modules/convenios/components/VincularConvMarco.vue'
import SearchableSelect from '@/modules/shared/components/SearchableSelect.vue'
import { useCreateConvEspComposable } from '@/modules/convenios/composables/CreateConvEspComposable'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import { RolInvolucrado } from '@/Types/Enums/Enums'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import { useConvenioStore } from '@/modules/convenios/stores/convenioStore'

const router = useRouter()
const convenioStore = useConvenioStore()

const {
  IsLoading,
  ConvenioEspecificoRequest,
  errorMensaje,
  empresas,
  Carreras,
  cargarNuevaEmpresa,
  ConvenioCreado,
  empresaForm,
  involucradosForm,
  submitForm: submitFormLogic,
  resetForm,
} = useCreateConvEspComposable()

const toast = useToast()

const empresaOptions = computed(() => {
  return empresas.value.map((e) => ({
    id: e.idEmpresa,
    label: e.nombreEmpresa,
  }))
})

const agregarInvolucrado = (nuevo: InsertInvolucradosDto) => {
  const dup = involucradosForm.value.some(
    (inv) =>
      (inv.nombre ?? '').toLowerCase().trim() === (nuevo.nombre ?? '').toLowerCase().trim() &&
      (inv.apellido ?? '').toLowerCase().trim() === (nuevo.apellido ?? '').toLowerCase().trim() &&
      (inv.telefono ?? '').trim() === (nuevo.telefono ?? '').trim(),
  )
  if (dup) {
    toast.error('Involucrado duplicado: ya existe con el mismo nombre, apellido y teléfono')
    return
  }
  involucradosForm.value.push(nuevo)

  if (
    nuevo.idCarrera != null &&
    nuevo.idCarrera !== 0 &&
    nuevo.rolInvolucrado !== RolInvolucrado.Externo
  ) {
    const ids = ConvenioEspecificoRequest.value.idCarreras ?? []
    if (!ids.includes(nuevo.idCarrera)) {
      ConvenioEspecificoRequest.value.idCarreras = [...ids, nuevo.idCarrera]
    }
  }
}

const agregarArea = (idCarrera: number) => {
  const ids = ConvenioEspecificoRequest.value.idCarreras ?? []
  if (!ids.includes(idCarrera)) {
    ConvenioEspecificoRequest.value.idCarreras = [...ids, idCarrera]
  }
}

const eliminarInvolucrado = (index: number) => {
  involucradosForm.value.splice(index, 1)
}

const guardarConvenio = async () => {
  const result = await submitFormLogic()

  if (result) {
    ConvenioCreado.value = result
    convenioStore.invalidateMarcoCache()
    resetForm()
    toast.success('Convenio cargado con éxito')
  } else if (errorMensaje.value) {
    toast.error(errorMensaje.value)
  }
}

const irAlConvenio = () => {
  if (ConvenioCreado.value) {
    router.push({ name: 'VistaConvenioEspecifico', params: { id: ConvenioCreado.value.id } })
  }
}
</script>
