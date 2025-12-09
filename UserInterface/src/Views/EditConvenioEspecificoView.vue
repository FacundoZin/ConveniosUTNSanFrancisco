<template>
  <div class="container mt-4 position-relative">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <!-- Botón visible cuando se actualiza el convenio -->
      <button
        @click="irAlConvenio"
        class="btn btn-outline-success d-flex align-items-center gap-2"
        title="Ver convenio"
      >
        <i class="bi bi-arrow-right-circle"></i>
        Ir al convenio
      </button>
    </div>

    <!-- Mensaje de error -->
    <div v-if="errorMensaje" class="alert alert-danger" role="alert">
      {{ errorMensaje }}
    </div>

    <form @submit.prevent="submitForm" class="row g-3">
      <!-- SECCIÓN: Datos del Convenio -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Editar Información de Convenio Específico</h4>

        <div class="row g-3">
          <div class="col-md-6">
            <label class="form-label">Número de Convenio</label>
            <input
              v-model="UpdateConvEspRequest.updateConvenioDto.numeroConvenio"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Título</label>
            <input
              v-model="UpdateConvEspRequest.updateConvenioDto.titulo"
              type="text"
              class="form-control"
            />
          </div>
        </div>

        <div class="row g-3 mt-2">
          <div class="col-md-6">
            <label class="form-label">Fecha de Firma</label>
            <input
              v-model="UpdateConvEspRequest.updateConvenioDto.fechaFirmaConvenio"
              type="date"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Fecha de Inicio de Actividades</label>
            <input
              v-model="UpdateConvEspRequest.updateConvenioDto.fechaInicioActividades"
              type="date"
              class="form-control"
            />
          </div>
        </div>

        <div class="row g-3 mt-2">
          <div class="col-md-6">
            <label class="form-label">Fecha de Fin</label>
            <input
              v-model="UpdateConvEspRequest.updateConvenioDto.fechaFinConvenio"
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
                      v-model="UpdateConvEspRequest.idCarreras"
                    />
                    {{ carrera.nombre }}
                  </label>
                </li>
              </ul>
            </div>
          </div>
        </div>

        <div class="col-12 mt-4">
          <label class="form-label">Comentario Opcional</label>
          <textarea
            v-model="UpdateConvEspRequest.updateConvenioDto.comentarioOpcional"
            class="form-control"
            rows="2"
          ></textarea>
        </div>

        <div class="row g-3 mt-3">
          <div class="col-md-3">
            <label class="form-label">Estado</label>
            <select
              v-model.number="UpdateConvEspRequest.updateConvenioDto.estado"
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
              v-model="UpdateConvEspRequest.updateConvenioDto.numeroResolucion"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-3">
            <label class="form-label">Refrendado</label>
            <select
              v-model="UpdateConvEspRequest.updateConvenioDto.refrendado"
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
              v-model="UpdateConvEspRequest.updateConvenioDto.esActa"
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

      <!-- SECCIÓN: Vincular Convenio Marco -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Convenio Marco Asociado</h4>

        <div v-if="InfoConvenioEspecificoCompleta?.convenioMarco">
          <ConvMarcoCard
            :convenio="InfoConvenioEspecificoCompleta.convenioMarco"
            @desvincular-marco="mostrarModalDesvinculacion('marco', null, '')"
          />
        </div>

        <div v-else>
          <VincularConvMarco :request="UpdateConvEspRequest" />
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Empresa Asociada -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Empresa</h4>

        <div v-if="InfoConvenioEspecificoCompleta && InfoConvenioEspecificoCompleta.empresa">
          <EmpresaCard
            :empresa="InfoConvenioEspecificoCompleta.empresa"
            :allow-edit="true"
            @desvincular-empresa="desvincularEmpresa"
            @actualizar-empresa="handleActualizarEmpresa"
          />
        </div>

        <div v-else class="col-12">
          <div class="col-12 mb-3">
            <div class="form-check form-switch">
              <input
                class="form-check-input"
                type="checkbox"
                id="switchNuevaEmpresa"
                v-model="cargarNuevaEmpresa"
              />
              <label class="form-check-label" for="switchNuevaEmpresa">
                Cargar nueva empresa
              </label>
            </div>
          </div>

          <!-- Empresa existente -->
          <div v-if="!cargarNuevaEmpresa" class="col-md-6">
            <label class="form-label">Seleccionar Empresa</label>
            <select v-model="empresaForm.id" class="form-select" required>
              <option value="" disabled>Seleccionar...</option>
              <option
                v-for="empresa in empresas"
                :key="empresa.idEmpresa"
                :value="empresa.idEmpresa"
              >
                {{ empresa.nombreEmpresa }}
              </option>
            </select>
          </div>

          <!-- Nueva empresa -->
          <div v-else class="col-12 border rounded p-3 bg-white">
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
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Involucrados -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Agregar Involucrados</h4>

        <InvolucradoForm @agregar="agregarInvolucrado" />

        <div class="involucrados-list mt-3">
          <InvolucradosCard
            v-for="(inv, idx) in UpdateConvEspRequest.insertInvolucradosDtos"
            :key="idx"
            :involucrado="inv"
            @eliminar="
              mostrarModalDesvinculacion('involucrado-nuevo', idx, `${inv.nombre} ${inv.apellido}`)
            "
          />
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Involucrados Existentes -->
      <div
        class="p-4 bg-light border rounded mb-4"
        v-if="
          InfoConvenioEspecificoCompleta?.involucrados &&
          InfoConvenioEspecificoCompleta.involucrados.length > 0
        "
      >
        <h4 class="text-primary mb-3">Involucrados Existentes</h4>

        <div class="involucrados-list mt-3">
          <InvolucradosExistingCard
            v-for="(inv, idx) in InfoConvenioEspecificoCompleta?.involucrados"
            :key="idx"
            :involucrado="inv"
            @eliminar="
              mostrarModalDesvinculacion(
                'involucrado-existente',
                inv.id,
                `${inv.nombre} ${inv.apellido}`,
              )
            "
          />
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Vincular Involucrados Existentes Adicionales -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Involucrados Existentes Adicionales</h4>
        <p class="text-muted">
          Selecciona involucrados existentes para vincularlos a este convenio
        </p>
        <InvolucradosExistentesSelector v-model="UpdateConvEspRequest.idsInvolucradosExistentes" />
      </div>

      <div class="col-12 mt-4 text-end">
        <button type="submit" class="btn btn-primary">Actualizar Convenio</button>
      </div>
    </form>

    <!-- Modal de confirmación -->
    <ConfirmacionModal
      :show="modalDesvinculacion.mostrar"
      :titulo="modalDesvinculacion.titulo"
      :mensaje="modalDesvinculacion.mensaje"
      textoConfirmar="Sí, continuar"
      textoCancelar="Cancelar"
      tipo="danger"
      @confirmar="confirmarDesvinculacion"
      @cancelar="cancelarDesvinculacion"
    />

    <div v-if="IsLoading" class="loader-overlay d-flex justify-content-center align-items-center">
      <div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import ConfirmacionModal from '@/Components/ConfirmacionModal.vue'
