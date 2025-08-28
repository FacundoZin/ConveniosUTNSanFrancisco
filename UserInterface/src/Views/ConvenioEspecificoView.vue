<script setup lang="ts">
import router from '@/router'
import ApiService from '@/Services/ApiService'
import '@/Styles/VistaConvenioEspecifico.css'
import type { ConvenioEspecificoCompleto } from '@/Types/Models'
import { isAxiosError } from 'axios'
import { onMounted, ref } from 'vue'
import { POSITION, useToast } from 'vue-toastification'

const idparam = router.currentRoute.value.params.id
let id = 0

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0])
} else {
  id = parseInt(idparam)
}

const toast = useToast()
const ConvenioEspecifico = ref<ConvenioEspecificoCompleto | null>(null)
const errorMensaje = ref('')

onMounted(async () => {
  try {
    const response = await ApiService.GetConvenioEspecificoCompleto(id)
    ConvenioEspecifico.value = response.data
  } catch (error) {
    if (isAxiosError(error) && error.response) {
      errorMensaje.value = ` ${error.response.data}`
    } else {
      errorMensaje.value = 'Lo sentimos, algo a salido mal'
    }
  }
})

const editConvenio = () => {
  if (ConvenioEspecifico.value) {
    router.push({
      name: 'EditConvenioEspecifico',
      params: { id: ConvenioEspecifico.value.id },
    })
  }
}

const DeleteConvenio = async () => {
  try {
    if (ConvenioEspecifico.value) {
      await ApiService.DeleteConvenioEspecifico(ConvenioEspecifico.value.id)
      toast.success("Convenio: " + ConvenioEspecifico.value.titulo + " eliminado con éxito");
      router.push({ name: 'ListaConvenios' })
    }
  } catch (error) {
    toast.error("Error al eliminar el convenio", { position: POSITION.BOTTOM_CENTER });
    if (isAxiosError(error) && error.response) {
      console.log(`Error: ${error.response.data.message}, ${error.response.data}`)
    } else {
      console.log(`Lo sentimos, algo ha salido mal. ${error}`)
    }
  }
}
</script>

<template>
  <div class="main-container">
    <div v-if="ConvenioEspecifico" class="convenio-especifico-detail">
      <div class="info-card convenio-card">
        <h2 class="card-title">{{ ConvenioEspecifico.titulo }}</h2>
        <p><strong>Nro. de Convenio:</strong> {{ ConvenioEspecifico.numeroconvenio }}</p>
        <p><strong>Fecha de Firma:</strong> {{ ConvenioEspecifico.fechaFirmaConvenio }}</p>
        <p>
          <strong>Inicio de Actividades:</strong> {{ ConvenioEspecifico.fechaInicioActividades }}
        </p>
        <p><strong>Fin del Convenio:</strong> {{ ConvenioEspecifico.fechaFinConvenio }}</p>
        <p v-if="ConvenioEspecifico.comentarioOpcional">
          <strong>Comentario:</strong> {{ ConvenioEspecifico.comentarioOpcional }}
        </p>
      </div>

      <div class="info-card involucrados-card">
        <h3 class="card-subtitle">Involucrados</h3>

        <div v-if="ConvenioEspecifico.involucrados?.length" class="involucrados-list">
          <div v-for="involucrado in ConvenioEspecifico.involucrados" :key="involucrado.id" class="involucrado-card">
            <h4 class="involucrado-nombre-completo">
              {{ involucrado.nombre }} {{ involucrado.apellido }}
            </h4>
            <p><strong>Rol:</strong> {{ involucrado.rolInvolucrado }}</p>
            <p>
              <strong>Email:</strong>
              <a :href="'mailto:' + involucrado.email">{{ involucrado.email }}</a>
            </p>
            <p><strong>Teléfono:</strong> {{ involucrado.telefono }}</p>
            <p><strong>Legajo:</strong> {{ involucrado.legajo }}</p>
          </div>
        </div>
        <div v-else class="no-involucrados-msg">
          <p>No hay involucrados para mostrar.</p>
        </div>
      </div>
    </div>
    <div v-else class="loading-state">
      <p>Cargando detalles del convenio...</p>
    </div>
  </div>

  <div class="button-container" v-if="ConvenioEspecifico">
    <button @click="editConvenio" class="btn-primary">Editar Convenio</button>
    <button @click="DeleteConvenio" class="btn-delete">Eliminar Convenio</button>
  </div>
</template>
