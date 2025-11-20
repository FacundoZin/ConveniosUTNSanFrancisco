<template>
  <div class="container mt-4 position-relative">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <!-- Botón visible solo cuando se crea un convenio -->
      <button
        @click="IrAConvenio()"
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
        <h4 class="text-primary mb-3">Editar Información de Convenio Marco</h4>

        <div class="row g-3">
          <div class="col-md-6">
            <label class="form-label">Número de Convenio</label>
            <input
              v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.numeroConvenio"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Título</label>
            <input
              v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.titulo"
              type="text"
              class="form-control"
            />
          </div>
        </div>

        <div class="row g-3 mt-2">
          <div class="col-md-6">
            <label class="form-label">Fecha de Firma</label>
            <input
              v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.fechaFirmaConvenio"
              type="date"
              class="form-control"
            />
          </div>

          <div class="col-md-6">
            <label class="form-label">Fecha de Fin</label>
            <input
              v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.fechaFin"
              type="date"
              class="form-control"
            />
          </div>
        </div>

        <div class="col-12 mt-4">
          <label class="form-label">Comentario Opcional</label>
          <textarea
            v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.comentarioOpcional"
            class="form-control"
            rows="2"
          ></textarea>
        </div>

        <div class="row g-3 mt-3">
          <div class="col-md-4">
            <label class="form-label">Estado</label>
            <select
              v-model.number="ConvenioMarcoRequest.updateConvenioMarcoDto.estado"
              class="form-select"
              required
            >
              <option value="" disabled>Seleccionar...</option>
              <option :value="0">Borrador</option>
              <option :value="1">Vigente</option>
              <option :value="2">Finalizado</option>
            </select>
          </div>

          <div class="col-md-4">
            <label class="form-label">Número de Resolución</label>
            <input
              v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.numeroResolucion"
              type="text"
              class="form-control"
            />
          </div>

          <div class="col-md-4">
            <label class="form-label">Refrendado</label>
            <select
              v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.refrendado"
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

      <!-- SECCIÓN: Vincular Convenio Específico -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Convenio Específico</h4>
        <div class="d-flex flex-column">
          <label for="numeroConvenioEspecifico" class="form-label"
            >Ingrese el número del convenio específico que desea vincular</label
          >
          <input
            id="numeroConvenioEspecifico"
            v-model="ConvenioMarcoRequest.numeroConvenioEspecificosParaVincular"
            type="text"
            class="form-control"
            placeholder="Ej: CE-2025-001"
          />
          <div class="form-text text-muted">
            El convenio se vinculará automáticamente al ingresar el número.
          </div>
        </div>
      </div>

      <hr class="my-4" />

      <!-- SECCIÓN: Empresa Asociada -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Vincular Empresa</h4>

        <div v-if="infoConvenioMarcoCompleta && infoConvenioMarcoCompleta.empresa">
          <EmpresaCard
            :empresa="infoConvenioMarcoCompleta.empresa"
            @desvincular-empresa="desvincularEmpresa"
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
            <select v-model="selectedEmpresaId" class="form-select" required>
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

      <!-- SECCIÓN: Convenios Específicos Asociados -->
      <div class="p-4 bg-light border rounded mb-4">
        <h4 class="text-primary mb-3">Convenios Específicos Asociados</h4>

        <ConveniosEspecificosTable
          :convenios="infoConvenioMarcoCompleta?.conveniosEspecificos"
          @ir-a-convenio="IrAConvenioEspecifico"
          @desvincular="mostrarModalDesvinculacion"
        />
      </div>

      <div class="col-12 mt-4 text-end">
        <button type="submit" class="btn btn-primary">Actualizar Convenio</button>
      </div>
    </form>

    <!-- Modal de confirmación -->
    <ConfirmacionModal
      :show="modalDesvinculacion.mostrar"
      titulo="Desvincular Convenio Específico"
      :mensaje="modalDesvinculacion.mensaje"
      textoConfirmar="Sí, desvincular"
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
import ConveniosEspecificosTable from '@/Components/ConveniosEspecificosTable.vue'
import EmpresaCard from '@/Components/EmpresaCard.vue'
import { useUpdateConvMarcoComposable } from '@/Composables/UpdateConvMarcoComposable'
import { POSITION, useToast } from 'vue-toastification'
import { ref } from 'vue'

const toast = useToast()
const {
  infoConvenioMarcoCompleta,
  ConvenioMarcoRequest,
  IsLoading,
  errorMensaje,
  cargarNuevaEmpresa,
  empresas,
  empresaForm,
  selectedEmpresaId,
  DesvincularConvenioEspecificos,
  submitForm: submitFormLogic,
  IrAConvenio,
  IrAConvenioEspecifico,
} = useUpdateConvMarcoComposable()

// Estado del modal de desvinculación
const modalDesvinculacion = ref({
  mostrar: false,
  convenioId: null as number | null,
  mensaje: '',
})

const mostrarModalDesvinculacion = (id: number, tituloConvenio: string) => {
  modalDesvinculacion.value = {
    mostrar: true,
    convenioId: id,
    mensaje: `¿Estás seguro de que querés desvincular el convenio "${tituloConvenio}"?`,
  }
}

const confirmarDesvinculacion = () => {
  if (modalDesvinculacion.value.convenioId !== null) {
    DesvincularConvenioEspecificos(modalDesvinculacion.value.convenioId)
    toast.success('Convenio específico desvinculado')
  }
  cancelarDesvinculacion()
}

const cancelarDesvinculacion = () => {
  modalDesvinculacion.value = {
    mostrar: false,
    convenioId: null,
    mensaje: '',
  }
}

const submitForm = async () => {
  console.log('📤 Datos a enviar al backend:', JSON.stringify(ConvenioMarcoRequest.value, null, 2))
  console.log('📦 Objeto completo:', ConvenioMarcoRequest.value)

  try {
    await submitFormLogic()
    toast.success('Convenio editado con éxito')
  } catch (error) {
    toast.error('Error al editar el convenio', { position: POSITION.BOTTOM_CENTER })
  }
}

const desvincularEmpresa = () => {
  if (infoConvenioMarcoCompleta.value) {
    infoConvenioMarcoCompleta.value.empresa = undefined
    ConvenioMarcoRequest.value.empresaDesvinculada = true
  }
}
</script>

