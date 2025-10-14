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
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.numeroConvenio" type="text" class="form-control"
          required />
      </div>

      <div class="col-md-6">
        <label class="form-label">Título</label>
        <input v-model="ConvenioMarcoRequest.insertConvenioDto.titulo" type="text" class="form-control" required />
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
        <select v-model="ConvenioMarcoRequest.insertConvenioDto.estado" class="form-select" required>
          <option value="" disabled>Seleccionar...</option>
          <option value="Vigente">Vigente</option>
          <option value="Finalizado">Finalizado</option>
          <option value="Suspendido">Suspendido</option>
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

      <!-- EMPRESA EXISTENTE O NUEVA -->
      <div class="col-12 mb-3">
        <div class="form-check form-switch">
          <input class="form-check-input" type="checkbox" id="switchNuevaEmpresa" v-model="cargarNuevaEmpresa" />
          <label class="form-check-label" for="switchNuevaEmpresa">
            Cargar nueva empresa
          </label>
        </div>
      </div>

      <!-- Empresa existente -->
      <div v-if="!cargarNuevaEmpresa" class="col-md-6">
        <label class="form-label">Seleccionar Empresa</label>
        <select v-model="ConvenioMarcoRequest.insertEmpresaDto.id" class="form-select" required>
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
            <input v-model="ConvenioMarcoRequest.insertEmpresaDto.nombre" type="text" class="form-control" required />
          </div>

          <div class="col-md-6">
            <label class="form-label">Razón Social</label>
            <input v-model="ConvenioMarcoRequest.insertEmpresaDto.razonSocial" type="text" class="form-control"
              required />
          </div>

          <div class="col-md-6">
            <label class="form-label">CUIT</label>
            <input v-model="ConvenioMarcoRequest.insertEmpresaDto.cuit" type="text" class="form-control" required />
          </div>

          <div class="col-md-6">
            <label class="form-label">Dirección</label>
            <input v-model="ConvenioMarcoRequest.insertEmpresaDto.direccion" type="text" class="form-control" />
          </div>

          <div class="col-md-6">
            <label class="form-label">Teléfono</label>
            <input v-model="ConvenioMarcoRequest.insertEmpresaDto.telefono" type="text" class="form-control" />
          </div>

          <div class="col-md-6">
            <label class="form-label">Email</label>
            <input v-model="ConvenioMarcoRequest.insertEmpresaDto.email" type="email" class="form-control" />
          </div>
        </div>
      </div>

      <div class="col-12 mt-4">
        <button type="submit" class="btn btn-primary">Cargar Convenio</button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ApiService } from '@/Services/ApiService'
import '@/Styles/FormCargaConvMarco.css'
import {
  createRuquestConvMarc,
  type CargarConvenioMarcoRequestDto
} from '@/Types/ConvenioMarco/CreateConvenioMarco'
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto'
import type { ConvenioCreated } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'

const router = useRouter()
const ConvenioMarcoRequest = ref<CargarConvenioMarcoRequestDto>(createRuquestConvMarc())
const errorMensaje = ref<string | null>(null)
const empresas = ref<ComboBoxEmpresasDto[]>([])
const cargarNuevaEmpresa = ref(false)
const toast = useToast()
const ConvenioCreado = ref<ConvenioCreated | null>(null)

onMounted(() => {
  getEmpresas()
})

const getEmpresas = async () => {
  try {
    const response = await ApiService.GetEmpresas()
    if (response) empresas.value = response
  } catch (err) {
    console.error('Error al obtener empresas', err)
  }
}

const submitForm = async () => {
  errorMensaje.value = null
  try {
    const result = await ApiService.CreateConvenioMarco(ConvenioMarcoRequest.value)
    if (!result.isSuccess) {
      errorMensaje.value = result.error.message
      return
    }
    ConvenioCreado.value = result.value
    resetForm()
    toast.success('Convenio cargado con éxito')
  } catch (error) {
    errorMensaje.value = 'Ocurrió un error al cargar el convenio'
    if (isAxiosError(error)) {
      if (error.response) {
        console.log(`Error al cargar el convenio (${error.response.status}):`, error.response.data)
      } else {
        console.log('Error al cargar el convenio: no se recibió respuesta del servidor')
      }
    } else {
      console.error(error)
      console.log('Error desconocido fuera del entorno HTTP')
    }
  }
}

const irAlConvenio = () => {
  if (ConvenioCreado.value) {
    router.push({ name: 'VistaConvenioMarco', params: { id: ConvenioCreado.value.ID } })
  }
}

const resetForm = () => {
  ConvenioMarcoRequest.value = createRuquestConvMarc()
  errorMensaje.value = null
}
</script>
