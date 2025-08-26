<script setup lang="ts">
import router from '@/router'
import ApiService from '@/Services/ApiService'
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

<style scoped>
.main-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
  font-family: 'Arial', sans-serif;
  padding-top: 35px;
  margin: 75px;
  background-color: #909090d3;
  border-radius: 12px;
}

.convenio-especifico-detail {
  display: flex;
  flex-direction: column;
  gap: 2rem;
}

/* Estilos para las tarjetas de información */
.info-card {
  background-color: #fff;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
  transition: transform 0.3s ease;
}

.info-card:hover {
  transform: translateY(-5px);
}

.convenio-card {
  border-left: 6px solid #0077c8;
  /* Línea azul UTN */
}

.involucrados-card {
  border-left: 6px solid #1a1a1a;
  /* Línea negra para contraste */
}

/* Estilos de tipografía y encabezados */
.card-title {
  font-size: 2.2rem;
  font-weight: bold;
  color: #0077c8;
  margin-top: 1px;
  margin-bottom: 1rem;
  border-bottom: 2px solid #e0e0e0;
  padding-bottom: 0.5rem;
}

.card-subtitle {
  border-bottom: 2px solid #e0e0e0;
  padding-bottom: 0.5rem;
  margin-top: 1px;
  font-size: 1.6rem;
  font-weight: 600;
  color: #1a1a1a;
  margin-bottom: 1rem;
}

p {
  line-height: 1.6;
}

strong {
  color: #1a1a1a;
}

.involucrados-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.involucrado-card {
  background-color: #f8f8f8;
  border-radius: 8px;
  padding: 1rem;
  border: 1px solid #e0e0e0;
  transition:
    box-shadow 0.2s ease,
    transform 0.2s ease;
}

.involucrado-card:hover {
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  transform: translateY(-2px);
}

.involucrado-nombre-completo {
  font-size: 1.2rem;
  font-weight: bold;
  color: #0077c8;
  margin-bottom: 0.5rem;
}

.no-involucrados-msg {
  background-color: #e6f7ff;
  border: 1px dashed #0077c8;
  color: #005ea3;
  padding: 2rem;
  border-radius: 12px;
  text-align: center;
  font-style: italic;
  font-size: 1.1rem;
}

.loading-state {
  text-align: center;
  margin-top: 5rem;
  font-size: 1.2rem;
  color: #888;
}

a {
  color: #0077c8;
  text-decoration: none;
}

a:hover {
  text-decoration: underline;
}

.button-container {
  display: flex;
  justify-content: center;
  margin-top: 10px;
  /* separalo del bloque gris */
  margin-bottom: 3rem;
  /* separación respecto al borde inferior de la pantalla */
}

/* Estilo para el botón de edición */
.edit-btn {
  background-color: #0077c8;
  /* Color azul UTN */
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: bold;
  transition: background-color 0.3s ease;
}

.edit-btn:hover {
  background-color: #005ea3;
  /* Azul más oscuro al pasar el ratón */
}

.delete-btn {
  background-color: #0077c8;
  /* Color azul UTN */
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: bold;
  transition: background-color 0.3s ease;
}

.delete-btn:hover {
  background-color: #005ea3;
  /* Azul más oscuro al pasar el ratón */
}
</style>
