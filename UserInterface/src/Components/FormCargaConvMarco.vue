<template>
  <div class="form-container">
    <form @submit.prevent="submitForm">
      <div class="form-section">
        <h2 class="form-title">Datos del Convenio Marco</h2>
        <div class="form-group">
          <label for="numeroConvenio">Número de Convenio:</label>
          <input type="number" id="numeroConvenio" v-model="UploadConvenioMarco.convenioDto.numeroconvenio" required />
        </div>
        <div class="form-group">
          <label for="titulo">Título:</label>
          <input type="text" id="titulo" v-model="UploadConvenioMarco.convenioDto.titulo" required />
        </div>
        <div class="form-group">
          <label for="fechaFirma">Fecha de Firma:</label>
          <input type="date" id="fechaFirma" v-model="UploadConvenioMarco.convenioDto.fechaFirmaConvenio" required />
        </div>
        <div class="form-group">
          <label for="fechaFin">Fecha de Finalizacion:</label>
          <input type="date" id="fechaFin" v-model="UploadConvenioMarco.convenioDto.fechaFin" required />
        </div>
        <div class="form-group">
          <label for="comentarioOpcional">Comentario (opcional):</label>
          <textarea id="comentarioOpcional" v-model="UploadConvenioMarco.convenioDto.comentarioOpcional"></textarea>
        </div>
      </div>
      <div class="form-section">
        <h2 class="form-title">Datos de la Empresa</h2>
        <div class="form-group">
          <label for="nombreEmpresa">Nombre de la Empresa:</label>
          <input type="text" id="nombreEmpresa" v-model="UploadConvenioMarco.empresaDto.nombre" required />
        </div>
        <div class="form-group">
          <label for="razonSocial">Razón Social:</label>
          <input type="text" id="razonSocial" v-model="UploadConvenioMarco.empresaDto.razonSocial" required />
        </div>
        <div class="form-group">
          <label for="cuit">CUIT:</label>
          <input type="text" id="cuit" v-model="UploadConvenioMarco.empresaDto.cuit" required />
        </div>
        <div class="form-group">
          <label for="direccion">Dirección:</label>
          <input type="text" id="direccion" v-model="UploadConvenioMarco.empresaDto.direccion" required />
        </div>
        <div class="form-group">
          <label for="telefono">Teléfono:</label>
          <input type="tel" id="telefono" v-model="UploadConvenioMarco.empresaDto.telefono" required />
        </div>
        <div class="form-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="UploadConvenioMarco.empresaDto.email" required />
        </div>
      </div>
      <button type="submit" class="submit-btn">Cargar Convenio</button>
    </form>
  </div>
</template>

<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { CargarConvenioMarcoRequestDto } from '@/Types/Api.Interface'
import { isAxiosError } from 'axios'
import { ref } from 'vue'
import { POSITION, useToast } from 'vue-toastification'

const toast = useToast()

const UploadConvenioMarco = ref<CargarConvenioMarcoRequestDto>({
  convenioDto: {
    numeroconvenio: 0,
    titulo: '',
    fechaFirmaConvenio: '',
    fechaFin: '',
    comentarioOpcional: '',
  },
  empresaDto: {
    nombre: '',
    razonSocial: '',
    cuit: '',
    direccion: '',
    telefono: '',
    email: '',
  },
})

const submitForm = async () => {
  try {
    await ApiService.CreateConvenioMarco(UploadConvenioMarco.value)
    toast.success(`Convenio "${UploadConvenioMarco.value.convenioDto.titulo}" cargado con éxito`)
  } catch (error) {
    toast.error(`Error al cargar el convenio`, { position: POSITION.BOTTOM_CENTER })
    if (isAxiosError(error)) {
      if (error.response) {
        console.log(`Error al cargar el convenio (${error.response.status}):`, error.response.data)
      } else {
        console.log('Error al cargar el convenio: no se recibió respuesta del servidor')
      }
    } else {
      console.error(error)
      console.log('Lo sentimos, algo salió mal fuera del entorno HTTP')
    }
  }
}
</script>

<style scoped>
.form-container {
  background-color: #f5f5f5;
  /* Gris claro */
  padding: 30px;
  border-radius: 10px;
  max-width: 800px;
  margin: 40px auto;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.form-section {
  background-color: #ffffff;
  /* Blanco */
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 25px;
  border: 1px solid #e0e0e0;
}

.form-title {
  color: #0077c8;
  /* Azul UTN */
  font-size: 1.5em;
  margin-bottom: 15px;
  border-bottom: 2px solid #0077c8;
  padding-bottom: 5px;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  font-weight: bold;
  margin-bottom: 5px;
  color: #333;
}

input[type='text'],
input[type='number'],
input[type='date'],
input[type='tel'],
input[type='email'],
textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-sizing: border-box;
  font-size: 1em;
  transition: border-color 0.3s;
}

input:focus,
textarea:focus {
  border-color: #0077c8;
  /* Azul UTN */
  outline: none;
}

textarea {
  resize: vertical;
  min-height: 80px;
}

.submit-btn {
  background-color: #0077c8;
  /* Azul UTN */
  color: white;
  padding: 12px 25px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1.1em;
  transition: background-color 0.3s ease;
  width: 100%;
}

.submit-btn:hover {
  background-color: #005ea3;
  /* Azul UTN más oscuro */
}
</style>
