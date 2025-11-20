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
                <input v-model.number="form.legajo" type="number" class="form-control" placeholder="Opcional" />
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

            <div class="col-12 text-end mt-4">
                <button type="submit" class="btn btn-outline-primary">
                    <i class="bi bi-plus-circle me-2"></i>Agregar Involucrado
                </button>
            </div>
        </div>
    </form>
</template>

<script setup lang="ts">
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados';
import { ref } from 'vue';

const emit = defineEmits<{ (evento: 'agregar', inv: InsertInvolucradosDto): void }>()

const form = ref<InsertInvolucradosDto>({
    nombre: '',
    apellido: '',
    email: null,
    telefono: null,
    legajo: null,
    rolInvolucrado: 0
})

const agregar = () => {
    const InsertInvolucradosDto: InsertInvolucradosDto = {
        nombre: form.value.nombre,
        apellido: form.value.apellido,
        email: form.value.email ? form.value.email : null,
        telefono: form.value.telefono ? form.value.telefono : null,
        legajo: form.value.legajo ? form.value.legajo : null,
        rolInvolucrado: form.value.rolInvolucrado
    }
    emit('agregar', InsertInvolucradosDto)
    form.value = {
        nombre: '',
        apellido: '',
        email: null,
        telefono: null,
        legajo: null,
        rolInvolucrado: 0
    }
}
</script>