import ConvMarcoCard from '@/Components/ConvMarcoCard.vue'
import EmpresaCard from '@/Components/EmpresaCard.vue'
import InvolucradoForm from '@/Components/InvolucradoForm.vue'
import InvolucradosCard from '@/Components/InvolucradosCard.vue'
import InvolucradosExistingCard from '@/Components/InvolucradosExistingCard.vue'
import InvolucradosExistentesSelector from '@/Components/InvolucradosExistentesSelector.vue'
import VincularConvMarco from '@/Components/VincularConvMarco.vue'
import { UseUpdateConvEspComposable } from '@/Composables/UpdateConvEspComposable'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import { POSITION, useToast } from 'vue-toastification'
import { useRouter, useRoute } from 'vue-router'
import { ref } from 'vue'

const router = useRouter()
const route = useRoute()
const toast = useToast()

const {
  IsLoading,
  InfoConvenioEspecificoCompleta,
  UpdateConvEspRequest,
  errorMensaje,
  empresas,
  Carreras,
  cargarNuevaEmpresa,
  ConvenioCreado,
  empresaForm,
  involucradosForm,
  submitForm: submitFormLogic,
  GetInfoConvenioEspecifico,
} = UseUpdateConvEspComposable()

const handleActualizarEmpresa = async () => {
  if (UpdateConvEspRequest.value.updateConvenioDto.id) {
    await GetInfoConvenioEspecifico(UpdateConvEspRequest.value.updateConvenioDto.id)
  }
}

