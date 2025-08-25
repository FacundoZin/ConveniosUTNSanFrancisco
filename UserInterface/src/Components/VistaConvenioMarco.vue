<template>
  <div class="main-container">
    <div v-if="errorMensaje" class="error-msg">
      {{ errorMensaje }}
    </div>

    <div v-else-if="!ConvenioMarco" class="loading-state">
      <p>Cargando convenio...</p>
    </div>

    <div v-else class="content-wrapper">
      <div class="info-card convenio-card">
        <h1 class="card-title">{{ ConvenioMarco.titulo }}</h1>
        <p><strong>Número:</strong> {{ ConvenioMarco.numeroconvenio }}</p>
        <p><strong>Fecha de firma:</strong> {{ ConvenioMarco.fechaFirmaConvenio }}</p>
        <p><strong>Fecha de fin:</strong> {{ ConvenioMarco.fechaFin }}</p>
        <p><strong>Comentario:</strong> {{ ConvenioMarco.comentarioOpcional || 'Sin comentarios' }}</p>
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
            <tr v-for="conv in ConvenioMarco.conveniosEspecificos" :key="conv.Id">
              <td>{{ conv.numeroconvenio }}</td>
              <td>{{ conv.Titulo }}</td>
              <td>{{ conv.FechaFirmaConvenio }}</td>
              <td>{{ conv.FechaInicioActividades }}</td>
              <td>{{ conv.FechaFinConvenio }}</td>
              <td>{{ conv.ConvenioType }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>

      <div class="edit-button-wrapper" v-if="ConvenioMarco">
        <button @click="editConvenio" class="edit-btn">Editar Convenio</button>
    </div>

</template>

<script setup lang="ts">
import router from '@/router';
import ApiService from '@/Services/ApiService';
import type { ConvenioMarcoCompleto } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const idparam = route.params.id;
let id = 0;

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0]);
} else {
  id = parseInt(idparam);
}

const ConvenioMarco = ref<ConvenioMarcoCompleto | null>(null);
const errorMensaje = ref('');

onMounted(async () => {
  try {
    const response = await ApiService.GetConvenioMarcoCompleto(id);
    ConvenioMarco.value = response.data;
  } catch (error) {
    if (isAxiosError(error) && error.response) {
      errorMensaje.value = `Error: ${error.response.data.message || error.response.data}`;
    } else {
      errorMensaje.value = "Lo sentimos, algo ha salido mal.";
    }
  }
});

const editConvenio = () => {
  if (ConvenioMarco.value) {
    router.push({
      name: 'EditConvenioMarco',
      params: { id: ConvenioMarco.value.idconvenio },
    });
  }
};
</script>

<style scoped>
.main-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
  font-family: 'Arial', sans-serif;
  padding-top: 35px;
  margin: 75px;
  background-color: #000000;
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
  border-left: 6px solid #0077c8; /* Línea azul UTN */
}

.empresa-card {
  border-left: 6px solid #1a1a1a; /* Línea negra para contraste */
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

/* Estilo para el botón de edición */
.edit-btn {
  background-color: #0077c8; /* Color azul UTN */
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
  background-color: #005ea3; /* Azul más oscuro al pasar el ratón */
}

</style>