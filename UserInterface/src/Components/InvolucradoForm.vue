<template>
    <form @submit.prevent="agregar" class="involucrado-form">
        <input v-model="form.nombre" placeholder="Nombre" required />
        <input v-model="form.apellido" placeholder="Apellido" required />
        <input v-model="form.email" placeholder="Email" type="email" />
        <input v-model="form.telefono" placeholder="Teléfono" />
        <input v-model.number="form.legajo" placeholder="Legajo (opcional)" />
        <select v-model.number="form.rolInvolucrado" required>
            <option value="" disabled selected>Seleccione un rol...</option>
            <option :value="0">Docente</option>
            <option :value="1">Alumno</option>
            <option :value="2">Secretario</option>
            <option :value="3">Externo</option>
        </select>
        <button type="submit" class="agregar-btn">Agregar Involucrado</button>
    </form>
</template>

<script setup lang="ts">
import '@/Styles/InvolucradosForm.css';
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