// Estado del modal de desvinculación
const modalDesvinculacion = ref({
  mostrar: false,
  tipo: '' as 'empresa' | 'marco' | 'involucrado-nuevo' | 'involucrado-existente' | '',
  id: null as number | null,
  titulo: '',
  mensaje: '',
})

const mostrarModalDesvinculacion = (
  tipo: 'empresa' | 'marco' | 'involucrado-nuevo' | 'involucrado-existente',
  id: number | null,
  nombre: string,
) => {
  let titulo = ''
  let mensaje = ''

  switch (tipo) {
    case 'empresa':
      titulo = 'Desvincular Empresa'
      mensaje = `¿Estás seguro de que querés desvincular la empresa "${nombre}"?`
      break
    case 'marco':
      titulo = 'Desvincular Convenio Marco'
      mensaje = '¿Estás seguro de que querés desvincular el convenio marco asociado?'
      break
    case 'involucrado-nuevo':
      titulo = 'Eliminar Involucrado'
      mensaje = `¿Estás seguro de que querés eliminar a ${nombre}?`
      break
    case 'involucrado-existente':
      titulo = 'Eliminar Involucrado'
      mensaje = `¿Estás seguro de que querés eliminar a ${nombre}?`
      break
  }

  modalDesvinculacion.value = {
    mostrar: true,
    tipo,
    id,
    titulo,
    mensaje,
  }
}

const confirmarDesvinculacion = () => {
  const { tipo, id } = modalDesvinculacion.value

  if (tipo === 'empresa') {
    ejecutarDesvinculacionEmpresa()
  } else if (tipo === 'marco') {
    ejecutarDesvinculacionMarco()
  } else if (tipo === 'involucrado-nuevo' && id !== null) {
    eliminarInvolucrado(id)
  } else if (tipo === 'involucrado-existente' && id !== null) {
    eliminarInvolucradoExistente(id)
  }

  cancelarDesvinculacion()
}

const cancelarDesvinculacion = () => {
  modalDesvinculacion.value = {
    mostrar: false,
    tipo: '',
    id: null,
    titulo: '',
    mensaje: '',
  }
}

const agregarInvolucrado = (nuevo: InsertInvolucradosDto) => {
  involucradosForm.value = [...involucradosForm.value, nuevo]
}

const eliminarInvolucrado = (index: number) => {
  involucradosForm.value.splice(index, 1)
}

const eliminarInvolucradoExistente = (Id: number) => {
  ;(UpdateConvEspRequest.value.idsInvolucraodsEliminados ??= []).push(Id)

  // Remover visualmente de la lista
  if (InfoConvenioEspecificoCompleta.value?.involucrados) {
    InfoConvenioEspecificoCompleta.value.involucrados =
      InfoConvenioEspecificoCompleta.value.involucrados.filter((inv) => inv.id !== Id)
  }
}

const desvincularEmpresa = (id: number) => {
  const nombreEmpresa =
    InfoConvenioEspecificoCompleta.value?.empresa?.nombre_Empresa || 'esta empresa'
  mostrarModalDesvinculacion('empresa', id, nombreEmpresa)
}

const ejecutarDesvinculacionEmpresa = () => {
  if (InfoConvenioEspecificoCompleta.value) {
    InfoConvenioEspecificoCompleta.value.empresa = undefined
    UpdateConvEspRequest.value.desvincularEmpresa = true
  }
}

const ejecutarDesvinculacionMarco = () => {
  if (InfoConvenioEspecificoCompleta.value && InfoConvenioEspecificoCompleta.value.convenioMarco) {
    InfoConvenioEspecificoCompleta.value.convenioMarco = null as any
    UpdateConvEspRequest.value.desvincularConvenioMarco = true
  }
}

const submitForm = async () => {
  try {
    await submitFormLogic()
    toast.success('Convenio editado con éxito')
  } catch (error) {
    toast.error('Error al actualizar el convenio', { position: POSITION.BOTTOM_CENTER })
  }
}

const irAlConvenio = () => {
  const id = ConvenioCreado.value?.id || Number(route.params.id)
  if (id) {
    router.push({ name: 'VistaConvenioEspecifico', params: { id } })
  }
}
</script>
