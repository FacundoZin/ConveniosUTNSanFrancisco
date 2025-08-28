<template>
  <div class="main-container">
    <div v-if="!ConvenioMarco" class="loading-state">
      <p>Cargando convenio...</p>
    </div>

    <div v-else class="content-wrapper">
      <div class="info-card convenio-card">
        <h1 class="card-title">{{ ConvenioMarco.titulo }}</h1>
        <p><strong>Número:</strong> {{ ConvenioMarco.numeroconvenio }}</p>
        <p><strong>Fecha de firma:</strong> {{ ConvenioMarco.fechaFirmaConvenio }}</p>
        <p><strong>Fecha de fin:</strong> {{ ConvenioMarco.fechaFin }}</p>
        <p>
          <strong>Comentario:</strong> {{ ConvenioMarco.comentarioOpcional || 'Sin comentarios' }}
        </p>
      </div>

      <div class="info-card empresa-card">
        <h2 class="card-subtitle">Datos de la Empresa</h2>
        <p><strong>Nombre:</strong> {{ ConvenioMarco.empresa.nombre_Empresa }}</p>
        <p><strong>Razón social:</strong> {{ ConvenioMarco.empresa.razonSocial }}</p>
        <p><strong>CUIT:</strong> {{ ConvenioMarco.empresa.cuit }}</p>
        <p><strong>Dirección:</strong> {{ ConvenioMarco.empresa.direccion_Empresa }}</p>
        <p><strong>Teléfono:</strong> {{ ConvenioMarco.empresa.telefono_Empresa }}</p>
        <p><strong>Email:</strong> {{ ConvenioMarco.empresa.email_Empresa }}</p>
      </div>

      <div class="specific-list-section">
        <h2 class="card-subtitle">Convenios Específicos Asociados</h2>
        <div v-if="ConvenioMarco.conveniosEspecificos.length === 0" class="no-specific-agreements">
          <p>Este convenio marco no tiene convenios específicos asociados.</p>
        </div>
        <table v-else class="specific-table">
          <thead>
            <tr>
              <th>Número</th>
              <th>Título</th>
              <th>Fecha de firma</th>
              <th>Fecha de inicio</th>
              <th>Fecha de fin</th>
              <th>Tipo</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="conv in ConvenioMarco.conveniosEspecificos" :key="conv.id">
              <td>{{ conv.numeroconvenio }}</td>
              <td>{{ conv.titulo }}</td>
              <td>{{ conv.fechaFinConvenio }}</td>
              <td>{{ conv.fechaInicioActividades }}</td>
              <td>{{ conv.fechaFinConvenio }}</td>
              <td>{{ conv.convenioType }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
  <div class="buttons-container" v-if="ConvenioMarco">
    <button @click="editConvenio" class="btn btn-primary">Editar Convenio</button>
    <button @click="CargarEspecifico" class="btn btn-primary">Cargar Convenio Específico</button>
    <button @click="DeleteConvenio" class="btn btn-delete">Eliminar Convenio</button>
  </div>
</template>

<script setup lang="ts">
import router from '@/router'
import ApiService from '@/Services/ApiService'
import '@/Styles/VistaConvenioMarco.css'
import type { ConvenioMarcoCompleto } from '@/Types/Models'
import { isAxiosError } from 'axios'
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { POSITION, useToast } from 'vue-toastification'

const toast = useToast()
const route = useRoute()
const idparam = route.params.id
let id = 0

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0])
} else {
  id = parseInt(idparam)
}

const ConvenioMarco = ref<ConvenioMarcoCompleto | null>(null)

onMounted(async () => {
  try {
    const response = await ApiService.GetConvenioMarcoCompleto(id)
    ConvenioMarco.value = response.data
  } catch (error) {
    toast.error('error al acceder a los datos del convenio marco', { position: POSITION.BOTTOM_CENTER })
    if (isAxiosError(error) && error.response) {
      console.log(error.response.data, error.response.status)
    } else {
      console.log(error)
    }
  }
})

const editConvenio = () => {
  if (ConvenioMarco.value) {
    router.push({
      name: 'EditConvenioMarco',
      params: { id: ConvenioMarco.value.idconvenio },
    })
  }
}

const CargarEspecifico = () => {
  if (ConvenioMarco.value) {
    router.push({
      name: 'CreateConvenioEspecifico',
      params: { id: ConvenioMarco.value.idconvenio },
    })
  }
}

const DeleteConvenio = async () => {
  try {
    if (ConvenioMarco.value) {
      await ApiService.DeleteConvenioMarco(ConvenioMarco.value.idconvenio)
      toast.success(`Convenio "${ConvenioMarco.value.titulo}" eliminado con éxito`)
      router.push({ name: 'ListaConvenios' })
    }
  } catch (error) {
    toast.error(`Error al eliminar el convenio`, { position: POSITION.BOTTOM_CENTER })
    if (isAxiosError(error) && error.response) {
      console.log(`Error: ${error.response.data.message}, ${error.response.data}`)
    } else {
      console.log(`Lo sentimos, algo ha salido mal. ${error}`)
    }
  }
}
</script>
