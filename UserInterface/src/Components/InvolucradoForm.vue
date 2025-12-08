<template>
  <form @submit.prevent="agregar" class="p-3 border rounded bg-white">
    <div class="row g-3">
      <div class="col-md-6">
        <label class="form-label">Nombre</label>
        <input v-model="form.nombre" type="text" class="form-control" required />
      </div>

      <div class="col-md-6">
        <label class="form-label">Apellido</label>
        <input v-model="form.apellido" type="text" class="form-control" required />
      </div>

      <div class="col-md-6">
        <label class="form-label">Email</label>
        <input v-model="form.email" type="email" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Teléfono</label>
        <input v-model="form.telefono" type="text" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Legajo</label>
        <input
          v-model.number="form.legajo"
          type="number"
          class="form-control"
          placeholder="Opcional"
        />
      </div>

      <div class="col-md-6">
        <label class="form-label">Rol</label>
        <select v-model.number="form.rolInvolucrado" class="form-select" required>
          <option value="" disabled selected>Seleccione un rol...</option>
          <option :value="0">Docente</option>
          <option :value="1">Alumno</option>
          <option :value="2">Secretario</option>
          <option :value="3">Externo</option>
        </select>
      </div>

      <div class="col-md-6">
        <label class="form-label">Carrera</label>
        <select v-model.number="form.idCarrera" class="form-select" required>
          <option value="" disabled>Seleccione una carrera...</option>
          <option v-for="carrera in carreras" :key="carrera.id" :value="carrera.id">
            {{ carrera.nombre }}
          </option>
        </select>
      </div>

      <div class="col-12 text-end mt-4">
        <button type="submit" class="btn btn-outline-primary" :disabled="isValidating">
          <span
            v-if="isValidating"
            class="spinner-border spinner-border-sm me-2"
            role="status"
          ></span>
          <i v-else class="bi bi-plus-circle me-2"></i>
          {{ isValidating ? 'Validando...' : 'Agregar Involucrado' }}
        </button>
      </div>
    </div>
  </form>
</template>

<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import { carrerasList } from '@/Types/CarrerasInvolucradas/CarrerasInvolucradas'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'

const emit = defineEmits<{ (evento: 'agregar', inv: InsertInvolucradosDto): void }>()
const toast = useToast()

const carreras = carrerasList
const isValidating = ref(false)

const form = ref<InsertInvolucradosDto>({
  nombre: '',
  apellido: '',
  email: null,
  telefono: null,
  legajo: null,
  idCarrera: 0,
  rolInvolucrado: 0,
})

const agregar = async () => {
  // Validar que no exista el involucrado
  isValidating.value = true

  const validationResult = await ApiService.ValidateInvolucrado({
    nombre: form.value.nombre || '',
    apellido: form.value.apellido || '',
  })

  isValidating.value = false

  if (!validationResult.isSuccess) {
    toast.error('Error al validar involucrado')
    return
  }

  if (validationResult.value.existe) {
    toast.error(validationResult.value.message, {
      timeout: 4000,
    })
    return
  }

  // Si no existe, proceder a agregar
  const InsertInvolucradosDto: InsertInvolucradosDto = {
    nombre: form.value.nombre,
    apellido: form.value.apellido,
    email: form.value.email ? form.value.email : null,
    telefono: form.value.telefono ? form.value.telefono : null,
    legajo: form.value.legajo ? form.value.legajo : null,
    idCarrera: form.value.idCarrera,
    rolInvolucrado: form.value.rolInvolucrado,
  }

  emit('agregar', InsertInvolucradosDto)

  // Reset form
  form.value = {
    nombre: '',
    apellido: '',
    email: null,
    telefono: null,
    legajo: null,
    idCarrera: 0,
    rolInvolucrado: 0,
  }
}
</script>

<style scoped>
/* Chrome, Safari, Edge, Opera */
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  appearance: none;
  margin: 0;
}

/* Firefox */
input[type='number'] {
  -moz-appearance: textfield;
  appearance: textfield;
}
</style>
