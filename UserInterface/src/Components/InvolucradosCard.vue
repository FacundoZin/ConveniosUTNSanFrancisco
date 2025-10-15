<template>
    <div class="involucrado-card">
        <h4>{{ involucrado.nombre }} {{ involucrado.apellido }}</h4>
        <p>📧 {{ involucrado.email }}</p>
        <p>📞 {{ involucrado.telefono }}</p>
        <p>🎓 Legajo: {{ involucrado.legajo || 'N/A' }}</p>
        <p class="rol">Rol: {{ rolesMap[involucrado.rolInvolucrado] }}</p>
        <button @click="$emit('eliminar')" class="delete-btn">✕</button>
    </div>
</template>


<script setup lang="ts">
import '@/Styles/InvolucradosCard.css';
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados';

defineProps<{ involucrado: InsertInvolucradosDto }>()

const rolesMap = ['Docente', 'Alumno', 'Secretario', 'Externo']

</script>
