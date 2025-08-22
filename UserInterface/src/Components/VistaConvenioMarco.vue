<script setup lang="ts">
import ApiService from '@/Services/ApiService';
import type { ConvenioMarcoCompleto } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';


const router = useRoute();
const idparam= router.params.id;
let id = 0;

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0]);
} else {
  id = parseInt(idparam)
}

const ConvenioMarco = ref<ConvenioMarcoCompleto | null>(null);
const errorMensaje = ref('');

onMounted( async() => {
    try{
        const response = await ApiService.GetConvenioMarcoCompleto(id);
        ConvenioMarco.value = response.data;
    }catch(error){
        if(isAxiosError(error) && error.response){
      errorMensaje.value = ` ${error.response.data}`;
    }else{
      errorMensaje.value = "Lo sentimos, algo a salido mal"
    }
    }
})
</script>

<template>
  <div class="detalle-container">
    <div v-if="errorMensaje" class="error-msg">
      {{ errorMensaje }}
    </div>

    <div v-else-if="!ConvenioMarco">
      <p>Cargando convenio...</p>
    </div>

    <div v-else>
      <!-- Datos del Convenio Marco -->
      <div class="marco-card">
        <h2>{{ ConvenioMarco.Titulo }}</h2>
        <p><strong>Número:</strong> {{ ConvenioMarco.numeroconvenio }}</p>
        <p><strong>Fecha de firma:</strong> {{ ConvenioMarco.FechaFirmaConvenio }}</p>
        <p><strong>Fecha de fin:</strong> {{ ConvenioMarco.FechaFin }}</p>
        <p><strong>Comentario:</strong> {{ ConvenioMarco.ComentarioOpcional || 'Sin comentarios' }}</p>
      </div>

      <!-- Datos de la Empresa -->
      <div class="empresa-card">
        <h3>Empresa</h3>
        <p><strong>Nombre:</strong> {{ ConvenioMarco.empresa.Nombre_Empresa }}</p>
        <p><strong>Razón social:</strong> {{ ConvenioMarco.empresa.RazonSocial }}</p>
        <p><strong>CUIT:</strong> {{ ConvenioMarco.empresa.Cuit }}</p>
        <p><strong>Dirección:</strong> {{ ConvenioMarco.empresa.Direccion_Empresa }}</p>
        <p><strong>Teléfono:</strong> {{ ConvenioMarco.empresa.Telefono_Empresa }}</p>
        <p><strong>Email:</strong> {{ ConvenioMarco.empresa.Email_Empresa }}</p>
      </div>

      <!-- Lista de Convenios Específicos -->
      <div class="especificos-list">
        <h3>Convenios específicos</h3>
        <div v-if="ConvenioMarco.ConveniosEspecificos.length === 0">
          <p>No hay convenios específicos asociados.</p>
        </div>
        <table v-else>
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
            <tr v-for="conv in ConvenioMarco.ConveniosEspecificos" :key="conv.Id">
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
</template>



<style scoped>
.detalle-convenio {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  background: #fff;
  border-radius: 1rem;
  padding: 1.5rem;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
  animation: fadeIn 0.3s ease-in-out;
}

.detalle-convenio h2 {
  font-size: 1.5rem;
  font-weight: 600;
  color: #2c3e50;
  margin-bottom: 0.5rem;
}

.detalle-convenio p {
  margin: 0;
  font-size: 1rem;
  color: #555;
  line-height: 1.5;
}

.detalle-convenio strong {
  color: #2c3e50;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>