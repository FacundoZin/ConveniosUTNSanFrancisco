<template>
    <form @submit.prevent="agregar" class="involucrado-form">
        <input v-model="form.nombre" placeholder="Nombre" required />
        <input v-model="form.apellido" placeholder="Apellido" required />
        <input v-model="form.email" placeholder="Email" type="email" required />
        <input v-model="form.telefono" placeholder="Teléfono" required />
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
import type { InsertInvolucradosDto, InsertInvolucradosFormDto } from '@/Types/Api.Interface';
import { ref } from 'vue';

const emit = defineEmits<{ (evento: 'agregar', inv: InsertInvolucradosDto): void }>()

const form = ref<InsertInvolucradosFormDto>({
    id: 0,
    nombre: '',
    apellido: '',
    email: '',
    telefono: '',
    legajo: '',
    rolInvolucrado: 0
})

const agregar = () => {
    const InsertInvolucradosDto = {
        id: form.value.id,
        nombre: form.value.nombre,
        apellido: form.value.apellido,
        email: form.value.email,
        telefono: form.value.telefono,
        legajo: form.value.legajo === '' ? 0 : Number(form.value.legajo),
        rolInvolucrado: form.value.rolInvolucrado
    }
    emit('agregar', InsertInvolucradosDto)
    form.value = {
        id: 0,
        nombre: '',
        apellido: '',
        email: '',
        telefono: '',
        legajo: '',
        rolInvolucrado: 0
    }
}
</script>
