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
        <input v-model="form.telefono" type="text" class="form-control" required />
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
        <label class="form-label">area</label>
        <select v-model.number="form.idCarrera" class="form-select" required>
          <option value="" disabled>Seleccione una area...</option>
          <option v-for="area in areas" :key="area.id" :value="area.id">
            {{ area.nombre }}
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
import InvolucradoService from '@/modules/involucrados/services/InvolucradoService'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import { areasList } from '@/Types/AreasInvolucradas/AreasInvolucradas'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'

const props = defineProps<{
  involucradosExistentes?: InsertInvolucradosDto[]
}>()

const emit = defineEmits<{ (evento: 'agregar', inv: InsertInvolucradosDto): void }>()
const toast = useToast()

const areas = areasList
const isValidating = ref(false)

const form = ref<InsertInvolucradosDto>({
  nombre: '',
  apellido: '',
  email: null,
  telefono: '',
  legajo: null,
  idCarrera: 0,
  rolInvolucrado: 0,
})

const normalize = (s: string | null | undefined) => (s ?? '').toLowerCase().trim()

const agregar = async () => {
  if (!form.value.telefono || !form.value.telefono.trim()) {
    toast.error('El teléfono es obligatorio')
    return
  }

  // Check in-memory duplicates via prop
  const existentes = props.involucradosExistentes ?? []
  const dupInMemory = existentes.some(
    (ex) =>
      normalize(ex.nombre) === normalize(form.value.nombre) &&
      normalize(ex.apellido) === normalize(form.value.apellido) &&
      normalize(ex.telefono) === normalize(form.value.telefono),
  )
  if (dupInMemory) {
    toast.error('Ya existe un involucrado con el mismo nombre, apellido y teléfono en el formulario')
    return
  }

  // Validar contra BD con Nombre+Apellido+Telefono
  isValidating.value = true

  const validationResult = await InvolucradoService.ValidateInvolucrado({
    nombre: form.value.nombre || '',
    apellido: form.value.apellido || '',
    telefono: form.value.telefono || '',
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
    telefono: form.value.telefono ? form.value.telefono.trim() : '',
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
    telefono: '',
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
