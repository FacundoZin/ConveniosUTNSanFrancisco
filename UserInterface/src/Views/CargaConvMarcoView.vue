<template>
  <div class="container mt-4 position-relative">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h3 class="text-primary">Cargar Convenio Marco</h3>

      <!-- Botón visible solo cuando se crea un convenio -->
      <button v-if="ConvenioCreado" @click="irAlConvenio"
        class="btn btn-outline-success d-flex align-items-center gap-2" title="Ver convenio creado">
        <i class="bi bi-arrow-right-circle"></i>
        Ver Convenio
      </button>
    </div>

    <!-- Mensaje de error -->
    <div v-if="errorMensaje" class="alert alert-danger" role="alert">
      {{ errorMensaje }}
    </div>

    <form @submit.prevent="submitForm" class="row g-3">
      <!-- DATOS DEL CONVENIO -->
      <div class="col-md-6">
        <label class="form-label">Número de Convenio</label>
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.numeroConvenio" type="text" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Título</label>
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.titulo" type="text" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Fecha de Firma</label>
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.fechaFirmaConvenio" type="date" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Fecha de Fin</label>
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.fechaFin" type="date" class="form-control" />
      </div>

      <div class="col-12">
        <label class="form-label">Comentario Opcional</label>
        <textarea v-model="ConvenioMarcoRequest.insertConvenioDto.comentarioOpcional" class="form-control"
          rows="2"></textarea>
      </div>

      <div class="col-md-4">
        <label class="form-label">Estado</label>
        <select v-model.number="ConvenioMarcoRequest.insertConvenioDto.estado" class="form-select" required>
          <option value="" disabled>Seleccionar...</option>
          <option :value="0">Borrador</option>
          <option :value="1">Vigente</option>
          <option :value="2">Finalizado</option>
        </select>
      </div>

      <div class="col-md-4">
        <label class="form-label">Número de Resolución</label>
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.numeroResolucion" type="text" class="form-control" />
      </div>

      <div class="col-md-4">
        <label class="form-label">Refrendado</label>
        <select v-model="ConvenioMarcoRequest.insertConvenioDto.refrendado" class="form-select" required>
          <option :value="true">Sí</option>
          <option :value="false">No</option>
        </select>
      </div>

      <hr class="my-4" />

      <VincularConvEspecifico @vincular-convenio-marco="VincularConvenioEspecifico" />

      <hr class="my-4" />

      <!-- EMPRESA EXISTENTE O NUEVA -->
      <div class="col-12 mb-3">
        <div class="form-check form-switch">
          <input class="form-check-input" type="checkbox" id="switchNuevaEmpresa" v-model="cargarNuevaEmpresa" />
          <label class="form-check-label" for="switchNuevaEmpresa"> Cargar nueva empresa </label>
        </div>
      </div>

      <!-- Empresa existente -->
      <div v-if="!cargarNuevaEmpresa" class="col-md-6">
        <label class="form-label">Seleccionar Empresa</label>
        <select v-model="empresaForm.id" class="form-select" required>
          <option value="" disabled>Seleccionar...</option>
          <option v-for="empresa in empresas" :key="empresa.idEmpresa" :value="empresa.idEmpresa">
            {{ empresa.nombreEmpresa }}
          </option>
        </select>
      </div>

      <!-- Nueva empresa -->
      <div v-else class="col-12 border rounded p-3 bg-light">
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

      <div class="col-12 mt-4">
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
import VincularConvEspecifico from '@/Components/VincularConvEspecifico.vue'
import { useCreateConvMarcoComposable } from '@/Composables/CreateConvMarcoComposable'
import ApiService from '@/Services/ApiService'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'

const router = useRouter()
const toast = useToast()

const {
  IsLoading,
  ConvenioMarcoRequest,
  errorMensaje,
  empresas,
  cargarNuevaEmpresa,
  ConvenioCreado,
  empresaForm,
  submitForm: submitFormLogic, // Renombramos la función para evitar conflictos
  resetForm,
} = useCreateConvMarcoComposable()

const submitForm = async () => {
  const result = await submitFormLogic()

  if (result) {
    ConvenioCreado.value = result
    resetForm()
    toast.success('Convenio cargado con éxito')
  }
}

const VincularConvenioEspecifico = async (NumeroConvenio: string) => {
  errorMensaje.value = null

  try {
    const result = await ApiService.GetIdConvMarcoByNumeroConv(NumeroConvenio)

    if (result.isSuccess) {
      ConvenioMarcoRequest.value.idsConveniosEspecificosParaVincular ??= []
      ConvenioMarcoRequest.value.idsConveniosEspecificosParaVincular.push(result.value)
      toast.success('convenio especifico vinculado con éxito')
    } else {
      if (result.error.status === 404) {
        errorMensaje.value = 'no se encontro el convenio especifico que esta intentando vincular'
      }
      errorMensaje.value = 'ocurrio un error al vincular el convenio especifico'
    }
  } catch (ex) {
    errorMensaje.value = 'ocurrio un error al vincular el convenio especifico'

    console.log(ex)
  }
}

const irAlConvenio = () => {
  if (ConvenioCreado.value) {
    router.push({ name: 'VistaConvenioMarco', params: { id: ConvenioCreado.value.ID } })
  }
}
</script>
