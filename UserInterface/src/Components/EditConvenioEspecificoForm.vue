<template>
  <div class="edit-form-container">
    <div v-if="loading">Cargando formulario...</div>
    <div v-else-if="error">Error al cargar el convenio.</div>
    <div v-else-if="convenioData">
      <h2>Editando Convenio: {{ convenioData.titulo }}</h2>
      <form @submit.prevent="submitForm">
        <div class="form-group">
          <label for="titulo">Título:</label>
          <input type="text" id="titulo" v-model="convenioData.titulo" />
          <label for="numeroConvenio">Numero de convenio:</label>
          <input type="text" id="numeroConvenio" v-model="convenioData.numeroconvenio" />
          <label for="fechaFirma">Fecha de firma:</label>
          <input type="date" id="fechaFirma" v-model="convenioData.fechaFirmaConvenio" />
          <label for="fechaFin">Fecha de finalizacion:</label>
          <input type="date" id="fechaFin" v-model="convenioData.fechaFinConvenio" />
          <label for="comentario">Comentario:</label>
          <input type="text" id="comentario" v-model="convenioData.comentarioOpcional" />
        </div>
        <button type="submit" class="save-btn">Guardar Cambios</button>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import ApiService from '@/Services/ApiService';
import type { UpdateConvenioEspecificoDto } from '@/Types/Api.Interface';
import type { ConvenioEspecificoCompleto } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { POSITION, useToast } from 'vue-toastification';

const toast = useToast();
const route = useRoute();
const convenioData = ref<ConvenioEspecificoCompleto | null>(null);
const loading = ref(true);
const error = ref(false);
const id = ref(0);

onMounted(async () => {
  id.value = parseInt(Array.isArray(route.params.id) ? route.params.id[0] : route.params.id);

  if (id.value) {
    try {
      const response = await ApiService.GetConvenioMarcoCompleto(id.value);
      convenioData.value = response.data;
    } catch (e) {
      error.value = true;
      console.error(e);
    } finally {
      loading.value = false;
    }
  }
});

const submitForm = async () => {
  const dto: UpdateConvenioEspecificoDto = {
    id: id.value,
    numeroconvenio: convenioData.value?.numeroconvenio || 0,
    titulo: convenioData.value?.titulo || '',
    fechaFirmaConvenio: convenioData.value?.fechaFirmaConvenio || '',
    fechaInicioActividades: convenioData.value?.fechaInicioActividades || '',
    fechaFinConvenio: convenioData.value?.fechaFinConvenio || '',
    comentarioOpcional: convenioData.value?.comentarioOpcional || '',
  }

  try {
    const response = await ApiService.EditarConvenioEspecifico(dto);
    toast.success("Convenio editado con éxito");
  } catch (error) {
    if (isAxiosError(error)) {
      toast.error("Error al editar el convenio", { position: POSITION.BOTTOM_CENTER });
      if (error.response) {
        console.log(`Error al editar el convenio (${error.response.status}):`, error.response.data);
      } else {
        console.log(`Error al editar el convenio: no se recibió respuesta del servidor, ${error}`);
      }
    } else {
      console.log(`Lo sentimos, algo salió mal fuera del entorno HTTP, ${error}`);
    }
  }
};
</script>

<style scoped>
.edit-form-container {
  max-width: 800px;
  margin: 2rem auto;
  padding: 2rem;
  background-color: #fff;
  border-radius: 12px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.08);
}

.form-group {
  margin-bottom: 1rem;
}

.form-group label {
  display: block;
  font-weight: bold;
  margin-bottom: 0.5rem;
  color: #1a1a1a;
}

.form-group input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 1rem;
}

.save-btn {
  background-color: #00a1e4;
  color: white;
  padding: 12px 24px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.save-btn:hover {
  background-color: #0077c8;
}
</style>