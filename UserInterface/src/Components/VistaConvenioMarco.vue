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

.content-wrapper {
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

.empresa-card {
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

/* Estilos de la tabla de convenios específicos */
.specific-list-section {
  background-color: #f8f8f8;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: inset 0 0 10px rgba(0, 0, 0, 0.05);
}

.specific-table {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0 10px;
}

.specific-table th,
.specific-table td {
  padding: 15px;
  text-align: left;
}

.specific-table th {
  background-color: #0077c8;
  color: white;
  text-transform: uppercase;
  font-size: 0.9rem;
  letter-spacing: 1px;
}

.specific-table tr {
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
  transition: transform 0.2s ease;
}

.specific-table tr:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

/* Estilo para el mensaje de "no hay convenios" */
.no-specific-agreements {
  background-color: #e6f7ff;
  border: 1px dashed #0077c8;
  color: #005ea3;
  padding: 2rem;
  border-radius: 12px;
  text-align: center;
  font-style: italic;
  font-size: 1.1rem;
}

/* Mensaje de carga y error */
.loading-state,
.error-msg {
  text-align: center;
  margin-top: 5rem;
  font-size: 1.2rem;
  color: #888;
}

.error-msg {
  color: #d9534f;
}

/* Contenedor centrado con espacio entre botones */
.buttons-container {
  display: flex;
  justify-content: center;
  gap: 15px;
  margin: 10px auto;
  /* margen arriba y abajo */
  padding-bottom: 40px;
}

/* Base para todos los botones */
.btn {
  padding: 12px 24px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: bold;
  transition: all 0.3s ease;
  min-width: 200px;
  /* opcional: para que todos midan parecido */
}

/* Azul UTN */
.btn-primary {
  background-color: #0077c8;
  color: white;
}

.btn-primary:hover {
  background-color: #005ea3;
  transform: scale(1.05);

}

/* Rojo para eliminar */
.btn-delete {
  background-color: #d9534f;
  color: white;
}

.btn-delete:hover {
  background-color: #c9302c;
  transform: scale(1.05);
  /* efecto leve al hover */
}
</style>
