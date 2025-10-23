<template>
  <div class="container mt-4 position-relative">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h3 class="text-primary">Cargar Convenio Especifico</h3>

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

      <div class="col-md-6">
        <label class="form-label">Fecha de Fin</label>
        <input
          v-model="UpdateConvEspRequest.updateConvenioDto.fechaFinConvenio"
          type="date"
          class="form-control"
        />
      </div>

      <div class="col-md-6">
        <label class="form-label">Carrera</label>
        <select v-model="UpdateConvEspRequest.idCarreras" class="form-select" multiple>
          <option value="" disabled>Seleccionar</option>
          <option v-for="carrera in Carreras" :key="carrera.Id" :value="carrera.Id">
            {{ carrera.Nombre }}
          </option>
        </select>
      </div>

      <div class="col-12">
        <label class="form-label">Comentario Opcional</label>
        <textarea
          v-model="UpdateConvEspRequest.updateConvenioDto.comentarioOpcional"
          class="form-control"
          rows="2"
        ></textarea>
      </div>

      <div class="col-md-4">
        <label class="form-label">Estado</label>
        <select
          v-model.number="UpdateConvEspRequest.updateConvenioDto.estado"
          class="form-select"
          required
        >
          <option value="" disabled>Seleccionar...</option>
          <option :value="0">Borrador</option>
          <option :value="1">Vigente</option>
          <option :value="3">Finalizado</option>
        </select>
      </div>

      <div class="col-md-4">
        <label class="form-label">Número de Resolución</label>
        <input
          v-model="UpdateConvEspRequest.updateConvenioDto.numeroResolucion"
          type="text"
          class="form-control"
        />
      </div>

      <div class="col-md-4">
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

      <div class="col-md-4">
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

      <hr class="my-4" />

      <!-- Empresa o formulario -->
      <div class="col-12 mb-3">
        <div v-if="InfoConvenioEspecificoCompleta?.empresa">
          <!-- Solo muestra la empresa asociada -->
          <EmpresaAsociada
            :empresa="InfoConvenioEspecificoCompleta.empresa"
            @onDesvincularEmpresa="desvincularEmpresa"
          />
        </div>

        <div v-else>
          <!-- Switch + formulario de empresa -->
          <div class="form-check form-switch">
            <input
              class="form-check-input"
              type="checkbox"
              id="switchNuevaEmpresa"
              v-model="cargarNuevaEmpresa"
            />
            <label class="form-check-label" for="switchNuevaEmpresa"> Cargar nueva empresa </label>
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
        </div>
      </div>

      <hr class="my-4" />

      <VincularConvMarco @vincular-convenio-marco="VincularConvenioMarco" />

      <hr class="my-4" />

      <div class="col-12 involucrados-section">
        <h3 class="involucrados-title text-primary mb-3">👥 Agregar Involucrados</h3>

        <InvolucradoForm @agregar="agregarInvolucrado" />

        <div class="involucrados-list mt-3">
          <InvolucradosCard
            v-for="(inv, idx) in UpdateConvEspRequest.insertInvolucradosDtos"
            :key="idx"
            :involucrado="inv"
            @eliminar="eliminarInvolucrado(idx)"
          />
        </div>
      </div>

      <hr class="my-4" />

      <div class="col-12 involucrados-section">
        <h3 class="involucrados-title text-primary mb-3">👥 Involucrados Existentes</h3>

        <div class="involucrados-list mt-3">
          <InvolucradosExistingCard
            v-for="(inv, idx) in InfoConvenioEspecificoCompleta?.involucrados"
            :key="idx"
            :involucrado="inv"
            @eliminar="eliminarInvolucradoExistente(idx)"
          />
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
import EmpresaAsociada from '@/Components/EmpresaAsociada.vue'
import InvolucradoForm from '@/Components/InvolucradoForm.vue'
import InvolucradosCard from '@/Components/InvolucradosCard.vue'
import InvolucradosExistingCard from '@/Components/InvolucradosExistingCard.vue'
import VincularConvMarco from '@/Components/VincularConvMarco.vue'
import { UseUpdateConvEspComposable } from '@/Composables/UpdateConvEspComposable'
import ApiService from '@/Services/ApiService'
import '@/Styles/FormCargaConvEspecifico.css'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'

const router = useRouter()

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
  submitForm,
} = UseUpdateConvEspComposable()

const toast = useToast()

const agregarInvolucrado = (nuevo: InsertInvolucradosDto) => {
  involucradosForm.value.push(nuevo)
}

const eliminarInvolucrado = (index: number) => {
  involucradosForm.value.splice(index, 1)
}

const eliminarInvolucradoExistente = (Id: number) => {
  ;(UpdateConvEspRequest.value.idsInvolucraodsEliminados ??= []).push(Id)
}

const desvincularEmpresa = () => {
  InfoConvenioEspecificoCompleta.value!.empresa = undefined
  UpdateConvEspRequest.value.desvincularEmpresa = true
}

const guardarConvenio = async () => {
  const result = await submitForm()

  if (result) {
    ConvenioCreado.value = result
    toast.success('Convenio cargado con éxito')
  }
}

const VincularConvenioMarco = async (NumeroConvenio: string) => {
  errorMensaje.value = null

  try {
    const result = await ApiService.GetIdConvMarcoByNumeroConv(NumeroConvenio)

    if (result.isSuccess) {
      UpdateConvEspRequest.value.idConvenioMarcoVinculado = result.value
      toast.success('convenio marco vinculado con éxito')
    } else {
      if (result.error.status === 404) {
        errorMensaje.value = 'no se encontro el convenio marco que esta intentando vincular'
      }
      errorMensaje.value = 'ocurrio un error al vincular el convenio marco'
    }
  } catch (ex) {
    errorMensaje.value = 'ocurrio un error al vincular el convenio marco'

    console.log(ex)
  }
}

const irAlConvenio = () => {
  if (ConvenioCreado.value) {
    router.push({ name: 'VistaConvenioEspecifico', params: { id: ConvenioCreado.value.ID } })
  }
}
</script>
